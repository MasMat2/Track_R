using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Helpers;
using TrackrAPI.Hubs;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Controllers.Notificaciones;

// TODO: 2023-08-18 -> Temporal
[ApiController]
[Route("api/[controller]")]
public class NotificacionController : ControllerBase
{
    private readonly NotificacionDoctorService _notificacionService;
    private readonly NotificacionPacienteService _notificacionPacienteService;
    /* private readonly NotificacionPacienteHub _notificacionPacienteHub; */

    public NotificacionController(
        NotificacionDoctorService notificacionService,
        NotificacionPacienteService notificacionPacienteService
/*         NotificacionPacienteHub notificacionPacienteHub */)
    {
        _notificacionService = notificacionService;
        _notificacionPacienteService = notificacionPacienteService;
        /* _notificacionPacienteHub = notificacionPacienteHub; */
    }

    [HttpPost]
    public async Task Notificar(NotificacionDoctorCapturaDTO notificacionDto)
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        await _notificacionService.Notificar(notificacionDto, idUsuario);
    }
    [HttpGet("usuario")]
    public IEnumerable<NotificacionPacientePopOverDto> NotificacionesUsuario()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        return _notificacionPacienteService.ConsultarPorPacienteDto(idUsuario);
    }

    /* [HttpPost("leida")]
    public async Task MarcarComoVista(int idNotificacion)
    {
        List<int> idNotificaciones = new List<int> { idNotificacion };
        await _notificacionPacienteHub.MarcarComoVistas(idNotificaciones);
    } */

}