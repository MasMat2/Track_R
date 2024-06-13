using DocumentFormat.OpenXml.Office.Word;
using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Services.Sftp;

namespace TrackrAPI.Services.Archivos;

public class ArchivoService
{
    private readonly IArchivoRepository _archivoRepository;
    private readonly SftpService _sftpService;

    public ArchivoService(IArchivoRepository archivoRepository,
                          SftpService sftpService)
    {
        _archivoRepository = archivoRepository;
        _sftpService = sftpService;
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
        archivo.Archivo = file.ArchivoUrl != null ? _sftpService.DownloadFileAsBase64(file.ArchivoUrl): "";
        archivo.ArchivoMime = file.ArchivoTipoMime;
        archivo.IdArchivo = file.IdArchivo;
        archivo.Nombre = file.ArchivoNombre;
        return archivo;
    }

    public string GetFileName(int idArchivo)
    {
        return _archivoRepository.GetFileName(idArchivo);
    }

    public string GetFileMime(int idArchivo)
    {
        return _archivoRepository.GetFileMime(idArchivo);
    }

    public Archivo? ObtenerImagenUsuario(int idUsuario)
    {
        return _archivoRepository.ObtenerImagenUsuario(idUsuario);
    }
}
