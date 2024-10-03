namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionCapturaDTO(
    string Titulo,
    string Mensaje,
    string? ComplementoMensaje,
    int IdTipoNotificacion,
    int ? IdPersona,
    int ? IdChat,
    int ? IdPadecimiento
);