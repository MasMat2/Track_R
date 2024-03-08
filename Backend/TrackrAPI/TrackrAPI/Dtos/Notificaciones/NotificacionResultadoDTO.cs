namespace TrackrAPI.Dtos.Notificaciones;

public record NotificacionResultadoMultipleDTO(
    NotificacionDTO Notificacion,
    List<NotificacionUsuarioDto> NotificacionesUsuario
);

public record NotificacionResultadoDTO(
    NotificacionDTO Notificacion,
    NotificacionUsuarioDto NotificacionUsuario
);