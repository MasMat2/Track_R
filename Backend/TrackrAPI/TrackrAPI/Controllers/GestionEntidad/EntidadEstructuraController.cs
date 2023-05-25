﻿using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;

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
    }
}
