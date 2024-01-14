using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Helpers;
using TrackrAPI.Hubs;
using TrackrAPI.Repositorys.Notificaciones;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionDoctorService
{
    private readonly NotificacionService _notificacionService;
    private readonly NotificacionUsuarioService _notificacionUsuarioService;
    private readonly UsuarioService _usuarioService;
    private readonly IHubContext<NotificacionDoctorHub, INotificacionDoctorHub> _hubContext;

    public NotificacionDoctorService(
        NotificacionService notificacionService,
        NotificacionUsuarioService notificacionUsuarioService,
        UsuarioService usuarioService,
        IHubContext<NotificacionDoctorHub, INotificacionDoctorHub> hubContext)
    {
        _notificacionService = notificacionService;
        _notificacionUsuarioService = notificacionUsuarioService;
        _usuarioService = usuarioService;
        _hubContext = hubContext;
    }

    private NotificacionDoctorDTO Mapear(
        NotificacionDTO notificacionDto,
        NotificacionUsuarioDto notificacionUsuarioDto,
        int idPaciente)
    {
        return new NotificacionDoctorDTO(
            notificacionUsuarioDto.IdNotificacionUsuario,
            notificacionUsuarioDto.IdNotificacion,
            notificacionUsuarioDto.IdUsuario,
            notificacionDto.Titulo,
            notificacionDto.Mensaje,
            notificacionDto.FechaAlta,
            notificacionUsuarioDto.Visto,
            notificacionDto.IdTipoNotificacion,
            idPaciente,
            ""
        );
    }

    private NotificacionCapturaDTO Mapear(NotificacionDoctorCapturaDTO notificacionDoctorDto)
    {
        var paciente = _usuarioService.Consultar(notificacionDoctorDto.IdPaciente);

        if (paciente is null)
        {
            throw new CdisException("No se encontró el paciente");
        }

        var notificacionDto = new NotificacionCapturaDTO(
            paciente.ObtenerNombreCompleto(),
            notificacionDoctorDto.Mensaje,
            notificacionDoctorDto.IdTipoNotificacion);

        return notificacionDto;
    }

    public IEnumerable<NotificacionDoctorDTO> ConsultarPorDoctor(int idUsuario)
    {
        return _notificacionUsuarioService.ConsultarPorDoctor(idUsuario);
    }

    public async Task Notificar(NotificacionDoctorCapturaDTO notificacionDoctorCaptura, int idDoctor)
    {
        using var ts = new TransactionScope();

        var notificacionCaptura = Mapear(notificacionDoctorCaptura);

        var resultado = _notificacionService.Agregar(notificacionCaptura, idDoctor);

        var notificacionDoctor = Mapear(
            resultado.Notificacion,
            resultado.NotificacionUsuario,
            notificacionDoctorCaptura.IdPaciente);

        await EnviarNotificacion(notificacionDoctor);

        ts.Complete();
    }

    public async Task Notificar(NotificacionDoctorCapturaDTO notificacionDoctorCaptura, List<int> idsDoctor)
    {
        using var ts = new TransactionScope();

        var notificacionCaptura = Mapear(notificacionDoctorCaptura);

        var resultado = _notificacionService.Agregar(notificacionCaptura, idsDoctor);
        var notificacionesDoctor = resultado.NotificacionesUsuario
            .Select(nu => Mapear(resultado.Notificacion, nu, notificacionDoctorCaptura.IdPaciente));

        var tasks = notificacionesDoctor.Select(EnviarNotificacion);

        await Task.WhenAll(tasks);

        ts.Complete();
    }

    private async Task EnviarNotificacion(NotificacionDoctorDTO notificacionDoctor)
    {
        var idUsuario = notificacionDoctor.IdUsuario.ToString();
        var usuarioCliente = _hubContext.Clients.User(idUsuario);

        await usuarioCliente.NuevaNotificacion(notificacionDoctor);
    }
}