using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Services.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Controllers.Catalogo
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly EspecialidadService especialidadService;
        private EspecialidadFormularioCapturaDto especialidad;

        [HttpGet("formulario/{idEspecialidad}")]
        public EspecialidadFormularioConsultaDto? ConsultarParaFormulario(int idEspecialidad)
        {
            return especialidadService.ConsultarParaFormulario(idEspecialidad);
        }

        [HttpGet("grid")]
        public IEnumerable<EspecialidadGridDto> Consultar()
        {
            return especialidadService.ConsultarParaGrid();
        }

         [HttpPost]
        public void Agregar(EspecialidadFormularioCapturaDto especialidadDto)
        {
            especialidadService.Agregar(especialidad);
        }


        [HttpPut]
        public void Editar(EspecialidadFormularioCapturaDto especialidadDto)
        {
            especialidadService.Editar(especialidad);
        }

        [HttpDelete("{idEspecialidad}")]
        public void Eliminar(int idEspecialidad)
        {
            especialidadService.Eliminar(idEspecialidad);
        }
    }
}