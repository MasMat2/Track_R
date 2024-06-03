using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Services.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Dtos.GestionExamen;
using Microsoft.AspNetCore.Authorization;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class ExamenReactivoController : ControllerBase
{
    private readonly ExamenReactivoService _examenReactivo;

    public ExamenReactivoController(ExamenReactivoService examenReactivo)
    {
        _examenReactivo = examenReactivo;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector")]
    public IEnumerable<ExamenReactivo> ConsultarTodosParaSelector()
    {
        return _examenReactivo.ConsultarTodosParaSelector();
    }

    [HttpGet]
    [Route("consultarGeneral")]
    public IEnumerable<ExamenReactivo> ConsultarGeneral()
    {
        return _examenReactivo.ConsultarGeneral();
    }

    [HttpGet]
    [Route("consultarReactivosExamen/{idExamen}")]
    public IEnumerable<ExamenReactivoDto> ConsultarReactivosExamen(int idExamen)
    {
        return _examenReactivo.ConsultarReactivosExamen(idExamen);
    }

    [HttpGet("consultarReactivosExamenParaExcel/{idProgramacionExamen}")]
    public RespuestasExcelDto ConsultarReactivosExamenExcel(int idProgramacionExamen)
    {
        return _examenReactivo.ConsultarReactivosExamenExcel(idProgramacionExamen);
    }

    [HttpGet]
    [Route("consultar/{idExamenReactivo}")]
    public ExamenReactivo? Consultar(int idExamenReactivo)
    {
        return _examenReactivo.Consultar(idExamenReactivo);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(ExamenReactivo programacionExamen)
    {
        return _examenReactivo.Agregar(programacionExamen);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(ExamenReactivo programacionExamen)
    {
        _examenReactivo.Editar(programacionExamen);
    }

    [HttpDelete]
    [Route("eliminar/{idExamenReactivo}")]
    public void Eliminar(int idExamenReactivo)
    {
        _examenReactivo.Eliminar(idExamenReactivo);
    }

    [HttpPost]
    [Route("revisar")]
    public float Revisar(List<ExamenReactivo> examenReactivoList)
    {
        return _examenReactivo.Revisar(examenReactivoList);
    }
}
