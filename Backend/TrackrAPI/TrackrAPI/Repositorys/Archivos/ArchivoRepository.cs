using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Archivos;

public class ArchivoRepository: Repository<Archivo>, IArchivoRepository
{
    public ArchivoRepository(TrackrContext context): base(context) { }

    public Archivo GetArchivo(int idArchivo)
    {
        return context.Archivo.Where(x => x.IdArchivo == idArchivo).FirstOrDefault();
    }

    public string GetFileName(int idArchivo)
    {
        return context.Archivo.Where(x => x.IdArchivo == idArchivo).Select(x => x.Nombre).FirstOrDefault();
    }

}

