using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.GestionEntidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionController : ControllerBase
    {
        private readonly SeccionService seccionService;

        public SeccionController(SeccionService seccionService)
        {
            this.seccionService = seccionService;
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<SeccionGridDto> ConsultarGeneral()
        {
            return seccionService.ConsultarGeneral();
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<SeccionDto> consultarTodosParaSelector()
        {
            return seccionService.ConsultarTodosParaSelector();
        }

        [HttpGet]
        [Route("consultar/{idSeccion}")]
        public Seccion Consultar(int idSeccion)
        {
            return seccionService.Consultar(idSeccion);
        }

        [HttpPost]
        [Route("agregar")]
        public void Agregar(Seccion seccion)
        {
            seccionService.Agregar(seccion);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Seccion seccion)
        {
            seccionService.Editar(seccion);
        }

        [HttpDelete]
        [Route("eliminar/{idSeccion}")]
        public void Eliminar(int idSeccion)
        {
            seccionService.Eliminar(idSeccion);
        }
    }
}