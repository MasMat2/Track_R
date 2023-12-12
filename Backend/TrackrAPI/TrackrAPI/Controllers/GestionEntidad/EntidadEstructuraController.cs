using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Controllers.GestionEntidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadEstructuraController : ControllerBase
    {
        private readonly EntidadEstructuraService entidadEstructuraService;
        public EntidadEstructuraController(EntidadEstructuraService entidadEstructuraService)
        {
            this.entidadEstructuraService = entidadEstructuraService;
        }

        [HttpGet("consultarArbol/{idEntidad}")]
        public IEnumerable<EntidadEstructuraDto> ConsultarArbol(int idEntidad)
        {
            var arbol = entidadEstructuraService.ConsultarArbol(idEntidad);

            foreach (var pestana in arbol)
            {
                pestana.Nombre = pestana.Clave + " - " + pestana.Nombre;
            }

            return arbol;
        }

        [HttpGet("tabulador/{idEntidad}")]
        public IEnumerable<EntidadEstructuraDto> ConsultarParaTabulador(int idEntidad)
        {
            var arbol = entidadEstructuraService.ConsultarParaTabulador(idEntidad);
            return arbol;
        }

        [HttpGet("consultarPorEntidadParaSelector/{idEntidad}")]
        public IEnumerable<EntidadEstructuraDto> ConsultarPorJerarquiaParaSelector(int idEntidad)
        {
            return entidadEstructuraService.ConsultarPorEntidadParaSelector(idEntidad);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(List<EntidadEstructuraDto> entidadEstructuras)
        {
            entidadEstructuraService.Agregar(entidadEstructuras);
        }

        [HttpDelete]
        [Route("eliminar/{idEntidadEstructura}")]
        public void Eliminar(int idEntidadEstructura)
        {
            entidadEstructuraService.Eliminar(idEntidadEstructura);
        }

        [HttpGet("consultarPadecimientosParaSelector")]
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarPadecimientosParaSelector()
        {
            return entidadEstructuraService.ConsultarPadecimientosParaSelector();
        }

        [HttpGet("antecedentes/selector")]
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarAntecedentesParaSelector()
        {
            return entidadEstructuraService.ConsultarAntecedentesParaSelector();
        }

        [HttpGet("diagnosticos/selector")]
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarDiagnosticosParaSelector()
        {
            return entidadEstructuraService.ConsultarDiagnosticosParaSelector();
        }
    }
}
