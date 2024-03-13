using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using TrackrAPI.Services.Seguridad;
using TrackrAPI.Helpers;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private MonedaService monedaService;
        private UsuarioService usuarioService;

        public MonedaController(MonedaService monedaService, UsuarioService usuarioService)
        {
            this.monedaService = monedaService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<MonedaGridDto> ConsultarTodosParaGrid()
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            int idCompania = (int)usuario.IdCompania;
            return monedaService.ConsultarTodosParaGrid(idCompania);
        }

        [HttpGet]
        [Route("consultarParaSelector")]
        public IEnumerable<MonedaSelectorDto> ConsultarParaSelector()
        {
            return monedaService.ConsultarParaSelector();
        }


        [HttpGet]
        [Route("consultarPorId/{idMoneda}")]
        public MonedaDto Consultar(int idMoneda)
        {
            return monedaService.ConsultarDto(idMoneda);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(Moneda moneda)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            return monedaService.Agregar(moneda);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Moneda moneda)
        {
            Usuario usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));
            monedaService.Editar(moneda);
        }

        [HttpDelete]
        [Route("eliminar/{idMoneda}")]
        public void Eliminar(int idMoneda)
        {
            monedaService.Eliminar(idMoneda);
        }
    }
}
