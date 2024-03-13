using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Services.Contabilidad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Contabilidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgrupadorCuentaContableController : ControllerBase
    {
        private AgrupadorCuentaContableService agrupadorCuentaContableService;

        public AgrupadorCuentaContableController(AgrupadorCuentaContableService agrupadorCuentaContableService)
        {
            this.agrupadorCuentaContableService = agrupadorCuentaContableService;
        }

        [HttpGet("consultarParaGrid")]
        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaGrid()
        {
            return agrupadorCuentaContableService.ConsultarParaGrid();
        }

        [HttpGet("consultarParaSelector")]
        public IEnumerable<AgrupadorCuentaContableDto> ConsultarParaSelector()
        {
            return agrupadorCuentaContableService.ConsultarParaSelector();
        }

        [HttpGet("consultarDto/{idAgrupador}")]
        public AgrupadorCuentaContableDto ConsutlarDto(int idAgrupador)
        {
            return agrupadorCuentaContableService.ConsultarDto(idAgrupador);
        }

        [HttpPost("agregar")]
        public void Agregar(AgrupadorCuentaContableDto agrupador)
        {
            agrupadorCuentaContableService.Agregar(agrupador);
        }

        [HttpPut("editar")]
        public void Editar(AgrupadorCuentaContableDto agrupador)
        {
            agrupadorCuentaContableService.Editar(agrupador);
        }

        [HttpDelete("eliminar/{idAgrupador}")]
        public void Delete(int idAgrupador)
        {
            agrupadorCuentaContableService.Eliminar(idAgrupador);
        }
    }
}
