using TrackrAPI.Services.Examen;
using TrackrAPI.Models;
using TrackrAPI.Dtos.Examen;
using Microsoft.AspNetCore.Mvc;

namespace TrackrAPI.Controllers.Examen;

[Route("api/[controller]")]
[ApiController]
public class ContenidoExamenController : ControllerBase
{
    private readonly ContenidoExamenService _contenidoExamenService;

    public ContenidoExamenController(ContenidoExamenService contenidoExamenService)
    {
        _contenidoExamenService = contenidoExamenService;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector")]
    public IEnumerable<ContenidoExamenGridDto> ConsultarTodosParaSelector()
    {
        return _contenidoExamenService.ConsultarTodosParaSelector();
    }

    [HttpGet]
    [Route("consultarGeneral/{idTipoExamen}")]
    public IEnumerable<ContenidoExamenGridDto> ConsultarGeneral(int idTipoExamen)
    {
        return _contenidoExamenService.ConsultarGeneral(idTipoExamen);
    }

    [HttpGet]
    [Route("consultar/{idContenidoExamen}")]
    public ContenidoExamenDto Consultar(int idContenidoExamen)
    {
        return _contenidoExamenService.Consultar(idContenidoExamen);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(ContenidoExamen contenidoExamen)
    {
        return _contenidoExamenService.Agregar(contenidoExamen);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(ContenidoExamen contenidoExamen)
    {
        _contenidoExamenService.Editar(contenidoExamen);
    }

    [HttpDelete]
    [Route("eliminar/{idContenidoExamen}")]
    public void Eliminar(int idContenidoExamen)
    {
        _contenidoExamenService.Eliminar(idContenidoExamen);
    }
}
