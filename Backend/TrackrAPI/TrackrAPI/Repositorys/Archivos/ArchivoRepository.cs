using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Archivos;

public class ArchivoRepository: Repository<Archivo>, IArchivoRepository
{
    public ArchivoRepository(TrackrContext context): base(context) { }

}

