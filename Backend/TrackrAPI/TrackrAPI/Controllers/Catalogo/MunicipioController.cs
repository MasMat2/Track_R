using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private MunicipioService municipioService;

        public MunicipioController(MunicipioService municipioService) {
            this.municipioService = municipioService;
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<MunicipioGridDto> ConsultarTodosParaGrid()
        {
            return municipioService.ConsultarTodosParaGrid();
        }

        [HttpGet]
        [Route("consultarPorEstadoParaSelector/{idEstado}")]
        public IEnumerable<MunicipioDto> ConsultarPorEstadoParaSelector(int idEstado)
        {
            return municipioService.ConsultarPorEstadoParaSelector(idEstado);
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<MunicipioDto> ConsultarTodosParaSelector()
        {
            return municipioService.ConsultarTodosParaSelector();
        }

        [HttpGet]
        [Route("consultar/{idMunicipio}")]
        public MunicipioDto Consultar(int idMunicipio)
        {
            return municipioService.ConsultarDto(idMunicipio);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Municipio municipio)
        {
            municipioService.Agregar(municipio);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Municipio municipio)
        {
            municipioService.Editar(municipio);
        }

        [HttpDelete]
        [Route("eliminar/{idMunicipio}")]
        public void Eliminar(int idMunicipio)
        {
            municipioService.Eliminar(idMunicipio);
        }
    }
}
