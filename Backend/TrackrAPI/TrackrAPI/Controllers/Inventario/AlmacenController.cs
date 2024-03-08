using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Inventario;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Inventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        private AlmacenService almacenService;
        private UsuarioService usuarioService;

        public AlmacenController(AlmacenService almacenService,
            UsuarioService usuarioService)
        {
            this.almacenService = almacenService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<AlmacenGridDto> ConsultarGeneral()
        {
            return almacenService.ConsultarGeneral(Utileria.ObtenerIdUsuarioSesion(this));
        }

        [HttpGet]
        [Route("consultar/{idAlmacen}")]
        public AlmacenDto Consultar(int idAlmacen)
        {
            return almacenService.ConsultarDto(idAlmacen);
        }


        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<AlmacenDto> ConsultarTodosSelectorGrid()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return almacenService.ConsultarTodosParaSelector((int)usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultarPorEstado/{idEstado}")]
        public IEnumerable<AlmacenGridDto> ConsultarPorEstado(int idEstado)
        {
            return almacenService.ConsultarPorEstado(idEstado);
        }

        [HttpGet]
        [Route("consultarPorUsuario/{idUsuario}")]
        public IEnumerable<AlmacenGridDto> ConsultarPorUsuario(int idUsuario)
        {
            return almacenService.ConsultarPorUsuario(idUsuario); ;
        }

        [HttpGet]
        [Route("consultarPorUsuario/{idEstatus}")]
        public IEnumerable<AlmacenGridDto> ConsultarPorEstatus(int idEstatus)
        {
            return almacenService.ConsultarPorEstatus(idEstatus);
        }

        [HttpGet]
        [Route("consultarPorCompania")]
        public IEnumerable<AlmacenDto> ConsultarPorCompania()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            return almacenService.ConsultarPorCompania((int)usuario.IdCompania, usuario.IdUsuario);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(Almacen almacen)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            almacen.IdCompania = (int)usuario.IdCompania;

            return almacenService.Agregar(almacen);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Almacen almacen)
        {
            almacenService.Editar(almacen);
        }

        [HttpDelete]
        [Route("eliminar/{idAlmacen}")]
        public void Eliminar(int idAlmacen)
        {
            almacenService.Eliminar(idAlmacen);
        }
    }
}