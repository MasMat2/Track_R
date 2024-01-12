using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Services.Archivos;

namespace TrackrAPI.Controllers.Archivos
{
    [ApiController]
    [Route("api/ArchivoTrackr")]
    public class ArchivoController : ControllerBase
    {
        private readonly ArchivoService _archivoService;

        public ArchivoController (ArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        [HttpPost]
        public void Agregar(ArchivoFormDTO archivoFormDTO)
        {
            _archivoService.Agregar(archivoFormDTO);
        }

        [HttpGet("{IdArchivo}")]
        public ArchivoDTO GetArchivo(int IdArchivo) { 
            return _archivoService.GetArchivo(IdArchivo);
        }

        [HttpGet("fileName/{IdArchivo}")]
        public string GetFileName(int IdArchivo)
        {
            return _archivoService.GetFileName(IdArchivo);
        }
    }
}
