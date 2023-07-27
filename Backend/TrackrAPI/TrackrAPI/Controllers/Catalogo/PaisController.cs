using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private PaisService paisService;

        public PaisController(PaisService paisService)
        {
            this.paisService = paisService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<PaisDto> ConsultarTodosParaSelector()
        {
            return paisService.ConsultarTodosParaSelector();
        }

        [HttpGet]
        [Route("consultarPorClave/{clave}")]
        public PaisDto consultarPorClave(string clave)
        {
            return paisService.ConsultarDto(clave);
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<PaisGridDto> ConsultarGeneral()
        {
            return paisService.ConsultarGeneral();
        }

        [HttpGet]
        [Route("consultar/{idPais}")]
        public PaisDto Consultar(int idPais)
        {
            return paisService.ConsultarDto(idPais);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Pais pais)
        {
            paisService.Agregar(pais);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Pais pais)
        {
            paisService.Editar(pais);
        }

        [HttpDelete]
        [Route("eliminar/{idPaisDto}")]
        public void Eliminar(int idPaisDto)
        {
            paisService.Eliminar(idPaisDto);
        }
    }
}