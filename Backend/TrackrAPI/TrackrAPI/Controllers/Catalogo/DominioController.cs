using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DominioController: ControllerBase
    {
        private DominioService dominioService;

        public DominioController(DominioService dominioService)
        {
            this.dominioService = dominioService;
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<DominioGridDto> ConsultarTodosParaGrid()
        {
            int idUsuarioSesion = Utileria.ObtenerIdUsuarioSesion(this);
            return dominioService.ConsultarTodosParaGrid(idUsuarioSesion);
        }

        [HttpGet]
        [Route("consultar/{idDominio}")]
        public DominioDto Consultar(int idDominio)
        {
            return dominioService.ConsultarDto(idDominio);
        }

        [HttpGet]
        [Route("consultarPorNombre/{nombre}")]
        public Dominio ConsultarPorNombre(string nombre)
        {
            return dominioService.Consultar(nombre);
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<DominioDto> consultarTodosParaSelector()
        {
            return dominioService.consultarTodosParaSelector();
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(Dominio dominio)
        {
            return dominioService.Agregar(dominio);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Dominio dominio)
        {
            dominioService.Editar(dominio);
        }

        [HttpDelete]
        [Route("eliminar/{idDominio}")]
        public void Eliminar(int idDominio)
        {
            dominioService.Eliminar(idDominio);
        }
    }
}
