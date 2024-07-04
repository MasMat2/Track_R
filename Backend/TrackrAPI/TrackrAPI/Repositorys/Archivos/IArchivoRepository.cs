using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Archivos;

public interface IArchivoRepository : IRepository<Archivo>
{
    public Archivo GetArchivo(int idArchivo);
    public Archivo GetArchivoByUrl(string urlArchivo);
    public string GetFileName(int idArchivo);
    public int? GetFileId(string urlArchivo);
    public string GetFileMime(int idArchivo);
    public Archivo? ObtenerImagenUsuario(int idUsuario);
    public Task<Archivo?> ObtenerImagenUsuarioAsync(int idUsuario);
}

