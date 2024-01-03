using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;

namespace TrackrAPI.Services.Archivos;

public class ArchivoService
{
    private readonly IArchivoRepository _archivoRepository;

    public ArchivoService (IArchivoRepository archivoRepository)
    {
        _archivoRepository = archivoRepository;
    }

    public Archivo Agregar(ArchivoFormDTO archivoFormDTO)
    {
        var archivo = new Archivo
        {
            Nombre = archivoFormDTO.Nombre,
            ArchivoNombre = archivoFormDTO.ArchivoNombre,
            ArchivoTipoMime = archivoFormDTO.ArchivoTipoMime,
            Archivo1 = archivoFormDTO.Archivo,
            FechaRealizacion = archivoFormDTO.FechaRealizacion,
            IdUsuario = archivoFormDTO.IdUsuario
        };
        return _archivoRepository.Agregar(archivo);
    }
}
