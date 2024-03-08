using TrackrAPI.Services.Catalogo;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Models;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private LocalidadService localidadService;

        public LocalidadController(
            LocalidadService localidadService
        )
        {
            this.localidadService = localidadService;
        }

        [HttpGet]
        [Route("consultarPorEstado/{idEstado}")]
        public IEnumerable<Localidad> ConsultarPorEstado(int idEstado)
        {
            return localidadService.ConsultarPorEstado(idEstado);
        }
    }
}
