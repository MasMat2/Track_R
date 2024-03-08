using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.CanalDistribucion;
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
    public class ListaPrecioController : ControllerBase
    {
        private ListaPrecioService listaPrecioService;
        private UsuarioService usuarioService;

        public ListaPrecioController(ListaPrecioService listaPrecioService,
            UsuarioService usuarioService)
        {
            this.listaPrecioService = listaPrecioService;
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<ListaPrecioDto> ConsultarTodosParaSelector()
        {
            return listaPrecioService.ConsultarTodosParaSelector();
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<ListaPrecioGridDto> ConsultarTodosParaGrid()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            return listaPrecioService.ConsultarTodosPorHospitalParaGrid((int)usuario.IdHospital);
        }

        [HttpGet]
        [Route("consultarTodosPorHospitalParaSelector")]
        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaSelector()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            return listaPrecioService.ConsultarTodosPorHospitalParaSelector((int)usuario.IdHospital);
        }

        [HttpGet]
        [Route("consultar/{idListaPrecios}")]
        public ListaPrecioDto Consultar(int idListaPrecios)
        {
            return listaPrecioService.ConsultarDto(idListaPrecios);
        }

        [HttpGet]
        [Route("consultarVigente")]
        public IEnumerable<ListaPrecioDto> ConsultarVigente()
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            return listaPrecioService.ConsultarVigente((int)usuario.IdHospital);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(ListaPrecio listaPrecio)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            return listaPrecioService.Agregar(listaPrecio, (int)usuario.IdHospital);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(ListaPrecio listaPrecio)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            listaPrecioService.Editar(listaPrecio, (int)usuario.IdHospital);
        }

        [HttpDelete]
        [Route("eliminar/{idListaPrecio}")]
        public void Eliminar(int idListaPrecio)
        {
            listaPrecioService.Eliminar(idListaPrecio);
        }

        [HttpGet]
        [Route("consultarPorPresentacion/{idPresentacion}")]
        public ListaPrecioDetalle ConsultarPorPresentacion(int idPresentacion)
        {
            return listaPrecioService.ConsultarPorPresentacion(idPresentacion);
        }

        [HttpPost]
        [Route("copiar")]
        public void Copiar(dynamic listaPrecio)
        {
            var usuario = usuarioService.Consultar(Utileria.ObtenerIdUsuarioSesion(this));

            listaPrecioService.Copiar(listaPrecio, (int)usuario.IdHospital);
        }
    }
}
