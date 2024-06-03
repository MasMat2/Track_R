using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionExamen;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class RespuestaController : ControllerBase
{
    private readonly RespuestaService _respuestaService;

    public RespuestaController(RespuestaService respuestaService)
    {
        _respuestaService = respuestaService;
    }

    [HttpGet]
    [Route("consultarTodosPorReactivo/{idReactivo}")]
    public IEnumerable<RespuestaDto> ConsultarTodosPorReactivo(int idReactivo)
    {
        return _respuestaService.ConsultarTodosPorReactivo(idReactivo);
    }

    [HttpGet]
    [Route("consultar/{idRespuesta}")]
    public RespuestaDto? ConsultarParaFormulario(int idRespuesta)
    {
        return _respuestaService.ConsultarParaFormulario(idRespuesta);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(Respuesta respuesta)
    {
        return _respuestaService.Agregar(respuesta);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(Respuesta respuesta)
    {
        _respuestaService.Editar(respuesta);
    }

    [HttpDelete]
    [Route("eliminar/{idRespuesta}")]
    public void Eliminar(int idRespuesta)
    {
        _respuestaService.Eliminar(idRespuesta);
    }
}
