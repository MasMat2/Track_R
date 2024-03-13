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
        private readonly EstadoService estadoService;

        public EstadoController(EstadoService estadoService)
        {
            this.estadoService = estadoService;
        }

        [HttpGet("grid")]
        public IEnumerable<EstadoGridDto> ConsultarParaGrid()
        {
            return estadoService.ConsultarParaGrid();
        }

        [HttpGet("selector/pais/{idPais}")]
        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            return estadoService.ConsultarPorPaisParaSelector(idPais);
        }

        [HttpGet("{idEstado}")]
        public EstadoDto? Consultar(int idEstado)
        {
            return estadoService.Consultar(idEstado);
        }

        [HttpGet("formulario/{idEstado}")]
        public EstadoFormularioConsultaDto? ConsultarParaFormulario(int idEstado)
        {
            return estadoService.ConsultarParaFormulario(idEstado);
        }

        [HttpPost]
        public void Agregar(EstadoFormularioCapturaDto estado)
        {
            estadoService.Agregar(estado);
        }

        [HttpPut]
        public void Editar(EstadoFormularioCapturaDto estado)
        {
            estadoService.Editar(estado);
        }

        [HttpDelete("{idEstado}")]
        public void Eliminar(int idEstado)
        {
            estadoService.Eliminar(idEstado);
        }
    }
}