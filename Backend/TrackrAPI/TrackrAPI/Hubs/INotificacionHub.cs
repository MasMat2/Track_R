using TrackrAPI.Dtos.Notificaciones;

namespace TrackrAPI.Hubs;

public interface INotificacionHub
{
    Task NuevaConexion(IEnumerable<NotificacionUsuarioDto> notificaciones);
    Task NuevaNotificacion(NotificacionUsuarioDto notificacion);
    Task NotificarComoVistas(List<int> idNotificacionUsuario);
}