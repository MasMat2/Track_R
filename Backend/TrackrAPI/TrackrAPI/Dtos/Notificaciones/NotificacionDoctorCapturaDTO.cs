namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionDoctorCapturaDTO(
    string Mensaje,
    int IdTipoNotificacion,
    int IdPaciente
);