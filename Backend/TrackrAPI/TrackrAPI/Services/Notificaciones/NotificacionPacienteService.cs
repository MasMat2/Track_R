using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Helpers;
using TrackrAPI.Hubs;
using TrackrAPI.Repositorys.Notificaciones;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionPacienteService
{
    private readonly NotificacionService _notificacionService;
    private readonly NotificacionUsuarioService _notificacionUsuarioService;
    private readonly IHubContext<NotificacionPacienteHub, INotificacionPacienteHub> hubContext;
    private readonly UsuarioService _usuarioService;

    public NotificacionPacienteService(
        NotificacionService notificacionService,
        NotificacionUsuarioService notificacionUsuarioService,
        IHubContext<NotificacionPacienteHub, INotificacionPacienteHub> hubContext,
        UsuarioService usuarioService)
    {
        _notificacionService = notificacionService;
        this._notificacionUsuarioService = notificacionUsuarioService;
        this.hubContext = hubContext;
        _usuarioService = usuarioService;
    }

    private NotificacionPacienteDTO Mapear(NotificacionDTO notificacionDto, NotificacionUsuarioDto notificacionUsuarioDto)
    {
        var paciente = _usuarioService.Consultar((int) notificacionDto.IdPersona);
        if(paciente == null)
        {
            throw new Exception("No se encontró el paciente");
        }
        return new NotificacionPacienteDTO(
            notificacionUsuarioDto.IdNotificacionUsuario,
            notificacionUsuarioDto.IdNotificacion,
            notificacionUsuarioDto.IdUsuario,
            paciente.ObtenerNombreCompleto(),
            notificacionDto.Mensaje,
            notificacionDto.ComplementoMensaje,
            notificacionDto.FechaAlta,
            notificacionUsuarioDto.Visto,
            notificacionDto.IdTipoNotificacion,
            notificacionDto.IdChat
        );
    }

    public IEnumerable<NotificacionPacienteDTO> ConsultarPorPaciente(int idUsuario)
    {
        return _notificacionUsuarioService.ConsultarPorPaciente(idUsuario);
    }

    public IEnumerable<NotificacionPacientePopOverDto> ConsultarPorPacienteDto(int idUsuario)
    {
        return _notificacionUsuarioService.ConsultarPorPaciente(idUsuario)
        .Select(n => new NotificacionPacientePopOverDto {
            FechaAlta = n.FechaAlta,
            IdNotificacion = n.IdNotificacion,
            IdTipoNotificacion = n.IdTipoNotificacion,
            Mensaje = n.Mensaje,
            ComplementoMensaje = n.ComplementoMensaje,
            Titulo = n.Titulo,
            Visto = n.Visto
        });
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