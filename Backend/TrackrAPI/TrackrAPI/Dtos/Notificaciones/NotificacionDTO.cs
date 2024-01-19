namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionDTO(
    int IdNotificacion,
    string Titulo,
    string Mensaje,
    DateTime FechaAlta,
    int IdTipoNotificacion,
    int ? IdPersona,
    int ? IdChat
);