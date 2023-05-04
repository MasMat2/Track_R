using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private TipoUsuarioService tipoUsuarioService;

        public TipoUsuarioController(TipoUsuarioService tipoUsuarioService)
        {
            this.tipoUsuarioService = tipoUsuarioService;
        }

        [HttpGet]
        [Route("consultarTipoAdministrador")]
        public TipoUsuarioDto ConsultarTipoAdministrador()
        {
            return tipoUsuarioService.ConsultarDto(GeneralConstant.ClaveTipoUsuarioAdministrador);
        }

        [HttpGet]
        [Route("consultarTiposUsuarioSelector")]
        public IEnumerable<TipoUsuarioDto> ConsultarTiposUsuarioSelector()
        {
            return tipoUsuarioService.ConsultarTiposUsuarioSelector();
        }
    }
}