using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Services.Examen;
using TrackrAPI.Models;
using TrackrAPI.Dtos.Examen;

namespace TrackrAPI.Controllers.Examen
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        private readonly AsignaturaService _asignaturaService;

        public AsignaturaController(AsignaturaService asignaturaService)
        {
            _asignaturaService = asignaturaService;
        }

        [HttpGet]
        [Route("consultarTodosParaSelector")]
        public IEnumerable<AsignaturaGridDto> ConsultarTodosParaSelector()
        {
            return _asignaturaService.ConsultarTodosParaSelector();
        }

        [HttpGet]
        [Route("consultarGeneral")]
        public IEnumerable<AsignaturaGridDto> ConsultarGeneral()
        {
            return _asignaturaService.ConsultarGeneral();
        }

        [HttpGet]
        [Route("consultar/{idAsignatura}")]
        public AsignaturaDto? Consultar(int idAsignatura)
        {
            return _asignaturaService.Consultar(idAsignatura);
        }

        [HttpPost]
        [Route("agregar")]
        public int Agregar(Asignatura asignatura)
        {
            return _asignaturaService.Agregar(asignatura);
        }

        [HttpPut]
        [Route("editar")]
        public void Editar(Asignatura asignatura)
        {
            _asignaturaService.Editar(asignatura);
        }

        [HttpDelete]
        [Route("eliminar/{idAsignatura}")]
        public void Eliminar(int idAsignatura)
        {
            _asignaturaService.Eliminar(idAsignatura);
        }
    }
}
