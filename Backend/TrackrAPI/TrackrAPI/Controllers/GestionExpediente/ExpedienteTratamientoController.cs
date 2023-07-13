using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteTratamientoController
    {
        ExpedienteTratamientoService expedienteTratamientoService;

        public ExpedienteTratamientoController(ExpedienteTratamientoService expedienteTratamientoService)
        {
            this.expedienteTratamientoService = expedienteTratamientoService;
        }
        [HttpGet("consultarPorUsuario/{idUsuario}")]
        public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteTratamientoService.ConsultarPorUsuario(idUsuario);
        }


        [HttpGet("consultar/{idExpedienteTratamiento}")]
        public ExpedienteTratamiento Consultar(int idExpedienteTratamiento)
        {
            return expedienteTratamientoService.Consultar(idExpedienteTratamiento);
        }
    }
}
