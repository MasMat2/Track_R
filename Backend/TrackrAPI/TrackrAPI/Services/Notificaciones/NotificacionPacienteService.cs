using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Hubs;
using TrackrAPI.Repositorys.Notificaciones;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionPacienteService
{
    private readonly NotificacionService _notificacionService;
    private readonly NotificacionUsuarioService _notificacionUsuarioService;
    private readonly IHubContext<NotificacionPacienteHub, INotificacionPacienteHub> hubContext;

    public NotificacionPacienteService(
        NotificacionService notificacionService,
        NotificacionUsuarioService notificacionUsuarioService,
        IHubContext<NotificacionPacienteHub, INotificacionPacienteHub> hubContext)
    {
        _notificacionService = notificacionService;
        this._notificacionUsuarioService = notificacionUsuarioService;
        this.hubContext = hubContext;
    }

    private NotificacionPacienteDTO Mapear(NotificacionDTO notificacionDto, NotificacionUsuarioDto notificacionUsuarioDto)
    {
        return new NotificacionPacienteDTO(
            notificacionUsuarioDto.IdNotificacionUsuario,
            notificacionUsuarioDto.IdNotificacion,
            notificacionUsuarioDto.IdUsuario,
            notificacionDto.Titulo,
            notificacionDto.Mensaje,
            notificacionDto.FechaAlta,
            notificacionUsuarioDto.Visto
        );
    }

    public IEnumerable<NotificacionPacienteDTO> ConsultarPorPaciente(int idUsuario)
    {
        return _notificacionUsuarioService.ConsultarPorPaciente(idUsuario);
    }

    public async Task<NotificacionPacienteDTO> Notificar(NotificacionCapturaDTO notificacionDto, int idUsuario)
    {
        using var ts = new TransactionScope();

        var resultado = _notificacionService.Agregar(notificacionDto, idUsuario);
        var notificacionPaciente  = Mapear(resultado.Notificacion, resultado.NotificacionUsuario);
        await EnviarNotificacion(notificacionPaciente);

        ts.Complete();
        return  notificacionPaciente;
    }

    public async Task Notificar(NotificacionCapturaDTO notificacionDto, List<int> idUsuarios)
    {
        using var ts = new TransactionScope();

        var notificacion = _notificacionService.Agregar(notificacionDto, idUsuarios);
        var notificacionesPaciente = notificacion.NotificacionesUsuario
            .Select(nu => Mapear(notificacion.Notificacion, nu));

        var tasks = notificacionesPaciente.Select(EnviarNotificacion);

        await Task.WhenAll(tasks);

        ts.Complete();
    }

    private async Task EnviarNotificacion(NotificacionPacienteDTO notificacionPaciente)
    {
        var idUsuario = notificacionPaciente.IdUsuario.ToString();
        var usuarioCliente = hubContext.Clients.User(idUsuario);

        await usuarioCliente.NuevaNotificacion(notificacionPaciente);
    }
}