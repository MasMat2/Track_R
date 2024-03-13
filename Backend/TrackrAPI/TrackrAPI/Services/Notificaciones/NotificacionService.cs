using System.Transactions;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Notificaciones;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionService
{
    private readonly INotificacionRepository notificacionRepository;
    private readonly NotificacionUsuarioService _notificacionUsuarioService;

    public NotificacionService(
        INotificacionRepository notificacionRepository,
        NotificacionUsuarioService notificacionUsuarioService)
    {
        this.notificacionRepository = notificacionRepository;
        _notificacionUsuarioService = notificacionUsuarioService;
    }

    private NotificacionDTO Agregar(NotificacionCapturaDTO notificacionDto)
    {
        var notificacion = new Notificacion()
        {
            Titulo = notificacionDto.Titulo,
            Mensaje = notificacionDto.Mensaje,
            FechaAlta = DateTime.UtcNow,
            IdTipoNotificacion = notificacionDto.IdTipoNotificacion,
            IdPersona = notificacionDto.IdPersona,
            IdChat = notificacionDto.IdChat,
        };

        notificacionRepository.Agregar(notificacion);

        return new NotificacionDTO(
            notificacion.IdNotificacion,
            notificacion.Titulo,
            notificacion.Mensaje,
            notificacion.FechaAlta,
            notificacion.IdTipoNotificacion,
            notificacion.IdPersona,
            notificacion.IdChat
        );
    }

    public NotificacionResultadoMultipleDTO Agregar(NotificacionCapturaDTO notificacionDto, List<int> idsUsuario)
    {
        using var ts = new TransactionScope();

        var notificacion = Agregar(notificacionDto);
        var notificacionesUsuario = _notificacionUsuarioService.Agregar(notificacion.IdNotificacion, idsUsuario);

        ts.Complete();

        return new NotificacionResultadoMultipleDTO(notificacion, notificacionesUsuario.ToList());
    }

    public NotificacionResultadoDTO Agregar(NotificacionCapturaDTO notificacionDto, int idUsuario)
    {
        using var ts = new TransactionScope();

        var notificacion = Agregar(notificacionDto);
        var notificacionUsuario = _notificacionUsuarioService.Agregar(notificacion.IdNotificacion, idUsuario);

        ts.Complete();

        return new NotificacionResultadoDTO(notificacion, notificacionUsuario);
    }
}