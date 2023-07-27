using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DominioDetalleController : ControllerBase
    {
        private DominioDetalleService dominioDetalleService;
        private UsuarioService usuarioService;

        public DominioDetalleController(DominioDetalleService dominioDetalleService,
            UsuarioService usuarioService)
        {
            this.dominioDetalleService = dominioDetalleService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarPorDominio/{idDominio}")]
        public IEnumerable<DominioDetalleGridDto> ConsultarPorDominio(int idDominio)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return dominioDetalleService.ConsultarPorDominio(idDominio, (int)usuario.IdCompania);
        }

        [HttpGet]
        [Route("consultar/{idDominioDetalle}")]
        public DominioDetalleDto Consultar(int idDominioDetalle)
        {
            return dominioDetalleService.ConsultarDto(idDominioDetalle);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(DominioDetalle dominioDetalle)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            dominioDetalle.IdCompania = usuario.IdCompania;
            dominioDetalleService.Agregar(dominioDetalle);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(DominioDetalle dominioDetalle)
        {
            dominioDetalleService.Editar(dominioDetalle);
        }

        [HttpDelete]
        [Route("eliminar/{idDominioDetalle}")]
        public void Eliminar(int idDominioDetalle)
        {
            dominioDetalleService.Eliminar(idDominioDetalle);
        }
    }
}
