using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;

namespace TrackrAPI.Controllers.GestionExpediente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteTratamientoController: ControllerBase
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

        [HttpGet("grid/usuario/{idUsuario}")]
        public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarParaGrid(int idUsuario)
        {
            return expedienteTratamientoService.ConsultarParaGrid(idUsuario);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IEnumerable<ExpedienteTratamientoDto> ConsultarPorUsuario(int idUsuario)
        {
            return expedienteTratamientoService.ConsultarPorUsuario(idUsuario);
        }

        [HttpGet("")]
        public IEnumerable<ExpedienteTratamientoDto> ConsultarTratamientos()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.ConsultarTratamientos(idUsuario);
        }

        [HttpPost("agregar")]
        public int Agregar(ExpedienteTratamientoDto expedienteTratamientoDto)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.Agregar(expedienteTratamientoDto, idUsuario);
        }


        [HttpGet("selectorDeDoctor")]
        public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor()
        {
            return expedienteTratamientoService.SelectorDeDoctor();
        }

        [HttpGet("selectorDePadecimiento")]
        public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.SelectorDePadecimiento(idUsuario);
        }

    }
}
