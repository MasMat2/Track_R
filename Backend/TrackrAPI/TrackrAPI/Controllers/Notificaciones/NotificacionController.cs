using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Controllers.Notificaciones;

// TODO: 2023-08-18 -> Temporal
[ApiController]
[Route("api/[controller]")]
public class NotificacionController : ControllerBase
{
    private readonly NotificacionService _notificacionService;

    public NotificacionController(NotificacionService notificacionService)
    {
        _notificacionService = notificacionService;
    }

    [HttpPost]
    public async Task Notificar()
    {
        int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
        await _notificacionService.NotificarYGuardar(idUsuario, "Test", "Testing");
    }
}