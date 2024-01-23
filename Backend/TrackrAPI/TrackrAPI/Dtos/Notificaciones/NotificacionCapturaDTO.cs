namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionCapturaDTO(
    string Titulo,
    string Mensaje,
    int IdTipoNotificacion,
    int ? IdPersona,
    int ? IdChat
);