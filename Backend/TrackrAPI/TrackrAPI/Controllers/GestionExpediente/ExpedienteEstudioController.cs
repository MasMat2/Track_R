using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteEstudioController:ControllerBase
    {
        ExpedienteEstudioService expedienteEstudioService;

        public ExpedienteEstudioController(ExpedienteEstudioService expedienteEstudioService)
        {
            this.expedienteEstudioService = expedienteEstudioService;
        }
        [HttpGet("consultarPorUsuario/{idUsuario}")]
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteEstudioService.ConsultarPorUsuario(idUsuario);
        }

        [HttpGet("consultar/{idExpedienteEstudio}")]
        public ExpedienteEstudio Consultar(int idExpedienteEstudio)
        {
            return expedienteEstudioService.Consultar(idExpedienteEstudio);
        }
 
        [HttpPost]
        public void Agregar(ExpedienteEstudioFormularioCapturaDTO expedienteEstudio)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            expedienteEstudioService.Agregar(expedienteEstudio, idUsuario);
        }
        [HttpGet("grid")]
        public IEnumerable<ExpedienteEstudioGridDTO> ConsultarParaGrid()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteEstudioService.ConsultarPorUsuario(idUsuario);
        }

        [HttpDelete]
        [Route("{idExpedienteEstudio}")]
        public void Eliminar(int idExpedienteEstudio)
        {
            expedienteEstudioService.Eliminar(idExpedienteEstudio);
        }
    }
}
