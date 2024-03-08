using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Controllers.Catalogo
{

    [Route("api/[controller]")]
    [ApiController]
    public class PuntoVentaController: ControllerBase
    {
        private PuntoVentaService puntoVentaService;
        private UsuarioService usuarioService;

        public PuntoVentaController(PuntoVentaService puntoVentaService,
            UsuarioService usuarioService)
        {
            this.puntoVentaService = puntoVentaService;
            this.usuarioService = usuarioService;
        }


        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<PuntoVentaGridDto> ConsultarTodosParaGrid()
        {
            var usuarioSesion = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return puntoVentaService.ConsultarTodosParaGrid((int)usuarioSesion.IdCompania);
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<PuntoVentaDto> ConsultarTodosSelectorGrid()
        {
            var usuarioSesion = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return puntoVentaService.ConsultarTodosParaSelector((int)usuarioSesion.IdCompania);
        }

        [HttpGet]
        [Route("consultarPorUsuarioEnSesion")]
        public PuntoVentaDto ConsultarPorUsuarioEnSesion()
        {
            var usuarioSesion = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return puntoVentaService.consultarPorUsuarioEnSesion(usuarioSesion);
        }


        [HttpGet]
        [Route("consultar/{idPuntoVenta}")]
        public PuntoVentaDto Consultar(int idPuntoVenta)
        {
            var usuarioSesion = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return puntoVentaService.ConsultarDto(idPuntoVenta, usuarioSesion);
        }

        [HttpGet]
        [Route("consultarUsuariosAsignados/{idPuntoVenta}")]
        public IEnumerable<UsuarioDto> ConsultarUsuariosAsignados(int idPuntoVenta)
        {
            return puntoVentaService.ConsultarUsuariosAsignados(idPuntoVenta);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(PuntoVenta tipoPresentacion)
        {
            puntoVentaService.Agregar(tipoPresentacion);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(PuntoVenta tipoPresentacion)
        {
            puntoVentaService.Editar(tipoPresentacion);
        }

        [HttpDelete]
        [Route("eliminar/{idPuntoVenta}")]
        public void Eliminar(int idPuntoVenta)
        {
            puntoVentaService.Eliminar(idPuntoVenta);
        }





















    }
}
