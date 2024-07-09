using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificacionPacienteHub : Hub<INotificacionPacienteHub>
{
    private readonly NotificacionPacienteService _notificacionPacienteService;
    private readonly NotificacionUsuarioService _notificacionUsuarioService;

    public NotificacionPacienteHub(
        NotificacionPacienteService notificacionPacienteService,
        NotificacionUsuarioService notificacionUsuarioService)
    {
        _notificacionPacienteService = notificacionPacienteService;
        _notificacionUsuarioService = notificacionUsuarioService;
    }

    public override async Task OnConnectedAsync()
    {
        var idUsuario = ObtenerIdUsuario();
        var notificaciones = _notificacionPacienteService.ConsultarPorPaciente(idUsuario).ToList();

        await Clients.Caller.NuevaConexion(notificaciones);
        await base.OnConnectedAsync();
    }

    public async Task MarcarComoVistas(List<int> idNotificacionesUsuario, bool tomaTomada)
    {
        var idUsuario = ObtenerIdUsuario();

        _notificacionUsuarioService.MarcarComoVistas(idNotificacionesUsuario, tomaTomada);
        await Clients.User(idUsuario.ToString()).NotificarComoVistas(idNotificacionesUsuario);
    }

    private int ObtenerIdUsuario()
    {
        if (Context.UserIdentifier is null)
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        if (!int.TryParse(Context.UserIdentifier, out int idUsuario))
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        return idUsuario;
    }
}