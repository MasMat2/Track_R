using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting.Internal;
using Renci.SshNet;
using System.IO;
using TrackrAPI.Dtos.Seguridad;

namespace TrackrAPI.Services.Sftp
{
    public class SftpService
    {
        private string host;
        private string username;
        private string password;
        private int port; 
        private readonly string _server;
        private readonly string _database;
        private IWebHostEnvironment hostingEnvironment;

        public SftpService(IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            // Read from appsettings.json
            host = configuration["SFTP:Host"]; // 198.251.66.79
            username = configuration["SFTP:Usuario"]; // roadisftp
            password = configuration["SFTP:Contrasena"]; // sftproadis
            port = int.Parse(configuration["SFTP:Puerto"]); // 22

            // Get server and database name
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var builder = new SqlConnectionStringBuilder(connectionString);
            _server = builder.DataSource;
            _database = builder.InitialCatalog;

            this.hostingEnvironment = hostingEnvironment;
        }

        public string GetLocalPath(string filePath)
        {
            return Path.Combine(hostingEnvironment.ContentRootPath, filePath);
        }
        public string GetRemotePath(string filePath)
        {
            // Separate by server and database
            string remote_path = Path.Combine("/srv/roadis/servers", this._server, this._database, filePath);
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
                }
                sftp.Disconnect();
            }
        }

        public string DownloadFile(string filePath)
        {
            byte[] fileContent;

            string localFilePath = GetLocalPath(filePath);
            try
            {
                using (var sftp = new SftpClient(host, port, username, password))
                {
                    sftp.Connect();
                    using (Stream fileStream = new FileStream(localFilePath, FileMode.CreateNew))
                    {
                        // Remote filePath
                        string linuxPath = GetRemotePath(filePath);
                        sftp.DownloadFile(linuxPath, fileStream);
                    }
                    sftp.Disconnect();
                }
            }
            catch (IOException ex) when (ex.HResult == -2147024816)
            {
                Console.WriteLine("The file already exists. Another process has created the file.");
            }

            fileContent = File.ReadAllBytes(localFilePath);

            return Convert.ToBase64String(fileContent);
        }
    }
}
