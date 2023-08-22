using TrackrAPI.Dtos.Notificaciones;

namespace TrackrAPI.Hubs;

public interface INotificacionPacienteHub
{
    Task NuevaConexion(IEnumerable<NotificacionPacienteDTO> notificaciones);
    Task NuevaNotificacion(NotificacionPacienteDTO notificacion);
    Task NotificarComoVistas(List<int> idNotificacionUsuario);
}