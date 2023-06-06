using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteEstudioController
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
    }
}
