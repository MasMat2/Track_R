using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Sftp;

public interface ISftpCacheRepository : IRepository<SftpCache>
{
    public DateTime GetLastWriteTime(string file_path);
    public DateTime UpdateLastWriteTime(string filePath, DateTime updatedLastWriteTime);

    // public DateTime UpdateLastWriteTime(string filePath, string lastWriteTimeString);
}

