
using Microsoft.AspNetCore.Mvc;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Controllers.Notificaciones;

[ApiController]
[Route("api/[controller]")]
public class NotificacionUsuarioController : ControllerBase
{
    private readonly NotificacionUsuarioService _notificacionUsuarioService;

    public NotificacionUsuarioController(NotificacionUsuarioService notificacionUsuarioService)
    {
        _notificacionUsuarioService = notificacionUsuarioService;
    }


    [HttpGet("{idNotificacionUsuario}")]
    public async Task<NotificacionUsuarioDto> ConsultarPorId(int idNotificacionUsuario)
    {
        return await _notificacionUsuarioService.ConsultarDto(idNotificacionUsuario);
    }

}