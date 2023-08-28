using TrackrAPI.Dtos.Notificaciones;

namespace TrackrAPI.Hubs;

public interface INotificacionDoctorHub
{
    Task NuevaConexion(IEnumerable<NotificacionDoctorDTO> notificaciones);
    Task NuevaNotificacion(NotificacionDoctorDTO notificacion);
    Task NotificarComoVistas(List<int> idNotificacionUsuario);
}