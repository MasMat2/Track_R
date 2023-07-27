using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.GestionEntidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadController : ControllerBase
    {
        private readonly EntidadService entidadService;

        public EntidadController(EntidadService entidadService)
        {
            this.entidadService = entidadService;
        }

        [HttpGet]
        [Route("consultar/{idEntidad}")]
        public EntidadDto Consultar(int idEntidad)
        {
            return entidadService.ConsultarDto(idEntidad);
        }

        [HttpGet]
        [Route("consultarPorClave/{clave}")]
        public Entidad ConsultarPorClave(string clave)
        {
            return entidadService.ConsultarPorClave(clave);
        }

        [HttpGet]
        [Route("consultarTodosParaGrid")]
        public IEnumerable<EntidadGridDto> ConsultarTodosParaGrid()
        {
            return entidadService.ConsultarTodosParaGrid();
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(Entidad entidad)
        {
            return entidadService.Agregar(entidad);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Entidad entidad)
        {
            entidadService.Editar(entidad);
        }

        [HttpDelete]
        [Route("eliminar/{idEntidad}")]
        public void Eliminar(int idEntidad)
        {
            entidadService.Eliminar(idEntidad);
        }

        [HttpPost]
        [Route("actualizarExpedienteTrackr")]
        public void ActualizarExpedienteTrackr()
        {
            entidadService.ActualizarExpedienteTrackr();
        }
    }
}