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
        private readonly ExpedienteTratamientoService expedienteTratamientoService;

        public ExpedienteTratamientoController(ExpedienteTratamientoService expedienteTratamientoService)
        {
            this.expedienteTratamientoService = expedienteTratamientoService;
        }

        [HttpGet("{idExpedienteTratamiento}")]
        public ExpedienteTratamientoDto? Consultar(int idExpedienteTratamiento)
        {
            return expedienteTratamientoService.Consultar(idExpedienteTratamiento);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteTratamientoService.ConsultarPorUsuario(idUsuario);
        }
    }   
}
