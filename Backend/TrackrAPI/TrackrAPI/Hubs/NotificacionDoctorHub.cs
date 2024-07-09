using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificacionDoctorHub : Hub<INotificacionDoctorHub>
{
    private readonly NotificacionDoctorService _notificacionDoctorService;
    private readonly NotificacionUsuarioService _notificacionUsuarioService;

    public NotificacionDoctorHub(
        NotificacionDoctorService notificacionDoctorService,
        NotificacionUsuarioService notificacionUsuarioService)
    {
        _notificacionDoctorService = notificacionDoctorService;
        _notificacionUsuarioService = notificacionUsuarioService;
    }

    public override async Task OnConnectedAsync()
    {
        var idUsuario = ObtenerIdUsuario();
        var notificaciones = await _notificacionDoctorService.ConsultarPorDoctor(idUsuario);

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