using DocumentFormat.OpenXml.Office.Word;
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

    public ArchivoDTO GetArchivo(int idArchivo)
    {
        ArchivoDTO archivo = new ArchivoDTO();
        var file = _archivoRepository.GetArchivo(idArchivo);
        archivo.Archivo = Convert.ToBase64String(file.Archivo1);
        archivo.ArchivoMime = file.ArchivoTipoMime;
        archivo.IdArchivo = file.IdArchivo;
        archivo.Nombre = file.ArchivoNombre;
        return archivo;
    }

    public string GetFileName(int idArchivo)
    {
        return _archivoRepository.GetFileName(idArchivo);
    }
}
