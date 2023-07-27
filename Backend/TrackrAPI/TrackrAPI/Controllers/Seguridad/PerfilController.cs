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
    public class PerfilController : ControllerBase
    {
        private PerfilService perfilService;
        private UsuarioService usuarioService;

        public PerfilController(PerfilService perfilService, UsuarioService usuarioService)
        {
            this.perfilService = perfilService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultar/{idPerfil}")]
        public PerfilDto Consultar(int idPerfil)
        {
            return perfilService.Consultar(idPerfil);
        }

        [HttpGet]
        [Route("consultarPorCompania")]
        public IEnumerable<PerfilDto> ConsultarGeneral()
        {
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            var usuario = usuarioService.Consultar(idUsuario);

            int IdCompania = (int)usuario.IdCompania;

            return perfilService.ConsultarGeneral(IdCompania);
        }

        [HttpGet]
        [Route("consultarPorCompaniaParaSelector/{idCompania}")]
        public IEnumerable<Perfil> ConsultarPorCompaniaParaSelector(int idCompania)
        {
            return perfilService.ConsultarPorCompaniaBase(idCompania);
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<PerfilDto> ConsultarTodosParaSelector()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;

            return perfilService.ConsultarTodosParaSelector(idCompania);
        }

        [HttpPost]
        [Route("agregar")]
        public PerfilDto Agregar(PerfilDto perfil)
        {
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            var usuario = usuarioService.Consultar(idUsuario);

            perfil.IdCompania = (int)usuario.IdCompania;
            perfilService.Agregar(perfil);
            return perfil;
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(PerfilDto perfil)
        {
            var idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            var usuario = usuarioService.Consultar(idUsuario);

            perfil.IdCompania = (int)usuario.IdCompania;

            perfilService.Editar(perfil);
        }

        [HttpDelete]
        [Route("eliminar/{idPerfil}")]
        public void Eliminar(int idPerfil)
        {
            perfilService.Eliminar(idPerfil);
        }

    }
}