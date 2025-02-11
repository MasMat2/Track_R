namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionPacienteDTO(
    int IdNotificacionUsuario,
    int IdNotificacion,
    int IdUsuario,
    string Titulo,
    string Mensaje,
    string? ComplementoMensaje,
    DateTime FechaAlta,
    bool Visto,
    int IdTipoNotificacion,
    int? IdChat
);