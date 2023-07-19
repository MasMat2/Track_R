using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Services.GestionExpediente;

namespace Trackr.Controllers.GestionExpediente;

[Route("api/[controller]")]
[ApiController]
public class ExpedienteRecomendacionController : ControllerBase
{

    private readonly ExpedienteRecomendacionService _expedienteRecomendacionService;
    private readonly ExpedienteTrackrService _expedienteTrackrService;
    private readonly TrackrContext _context;

    public ExpedienteRecomendacionController(ExpedienteRecomendacionService expedienteRecomendacionService, ExpedienteTrackrService expedienteTrackrService, TrackrContext context)
    {
        this._expedienteRecomendacionService = expedienteRecomendacionService;
        this._expedienteTrackrService = expedienteTrackrService;
        this._context = context;
    }

    [HttpGet("usuario/{idUsuario}")]
    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarPorUsuario(int idUsuario)
    {
        return _expedienteRecomendacionService.ConsultarPorUsuario(idUsuario);
    }

    [HttpGet("{idExpedienteRecomendacion}")]
    public ExpedienteRecomendacionDTO ConsultarPorId(int idExpedienteRecomendacion)
    {
        return _expedienteRecomendacionService.ConsultarPorId(idExpedienteRecomendacion);
    }
    [HttpGet]
    public IEnumerable<ExpedienteRecomendaciones> Consultar()
    {
        return _context.ExpedienteRecomendaciones.ToList();
    }

    [HttpGet("expediente/{idUsuario}")]
    public int ObtenerIdExpediente(int idUsuario)
    {
        return _expedienteTrackrService.ConsultarWrapperPorUsuario(idUsuario).expediente.IdExpediente;
    }

    [HttpPost]
    public void Agregar(ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
    {
        int idDoctor = Utileria.ObtenerIdUsuarioSesion(this);
        expedienteRecomendacionDTO.IdDoctor = idDoctor;

        expedienteRecomendacionDTO.Fecha = DateTime.Now;
        _expedienteRecomendacionService.Agregar(expedienteRecomendacionDTO);

    }

    [HttpPut]
    public void Editar(ExpedienteRecomendacionDTO expedienteRecomendacionDTO)
    {
        _expedienteRecomendacionService.Editar(expedienteRecomendacionDTO);
    }

    [HttpDelete("{idExpedienteRecomendacion}")]
    public void Eliminar(int idExpedienteRecomendacion)
    {
        _expedienteRecomendacionService.Eliminar(idExpedienteRecomendacion);
    }
}