using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Services.GestionExpediente;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackrAPI.Controllers.GestionExpediente
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
         [HttpGet("consultaParaSidebar/{idUsuario}")]
        public IEnumerable<ExpedienteSidebarDTO> ConsultaTest(int idUsuario)
        {
            return expedientePadecimientoService.ConsultaTest(idUsuario);
        }


    }
}
