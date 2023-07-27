using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private RolService rolService;
        private UsuarioService usuarioService;

        public RolController(RolService rolService, UsuarioService usuarioService)
        {
            this.rolService = rolService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<RolDto> ConsultarTodosParaSelector()
        {
            return rolService.ConsultarTodosParaSelector();
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<RolGridDto> ConsultarTodosParaGrid()
        {
            var usuarioSesion = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return rolService.ConsultarTodosParaGrid((int)usuarioSesion.IdCompania);
        }

        [HttpGet]
        [Route("consultarRolesUsuarioSesion")]
        public IEnumerable<RolDto> ConsultarRolesUsuarioSesion()
        {
            return rolService.ConsultarPorUsuario(Utileria.ObtenerIdUsuarioSesion(this));
        }


        [HttpGet]
        [Route("consultar/{idRol}")]
        public RolDto Consultar(int idRol)
        {
            return rolService.ConsultarDto(idRol);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Rol rol)
        {
            var usuarioSesion = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            rol.IdCompania = usuarioSesion.IdCompania;
            rolService.Agregar(rol);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Rol rol)
        {
            rolService.Editar(rol);
        }

        [HttpDelete]
        [Route("eliminar/{idRol}")]
        public void Eliminar(int idRol)
        {
            rolService.Eliminar(idRol);
        }
    }
}
