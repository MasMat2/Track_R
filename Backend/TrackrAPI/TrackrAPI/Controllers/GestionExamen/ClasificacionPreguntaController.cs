using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.General;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Services.GestionExamen;

namespace TrackrAPI.Controllers.GestionExamen;
    [Route("api/[controller]")]
    [ApiController]

    public class ClasificacionPreguntaController : ControllerBase
    {
        private readonly ClasificacionPreguntaService _clasificacionPreguntaService;

        public ClasificacionPreguntaController(ClasificacionPreguntaService clasificacionPreguntaService)
        {
            _clasificacionPreguntaService = clasificacionPreguntaService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ClasificacionPreguntaGridDto> ConsultarParaGrid()
        {
            return _clasificacionPreguntaService.ConsultarParaGrid();
        }

        [HttpGet]
        [Route("{idClasificacionPregunta}")]
        public ClasificacionPreguntaFormularioDto Consultar(int idClasificacionPregunta)
        {
            return _clasificacionPreguntaService.Consultar(idClasificacionPregunta);
        }

        [HttpPost]
        public void Agregar(ClasificacionPreguntaFormularioDto dto)
        {
            _clasificacionPreguntaService.Agregar(dto);
        }

        [HttpPut]
        public void Editar(ClasificacionPreguntaFormularioDto dto)
        {
            _clasificacionPreguntaService.Editar(dto);
        }

        [HttpDelete]
        [Route("{idClasificacionPregunta}")]
        public void Eliminar(int idClasificacionPregunta)
        {
            _clasificacionPreguntaService.Eliminar(idClasificacionPregunta);
        }

        [HttpGet]
        [Route("consultarSelector")]
        public IEnumerable<SimpleSelectorDto> ConsultarTodosParaSelector()
        {
            return _clasificacionPreguntaService.ConsultarTodosParaSelector();
        }
    }