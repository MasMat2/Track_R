
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Services.GestionExamen;

namespace TrackrAPI.Controllers.GestionExamen;
[Route("api/[controller]")]
[ApiController]

public class RespuestasClasificacionPreguntaController:ControllerBase

{
    private readonly RespuestasClasificacionPreguntaService _respuestasClasificacionPreguntaService;

    public RespuestasClasificacionPreguntaController(RespuestasClasificacionPreguntaService respuestasClasificacionPreguntaService)
    {
        _respuestasClasificacionPreguntaService = respuestasClasificacionPreguntaService;
    }

    [HttpGet("grid/{idClasificacionPregunta}")]
    public IEnumerable<RespuestasClasificacionPreguntaGridDto> ConsultarParaGrid(int idClasificacionPregunta)
    {
        return _respuestasClasificacionPreguntaService.ConsultarParaGrid(idClasificacionPregunta);
    }
    [HttpGet("{idRespuestasClasificacionPregunta}")]
    public RespuestasClasificacionPreguntaInformacionGeneralDto ConsultarParaFormulario(int idRespuestasClasificacionPregunta)
    {
        return _respuestasClasificacionPreguntaService.ConsultarParaFormulario(idRespuestasClasificacionPregunta);
    }


    [HttpPost]
    [Route("agregar")]
    public void Agregar(RespuestasClasificacionPreguntaFormularioDto captura)
    {
        _respuestasClasificacionPreguntaService.Agregar(captura);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(RespuestasClasificacionPreguntaFormularioDto captura)
    {
        _respuestasClasificacionPreguntaService.Editar(captura);
    }

    [HttpDelete]
    [Route("{idRespuestasClasificacionPregunta}")]
    public void Eliminar(int idRespuestasClasificacionPregunta)
    {
            _respuestasClasificacionPreguntaService.Eliminar(idRespuestasClasificacionPregunta);
    }
}