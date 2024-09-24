using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Services.GestionExpediente;

namespace Trackr.Controllers.GestionExpediente;

[Route("api/[controller]")]
[ApiController]
public class ExpedienteRecomendacionController : ControllerBase
{

    private readonly ExpedienteRecomendacionService _expedienteRecomendacionService;

    public ExpedienteRecomendacionController(ExpedienteRecomendacionService expedienteRecomendacionService)
    {
        _expedienteRecomendacionService = expedienteRecomendacionService;

    }

    [HttpGet("grid/usuario/{idUsuario}")]
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGridPorUsuario(int idUsuario)
    {
        return _expedienteRecomendacionService.ConsultarGridPorUsuario(idUsuario);
    }

    [HttpGet("grid/usuario/recomendacionGeneral/{idUsuario}")]
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGridPorUsuarioRecomendacionGeneral(int idUsuario)
    {
        return _expedienteRecomendacionService.ConsultarGridPorUsuarioRecomendacionGeneral(idUsuario);
    }

    [HttpGet("{idExpedienteRecomendacion}")]
    public ExpedienteRecomendacionFormDTO Consultar(int idExpedienteRecomendacion)
    {
        return _expedienteRecomendacionService.Consultar(idExpedienteRecomendacion);
    }

    [HttpPost]
    public async Task Agregar(ExpedienteRecomendacionFormDTO expedienteRecomendacionFormDTO)
    {
        int IdDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        expedienteRecomendacionFormDTO.IdDoctor = IdDoctor;
        await _expedienteRecomendacionService.Agregar(expedienteRecomendacionFormDTO);
    }

    [HttpPut]
    public void Editar(ExpedienteRecomendacionFormDTO expedienteRecomendacionFormDTO)
    {
        _expedienteRecomendacionService.Editar(expedienteRecomendacionFormDTO);
    }

    [HttpDelete("{idExpedienteRecomendacion}")]
    public void Eliminar(int idExpedienteRecomendacion)
    {
        _expedienteRecomendacionService.Eliminar(idExpedienteRecomendacion);
    }
}