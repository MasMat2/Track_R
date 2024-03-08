using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private UsuarioRolService usuarioRolService;

        public UsuarioRolController(UsuarioRolService usuarioRolService)
        {
            this.usuarioRolService = usuarioRolService;
        }

        [HttpGet]
        [Route("consultarPorUsuario/{idUsuario}")]
        public IEnumerable<UsuarioRolDto> ConsultarPorUsuario(int idUsuario)
        {
            return usuarioRolService.ConsultarPorUsuario(idUsuario);
        }

        [HttpGet]
        [Route("consultarPorUsuarioParaGrid/{idUsuario}")]
        public IEnumerable<UsuarioRolGridDto> ConsultarPorUsuarioParaGrid(int idUsuario)
        {
            return usuarioRolService.ConsultarPorUsuarioParaGrid(idUsuario);
        }

        [HttpPost]
        [Route("guardar")]
        public void Guardar(List<UsuarioRol> usuarioRolList)
        {
            usuarioRolService.Guardar(usuarioRolList);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(UsuarioRol usuarioRol)
        {
            usuarioRolService.Agregar(usuarioRol);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(UsuarioRol usuarioRol)
        {
            usuarioRolService.Editar(usuarioRol);
        }

        [HttpDelete]
        [Route("eliminar/{idUsuarioRol}")]

        public void Eliminar(int idUsuarioRol)
        {
            usuarioRolService.Eliminar(idUsuarioRol);
        }
    }
}
