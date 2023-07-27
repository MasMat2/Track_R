using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioLocacionController : ControllerBase
    {
        private UsuarioLocacionService usuarioLocacionService;

        public UsuarioLocacionController(UsuarioLocacionService usuarioLocacionService)
        {
            this.usuarioLocacionService = usuarioLocacionService;
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(UsuarioLocacion usuarioLocacion)
        {
            usuarioLocacionService.Agregar(usuarioLocacion);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(UsuarioLocacion usuarioLocacion)
        {
            usuarioLocacionService.Editar(usuarioLocacion);
        }

        [HttpDelete]
        [Route("eliminar/{idUsuarioLocacion}")]
        public void Eliminar(int idUsuarioLocacion)
        {
            usuarioLocacionService.Eliminar(idUsuarioLocacion);
        }

         [HttpGet]
         [Route("consultarPorUsuario/{idUsuario}")]
         public IEnumerable<UsuarioLocacionDto> ConsultarPorUsuario(int idUsuario)
         {
             return usuarioLocacionService.ConsultarPorUsuario(idUsuario);
         }

    }
}
