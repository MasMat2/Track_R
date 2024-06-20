using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Archivos;

public interface IArchivoRepository : IRepository<Archivo>
{
    public Archivo GetArchivo(int idArchivo);
    public string GetFileName(int idArchivo);
    public string GetFileMime(int idArchivo);
    public Archivo? ObtenerImagenUsuario(int idUsuario);
    public Task<Archivo?> ObtenerImagenUsuarioAsync(int idUsuario);
}

