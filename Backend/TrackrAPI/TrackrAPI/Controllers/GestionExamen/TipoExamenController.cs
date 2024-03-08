using TrackrAPI.Services.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Dtos.GestionExamen;
using Microsoft.AspNetCore.Mvc;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class TipoExamenController : ControllerBase
{
    private readonly TipoExamenService _tipoExamenService;

    public TipoExamenController(TipoExamenService tipoExamenService)
    {
        _tipoExamenService = tipoExamenService;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector")]
    public IEnumerable<TipoExamenGridDto> ConsultarTodosParaSelector()
    {
        return _tipoExamenService.ConsultarTodosParaSelector();
    }

    [HttpGet]
    [Route("consultarGeneral")]
    public IEnumerable<TipoExamenGridDto> ConsultarGeneral()
    {
        return _tipoExamenService.ConsultarGeneral();
    }

    [HttpGet]
    [Route("consultar/{idTipoExamen}")]
    public TipoExamenDto? Consultar(int idTipoExamen)
    {
        return _tipoExamenService.Consultar(idTipoExamen);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(TipoExamen tipoExamen)
    {
        return _tipoExamenService.Agregar(tipoExamen);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(TipoExamen tipoExamen)
    {
        _tipoExamenService.Editar(tipoExamen);
    }

    [HttpDelete]
    [Route("eliminar/{idTipoExamen}")]
    public void Eliminar(int idTipoExamen)
    {
        _tipoExamenService.Eliminar(idTipoExamen);
    }
}
