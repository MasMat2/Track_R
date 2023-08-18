using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Notificaciones;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionService
{
    private readonly INotificacionRepository notificacionRepository;
    private readonly INotificacionUsuarioRepository notificacionUsuarioRepository;
    private readonly IHubContext<NotificacionHub, INotificacionHub> hubContext;

    public NotificacionService(
        INotificacionRepository notificacionRepository,
        INotificacionUsuarioRepository notificacionUsuarioRepository,
        IHubContext<NotificacionHub, INotificacionHub> hubContext)
    {
        this.notificacionRepository = notificacionRepository;
        this.notificacionUsuarioRepository = notificacionUsuarioRepository;
        this.hubContext = hubContext;
    }

    private Notificacion AgregarNotificacion(string origen, string descripcion)
    {
        var notificacion = new Notificacion()
        {
            Origen = origen,
            Descripcion = descripcion,
            FechaAlta = DateTime.Now,
            IdTipoNotificacion = 1
        };

        notificacionRepository.Agregar(notificacion);

        return notificacion;
    }

    private NotificacionUsuario AgregarNotificacionUsuario(int idNotificacion, int idUsuario)
    {
        var notificacionUsuario = new NotificacionUsuario
        {
            IdNotificacion = idNotificacion,
            IdUsuario = idUsuario,
            Visto = false
        };

        notificacionUsuarioRepository.Agregar(notificacionUsuario);

        return notificacionUsuario;
    }

    public async Task NotificarYGuardar(int idUsuario, string origen, string descripcion)
    {
        using var ts = new TransactionScope();

        var notificacion = AgregarNotificacion(origen, descripcion);
        var notificacionUsuario = AgregarNotificacionUsuario(notificacion.IdNotificacion, idUsuario);

        await EnviarNotificacion(notificacion, notificacionUsuario);

        ts.Complete();
    }

    public async Task NotificarYGuardar(List<int> idUsuarios, string origen, string descripcion)
    {
        using var ts = new TransactionScope();

        var notificacion = AgregarNotificacion(origen, descripcion);

        // TODO: 2023-03-22 -> Hacer en paralelo
        foreach (var idUsuario in idUsuarios)
        {
            var notificacionUsuario = AgregarNotificacionUsuario(notificacion.IdNotificacion, idUsuario);
            await EnviarNotificacion(notificacion, notificacionUsuario);
        }

        ts.Complete();
    }

    private async Task EnviarNotificacion(Notificacion notificacion, NotificacionUsuario notificacionUsuario)
    {
        var notificacionDto = new NotificacionUsuarioDto
        {
            IdUsuario = notificacionUsuario.IdUsuario,
            IdNotificacion = notificacionUsuario.IdNotificacion,
            Origen = notificacion.Origen,
            Descripcion = notificacion.Descripcion,
            FechaAlta = DateTime.Now,
            Visto = false
        };

        var idUsuario = notificacionUsuario.IdUsuario.ToString();
        var usuarioCliente = hubContext.Clients.User(idUsuario);

        await usuarioCliente.NuevaNotificacion(notificacionDto);
    }
}