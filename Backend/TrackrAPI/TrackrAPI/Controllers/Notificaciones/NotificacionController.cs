using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Controllers.Notificaciones;

// TODO: 2023-08-18 -> Temporal
[ApiController]
[Route("api/[controller]")]
public class NotificacionController : ControllerBase
{
    private readonly NotificacionDoctorService _notificacionService;

    public NotificacionController(NotificacionDoctorService notificacionService)
    {
        _notificacionService = notificacionService;
    }

    [HttpPost]
    public async Task Notificar(NotificacionDoctorCapturaDTO notificacionDto)
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        await _notificacionService.Notificar(notificacionDto, idUsuario);
    }
}