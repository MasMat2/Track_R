using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionEntidad;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.GestionEntidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadEstructuraValorController : ControllerBase
    {
        private readonly EntidadEstructuraValorService entidadEstructuraValorService;

        public EntidadEstructuraValorController(EntidadEstructuraValorService entidadEstructuraValorService)
        {
            this.entidadEstructuraValorService = entidadEstructuraValorService;
        }

        [HttpGet]
        [Route("consultarPorTabulacion/{idEntidadEstructura},{idTabla}")]
        public IEnumerable<EntidadEstructuraValorDto> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            return entidadEstructuraValorService.ConsultarPorTabulacion(idEntidadEstructura, idTabla);
        }

        [HttpPost]
        [Route("guardar")]
        public void Guardar(List<EntidadEstructuraValor> entidadEstructuraValorList)
        {
            entidadEstructuraValorService.Guardar(entidadEstructuraValorList);
        }
    }
}