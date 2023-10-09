using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteConsumoMedicamentoController : ControllerBase
    {
        private readonly ExpedienteConsumoMedicamentoService expedienteConsumoMedicamentoService;

        public ExpedienteConsumoMedicamentoController(ExpedienteConsumoMedicamentoService expedienteConsumoMedicamentoService)
        {
            this.expedienteConsumoMedicamentoService = expedienteConsumoMedicamentoService;
        }

        [HttpGet("consultarParaGrid/{idUsuario}")]
        public IEnumerable<ExpedienteConsumoMedicamentoGridDto> ConsultarParaGrid(int idUsuario)
        {
            return expedienteConsumoMedicamentoService.ConsultarParaGrid(idUsuario);
        }

        [HttpGet("consultarConsumoMedicamento")]
        public IEnumerable<ExpedienteConsumoMedicamentoDto> ConsultarConsumoMedicamento()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteConsumoMedicamentoService.ConsultarConsumoMedicamento(idUsuario);
        }

    }

}
