using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Services.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Dtos.GestionExamen;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class ReactivoController : ControllerBase
{
    private readonly ReactivoService _reactivoService;

    public ReactivoController(ReactivoService reactivoService)
    {
        _reactivoService = reactivoService;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector")]
    public IEnumerable<ReactivoGridDto> ConsultarTodosParaSelector()
    {
        return _reactivoService.ConsultarTodosParaSelector();
    }

    [HttpGet]
    [Route("consultarGeneral")]
    public IEnumerable<ReactivoGridDto> ConsultarGeneral()
    {
        return _reactivoService.ConsultarGeneral();
    }

    [HttpGet]
    [Route("consultar/{idReactivo}")]
    public ReactivoDto? Consultar(int idReactivo)
    {
        return _reactivoService.Consultar(idReactivo);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(Reactivo reactivo)
    {
        return _reactivoService.Agregar(reactivo);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(Reactivo reactivo)
    {
        _reactivoService.Editar(reactivo);
    }

    [HttpDelete]
    [Route("eliminar/{idReactivo}")]
    public void Eliminar(int idReactivo)
    {
        _reactivoService.Eliminar(idReactivo);
    }
}
