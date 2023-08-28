namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionUsuarioDto(
    int IdNotificacionUsuario,
    int IdNotificacion,
    int IdUsuario,
    bool Visto
);