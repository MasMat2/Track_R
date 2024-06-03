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
    public class ExpedienteTratamientoController : ControllerBase
    {
        private readonly ExpedienteTratamientoService expedienteTratamientoService;

        public ExpedienteTratamientoController(ExpedienteTratamientoService expedienteTratamientoService)
        {
            this.expedienteTratamientoService = expedienteTratamientoService;
        }

        [HttpGet("consultarParaGrid/{idUsuario}")]
        public IEnumerable<ExpedienteTratamientoGridDTO> ConsultarParaGrid(int idUsuario)
        {
            return expedienteTratamientoService.ConsultarParaGrid(idUsuario);
        }

        [HttpGet("consultarTratamientos")]
        public IEnumerable<ExpedienteTratamientoDetalleDto> ConsultarTratamientos()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.ConsultarTratamientos(idUsuario);
        }

        [HttpGet("consultarTratamientosTrackr")]
        public IEnumerable<ExpedienteTratamientoPerfilDto> ConsultarTratamientosTrackr()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.ConsultarTratamientosTrackr(idUsuario);
        }

        [HttpGet("consultarTratamientoDetalle/{idExpedienteTratamiento}")]
        public ExpedienteTratamientoDetalleDto ConsultarTratamientoParaDetalle(int idExpedienteTratamiento)
        {
            return expedienteTratamientoService.ConsultarTratamientoParaDetalle(idExpedienteTratamiento);
        }

        [HttpPost("agregar")]
        public int Agregar(ExpedienteTratamientoDetalleDto expedienteTratamientoDto)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.Agregar(expedienteTratamientoDto, idUsuario);
        }

        [HttpPut("editar")]
        public int EditarTratamiento(ExpedienteTratamientoDetalleDto expedienteTratamientoDto)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return expedienteTratamientoService.EditarTratamiento(expedienteTratamientoDto, idUsuario);
        }

        [HttpDelete("eliminar/{idExpedienteTratamiento}")]
        public void Eliminar(int idExpedienteTratamiento)
        {
            expedienteTratamientoService.EliminarTratamiento(idExpedienteTratamiento);
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
