using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
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

        [HttpGet("consultarPorUsuarioSelector")]
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarPorUsuarioParaSelector()
        {
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedientePadecimientoService.ConsultarPorUsuarioParaSelector(idUsuario);
        }

        [HttpGet("valoresFueraRango/{idPadecimiento},{idUsuario}")]
        public IEnumerable<PadecimientoFueraRangoDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario)
        {
            return expedientePadecimientoService.ConsultarValoresFueraRango(idPadecimiento, idUsuario);
        }

        [HttpGet("grid/usuario/{idUsuario}")]
        public IEnumerable<ExpedientePadecimientoGridDTO> ConsultarParaGridPorUsuario(int idUsuario)
        {
            return expedientePadecimientoService.ConsultarParaGridPorUsuario(idUsuario);
        }

        [HttpDelete("{idExpedientePadecimiento}")]
        public void EliminarExpedientePadecimiento(int idExpedientePadecimiento)
        {
            expedientePadecimientoService.Eliminar(idExpedientePadecimiento);
        }

        [HttpPost("agregarExpedientePadecimiento")]
        public int AgregarExpedientePadecimiento(AgregarExpedientePadecimientoDTO expedientePadecimientoDTO)
        {
            return expedientePadecimientoService.AgregarPadecimiento(expedientePadecimientoDTO, Utileria.ObtenerIdUsuarioSesion(this));
        }

    }
}
