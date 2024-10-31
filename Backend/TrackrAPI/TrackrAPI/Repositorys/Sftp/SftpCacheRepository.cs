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
        var local = context.Set<SftpCache>()
            .Local
            .FirstOrDefault(entry => entry.Id.Equals(sftpFile.Id));
        
        if (local != null)
        {
            // Detach the local instance if it exists
            context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

        // Attach the entity
        context.SftpCache.Attach(sftpFile);
    }

    sftpFile.LastWriteTime = lastWriteTime;
    context.SaveChanges();

    return sftpFile.LastWriteTime;
}

}

