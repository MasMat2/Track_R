using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoAyudaController : ControllerBase
    {
        private AccesoAyudaService accesoAyudaService;

        public AccesoAyudaController(AccesoAyudaService accesoAyudaService)
        {
            this.accesoAyudaService = accesoAyudaService;
        }

        [HttpGet]
        [Route("consultar/{idAccesoAyuda}")]
        public AccesoAyudaDto Consultar(int idAccesoAyuda)
        {
            return accesoAyudaService.ConsultarDto(idAccesoAyuda);
        }
        [HttpGet]
        [Route("consultarPorAcceso/{idAcceso}")]
        public IEnumerable<AccesoAyudaDto> ConsultarPorAcceso(int idAcceso)
        {
            return accesoAyudaService.ConsultarPorAcceso(idAcceso);
        }
        [HttpGet]
        [Route("consultarPorAccesoPorSeccion/{idAcceso}")]
        public IEnumerable<AccesoAyudaSeccionDto> ConsultarPorAccesoPorSeccion(int idAcceso)
        {
            return accesoAyudaService.ConsultarPorAccesoPorSeccion(idAcceso);
        }
        [HttpPost]
        [Route("agregar")]
        public void Agregar(AccesoAyuda accesoAyuda)
        {
            accesoAyudaService.Agregar(accesoAyuda);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(AccesoAyuda accesoAyuda)
        {
            accesoAyudaService.Editar(accesoAyuda);
        }

        [HttpDelete]
        [Route("eliminar/{idAccesoAyuda}")]
        public void Eliminar(int idAccesoAyuda)
        {
            accesoAyudaService.Eliminar(idAccesoAyuda);
        }
    }
}
