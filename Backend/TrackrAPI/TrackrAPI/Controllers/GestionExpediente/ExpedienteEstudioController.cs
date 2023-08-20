using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente

{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteEstudioController : ControllerBase
    {
        private readonly ExpedienteEstudioService _expedienteEstudioService;

        public ExpedienteEstudioController(ExpedienteEstudioService expedienteEstudioService)
        {
            _expedienteEstudioService = expedienteEstudioService;
        }

        [HttpGet("consultarPorUsuario/{idUsuario}")]
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return _expedienteEstudioService.ConsultarPorUsuario(idUsuario);
        }

        [HttpGet("consultar/{idExpedienteEstudio}")]
        public ExpedienteEstudio Consultar(int idExpedienteEstudio)
        {
            return _expedienteEstudioService.Consultar(idExpedienteEstudio);
        }

        [HttpPost]
        public void Agregar(ExpedienteEstudioFormularioCapturaDTO expedienteEstudio)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            _expedienteEstudioService.Agregar(expedienteEstudio, idUsuario);
        }

        [HttpGet("grid")]
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarParaGrid()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return _expedienteEstudioService.ConsultarPorUsuario(idUsuario);
        }

        [HttpDelete]
        [Route("{idExpedienteEstudio}")]
        public void Eliminar(int idExpedienteEstudio)
        {
            _expedienteEstudioService.Eliminar(idExpedienteEstudio);
        }

    }
}
