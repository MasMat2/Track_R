using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Services.Seguridad;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedientePadecimientoController : ControllerBase
    {
        private ExpedientePadecimientoService expedientePadecimientoService;

        public ExpedientePadecimientoController(ExpedientePadecimientoService expedientePadecimientoService)
        {
            this.expedientePadecimientoService = expedientePadecimientoService;
        }
        [HttpGet("consultarParaSelector")]
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector()
        {
            return expedientePadecimientoService.ConsultarParaSelector();
        }

        [HttpGet("consultarPorUsuario/{idUsuario}")]
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedientePadecimientoService.ConsultarPorUsuario(idUsuario);
        }


    }
}
