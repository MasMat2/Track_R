using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Services.Archivos;

namespace TrackrAPI.Controllers.Archivos
{
    [ApiController]
    [Route("api/{controller}")]
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
    }
}
