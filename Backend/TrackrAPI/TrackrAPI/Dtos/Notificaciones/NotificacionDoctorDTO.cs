namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionDoctorDTO(
    int IdNotificacionUsuario,
    int IdNotificacion,
    int IdUsuario,
    string NombrePaciente,
    string Mensaje,
    string? ComplementoMensaje,
    DateTime FechaAlta,
    bool Visto,
    int IdTipoNotificacion,
    int IdPaciente,
    string ? Imagen,
    int ? IdChat,
    string ? ClaveTipoNotificacion
);