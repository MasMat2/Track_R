using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting.Internal;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.IO;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Repositorys.Sftp;

namespace TrackrAPI.Services.Sftp
{
    public class SftpService
    {
        private string host;
        private string username;
        private string password;
        private string root_path;
        private int port; 
        private readonly string _server;
        private readonly string _database;
        private IWebHostEnvironment hostingEnvironment;
        private ISftpCacheRepository sftpCacheRepository;

        public SftpService(IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment,
            ISftpCacheRepository sftpCacheRepository)
        {
            // Read from appsettings.json
            host = configuration["SFTP:Host"]; // 198.251.66.79
            username = configuration["SFTP:Usuario"]; // roadisftp
            password = configuration["SFTP:Contrasena"]; // sftproadis
            port = int.Parse(configuration["SFTP:Puerto"]); // 22
            root_path = configuration["SFTP:RootPath"] ?? "/srv/roadis"; // /srv/roadis
            root_path = root_path + "/servers"; // separate files by db server and db

            // Get server and database name
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var builder = new SqlConnectionStringBuilder(connectionString);
            _server = builder.DataSource;
            _database = builder.InitialCatalog;

            this.hostingEnvironment = hostingEnvironment;

            this.sftpCacheRepository = sftpCacheRepository;
        }

        public string GetLocalPath(string filePath)
        {
            return Path.Combine(hostingEnvironment.ContentRootPath, filePath);
        }
        public string GetRemotePath(string filePath)
        {
            // Separate by server and database
            string remote_path = Path.Combine(this.root_path, this._server, this._database, filePath);
            // Replace '\' by '/' for Linux
            return remote_path.Replace('\\', '/'); 
        }

        public void UploadFile(string filePath, string base64file)
        {

            string localFilePath = GetLocalPath(filePath);
            // Write locally first
            File.WriteAllBytes(localFilePath, Convert.FromBase64String(base64file));


            // Connect to sftp server
            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();
                // Read local filePath
                using (Stream fileStream = File.OpenRead(localFilePath))
                {
                    // Write in remote filePath
                    string linuxPath = GetRemotePath(filePath);
                    
                    // Create directory structure if it does not exist
                    string[] folders = linuxPath.Split('/');
                    string currentFolder = "";
                    for (int i = 0; i < folders.Length - 1; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(folders[i]))
                        {
                            currentFolder += "/" + folders[i];
                            if (!sftp.Exists(currentFolder))
                                sftp.CreateDirectory(currentFolder);
                        }
                    }

                    // Upload File
                    sftp.UploadFile(fileStream, linuxPath);
                    
                    var fileInfo = sftp.GetAttributes(linuxPath);
                    this.UpdateCache(filePath, fileInfo.LastWriteTime);
                }
                sftp.Disconnect();
            }
        }

        public void UploadBytesFile(string filePath, string base64file)
        {
            byte[] fileBytes = Convert.FromBase64String(base64file);
        
            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();
        
                // Write in remote filePath
                string linuxPath = GetRemotePath(filePath);
                
                // Create directory structure if it does not exist
                string[] folders = linuxPath.Split('/');
                string currentFolder = "";
                for (int i = 0; i < folders.Length - 1; i++)
                {
                    if (!string.IsNullOrWhiteSpace(folders[i]))
                    {
                        currentFolder += "/" + folders[i];
                        if (!sftp.Exists(currentFolder))
                            sftp.CreateDirectory(currentFolder);
                    }
                }
        
                // Upload File
                using (var memStream = new MemoryStream(fileBytes))
                {
                    sftp.UploadFile(memStream, linuxPath);
                }
        
                sftp.Disconnect();
            }
        }

        public string DownloadFile(string filePath)
        {
            byte[] fileContent;

            filePath = NormalizeFilePath(filePath);


            var lastWriteTime = this.sftpCacheRepository.GetLastWriteTime(filePath);

            string localFilePath = GetLocalPath(filePath);

            var directoryPath = Path.GetDirectoryName(localFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (File.GetLastWriteTime(localFilePath) != lastWriteTime || !File.Exists(localFilePath)){
                try
                {
                    using (var sftp = new SftpClient(host, port, username, password))
                    {
                        sftp.Connect();
                        
                        // Get Remote filePath
                        string linuxPath = GetRemotePath(filePath);

                        // Download File
                        using (Stream fileStream = new FileStream(localFilePath, FileMode.OpenOrCreate))
                        {
                            try
                            {
                                sftp.DownloadFile(linuxPath, fileStream);
                            }
                            catch (SftpPathNotFoundException ex)
                            {
                                Console.WriteLine("The file does not exist.");
                            }
                        }

                        // Update Cache
                        try
                        {
                            var fileInfo = sftp.GetAttributes(linuxPath);
                            this.UpdateCache(filePath, fileInfo.LastWriteTime);
                        }
                        catch(SftpPathNotFoundException ex)
                        {
                            Console.WriteLine("The file does not exist.");
                        }

                        sftp.Disconnect();
                    }
                }
                catch (IOException ex) when (ex.HResult == -2147024816)
                {
                    Console.WriteLine("The file already exists. Another process has created the file.");
                }
            }

            fileContent = File.ReadAllBytes(localFilePath);

            return Convert.ToBase64String(fileContent);
        }

        private static string NormalizeFilePath(string filePath)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
            return isWindows ? filePath.Replace("/", "\\") : filePath.Replace("\\", "/");
        }


        public string DownloadFileAsBase64(string filePath)
        {
            byte[] fileContent;

            try
            {
                using (var sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    using (var memStream = new MemoryStream())
                    {
                        // Remote filePath
                        string linuxPath = GetRemotePath(filePath);
                        sftp.DownloadFile(linuxPath, memStream);
                        fileContent = memStream.ToArray();
                    }
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

            return Convert.ToBase64String(fileContent);
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                using (var sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();

                    string linuxPath = GetRemotePath(filePath);

                    if (sftp.Exists(linuxPath))
                    {
                        sftp.DeleteFile(linuxPath);
                    }

                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción de manera que no detenga el programa
                Console.WriteLine($"Error al eliminar el archivo: {ex.Message}");
            }
        }

        private void UpdateCache(string filePath, DateTime lastWriteTime){
            
            var localFilePath = GetLocalPath(filePath);
            File.SetLastWriteTime(localFilePath, lastWriteTime);
            this.sftpCacheRepository.UpdateLastWriteTime(filePath, lastWriteTime);
        }

    }
}
