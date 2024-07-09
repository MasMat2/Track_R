using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Sftp;

public class SftpCacheRepository : Repository<SftpCache>, ISftpCacheRepository
{
    public SftpCacheRepository(TrackrContext context) : base(context) { }

    public DateTime GetLastWriteTime(string filePath)
    {
        // Replace '\' by '/' for Linux
        filePath = filePath.Replace('\\', '/');

        var sftpFile = context.SftpCache
            .FirstOrDefault(cache => cache.FilePath == filePath);

        return sftpFile != null ? sftpFile.LastWriteTime : DateTime.MinValue;
    }

    public DateTime UpdateLastWriteTime(string filePath, DateTime lastWriteTime)
    {
        // Replace '\' by '/' for Linux
        filePath = filePath.Replace('\\', '/');

        var sftpFile = context.SftpCache
            .FirstOrDefault(file => file.FilePath == filePath);

        if (sftpFile == null)
        {
            sftpFile = new SftpCache
            {
                FilePath = filePath
            };
            context.SftpCache.Add(sftpFile);
        }
        else
        {
            context.SftpCache.Attach(sftpFile); // Attach the file to track changes
        }

        sftpFile.LastWriteTime = lastWriteTime;
        context.SaveChanges();

        return sftpFile.LastWriteTime;
    }

}

