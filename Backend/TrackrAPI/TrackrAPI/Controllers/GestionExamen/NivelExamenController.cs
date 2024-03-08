using TrackrAPI.Services.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Dtos.GestionExamen;
using Microsoft.AspNetCore.Mvc;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class NivelExamenController : ControllerBase
{
    private readonly NivelExamenService _nivelExamenService;

    public NivelExamenController(NivelExamenService nivelExamenService)
    {
        _nivelExamenService = nivelExamenService;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector")]
    public IEnumerable<NivelExamenGridDto> ConsultarTodosParaSelector()
    {
        return _nivelExamenService.ConsultarTodosParaSelector();
    }

    [HttpGet]
    [Route("consultarGeneral")]
    public IEnumerable<NivelExamenGridDto> ConsultarGeneral()
    {
        return _nivelExamenService.ConsultarGeneral();
    }

    [HttpGet]
    [Route("consultar/{idNivelExamen}")]
    public NivelExamenDto? Consultar(int idNivelExamen)
    {
        return _nivelExamenService.Consultar(idNivelExamen);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(NivelExamen nivelExamen)
    {
        return _nivelExamenService.Agregar(nivelExamen);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(NivelExamen nivelExamen)
    {
        _nivelExamenService.Editar(nivelExamen);
    }

    [HttpDelete]
    [Route("eliminar/{idNivelExamen}")]
    public void Eliminar(int idNivelExamen)
    {
        _nivelExamenService.Eliminar(idNivelExamen);
    }
}
