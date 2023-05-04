using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private EstadoService estadoService;

        public EstadoController(EstadoService estadoService)
        {
            this.estadoService = estadoService;
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<EstadoGridDto> ConsultarTodosParaGrid()
        {
            return estadoService.ConsultarTodosParaGrid();
        }

        [HttpGet]
        [Route("consultarPorPaisParaSelector/{idPais}")]
        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            return estadoService.ConsultarPorPaisParaSelector(idPais);
        }

        [HttpGet]
        [Route("consultar/{idEstado}")]
        public EstadoDto Consultar(int idEstado)
        {
            return estadoService.ConsultarDto(idEstado);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Estado estado)
        {
            estadoService.Agregar(estado);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Estado estado)
        {
            estadoService.Editar(estado);
        }

        [HttpDelete]
        [Route("eliminar/{idEstado}")]
        public void Eliminar(int idEstado)
        {
            estadoService.Eliminar(idEstado);
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<EstadoSelectorDto> ConsultarGeneral()
        {
            return estadoService.ConsultarGeneral();
        }

    }
}