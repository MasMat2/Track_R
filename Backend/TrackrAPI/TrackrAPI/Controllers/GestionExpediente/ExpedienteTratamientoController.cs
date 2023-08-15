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


        [HttpGet("usuario/{idUsuario}")]
        public IEnumerable<ExpedienteTratamientoDto> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteTratamientoService.ConsultarPorUsuario(idUsuario);
        }

        [HttpGet("selectorDeDoctor")]
        public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor()
        {
            return expedienteTratamientoService.SelectorDeDoctor();
        }

        [HttpGet("selectorDePadecimiento/{idUsuario}")]
        public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento(int idUsuario)
        {
            return expedienteTratamientoService.SelectorDePadecimiento(idUsuario);
        }
        
        [HttpPost("agregar")]
        public int Agregar(ExpedienteTratamientoDto expedienteTratamientoDto)
        {
            return expedienteTratamientoService.Agregar(expedienteTratamientoDto);
        }
        
    }   
}
