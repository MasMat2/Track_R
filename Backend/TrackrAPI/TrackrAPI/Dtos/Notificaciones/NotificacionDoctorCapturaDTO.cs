namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionDoctorCapturaDTO(
    string Mensaje,
    string? ComplementoMensaje,
    int IdTipoNotificacion,
    int IdPaciente,
    int ? IdPersona,
    int ? IdChat,
    int ? IdPadecimiento
);