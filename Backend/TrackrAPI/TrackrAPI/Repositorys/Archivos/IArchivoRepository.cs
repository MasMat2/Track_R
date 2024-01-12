using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Archivos;

public interface IArchivoRepository : IRepository<Archivo>
{
    public Archivo GetArchivo(int idArchivo);
    public string GetFileName(int idArchivo);
}

