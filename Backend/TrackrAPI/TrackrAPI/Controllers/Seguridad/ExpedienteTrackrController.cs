using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteTrackrController : ControllerBase
    {
        private ExpedienteTrackrService expedienteTrackrService;

        public ExpedienteTrackrController(ExpedienteTrackrService expedienteTrackrService)
        {
            this.expedienteTrackrService = expedienteTrackrService;
        }

        [HttpGet]
        [Route("consultarWrapperPorUsuario/{idUsuario}")]
        public ExpedienteWrapper ConsultarWrapperPorUsuario(int idUsuario)
        {
            return expedienteTrackrService.ConsultarWrapperPorUsuario(idUsuario);
        }

        [HttpPost("agregarWrapper")]
        public void AgregarWrapper(ExpedienteWrapper expedienteWrapper)
        {
            expedienteTrackrService.AgregarWrapper(expedienteWrapper);
        }

        [HttpPost("editarWrapper")]
        public void EditarWrapper(ExpedienteWrapper expedienteWrapper)
        {
            expedienteTrackrService.EditarWrapper(expedienteWrapper);
        }

    }
}
