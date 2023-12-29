using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Services.GestionExamen;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Controllers.GestionExamen;

[Route("api/[controller]")]
[ApiController]
public class ExamenController : ControllerBase
{
    private readonly ExamenService _examenService;

    public ExamenController(ExamenService examenService)
    {
        _examenService = examenService;
    }

    [HttpGet]
    [Route("consultarTodosParaSelector/{idProgramacionExamen}")]
    public IEnumerable<Examen> ConsultarTodosParaSelector(int idProgramacionExamen)
    {
        return _examenService.ConsultarTodosParaSelector(idProgramacionExamen);
    }

    [HttpGet]
    [Route("consultarGeneral/{idProgramacionExamen}")]
    public IEnumerable<Examen> ConsultarGeneral(int idProgramacionExamen)
    {
        return _examenService.ConsultarGeneral(idProgramacionExamen);
    }

    [HttpGet]
    [Route("consultarMisExamenes")]
    public IEnumerable<ExamenGridDto> ConsultarMisExamenes()
    {
        return _examenService.ConsultarMisExamenes(Utileria.ObtenerIdUsuarioSesion(this));
    }

    [HttpGet("MisExamenes/contestados")]
    public IEnumerable<ExamenGridDto> ConsultarMisExamenesContestados()
    {
        return _examenService.ConsultarMisExamenesContestados(Utileria.ObtenerIdUsuarioSesion(this));
    }

    [HttpGet]
    [Route("consultar/{idExamen}")]
    public Examen? Consultar(int idExamen)
    {
        return _examenService.Consultar(idExamen);
    }

    [HttpGet]
    [Route("consultarMiExamen/{idExamen}")]
    public ExamenDto? ConsultarMiExamen(int idExamen)
    {
        return _examenService.ConsultarMiExamen(idExamen);
    }

    [HttpGet]
    [Route("consultarMiExamenIndividual/{idExamen}")]
    public ExamenDto? ConsultarMiExamenIndividual(int idExamen)
    {
        return _examenService.ConsultarMiExamenIndividual(idExamen);
    }

    [HttpGet]
    [Route("consultarCalificaciones/{idProgramacionExamen}")]
    public IEnumerable<ExamenCalificacionDto> ConsultarCalificaciones(int idProgramacionExamen)
    {
        return _examenService.ConsultarCalificaciones(idProgramacionExamen);
    }

    [HttpPost]
    [Route("agregar")]
    public int Agregar(Examen examen)
    {
        return _examenService.Agregar(examen);
    }

    [HttpPut]
    [Route("editar")]
    public void Editar(Examen examen)
    {
        _examenService.Editar(examen);
    }

    [HttpDelete]
    [Route("eliminar/{idExamen}")]
    public void Eliminar(int idExamen)
    {
        _examenService.Eliminar(idExamen);
    }

    [HttpPost]
    [Route("actualizar")]
    public void Actualizar(List<Examen> examenList)
    {
        _examenService.Actualizar(examenList);
    }
}
