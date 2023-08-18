using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Repositorys.Notificaciones;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificacionHub : Hub<INotificacionHub>
{
    private readonly INotificacionUsuarioRepository _notificacionUsuarioRepository;

    public NotificacionHub(INotificacionUsuarioRepository notificacionUsuarioRepository)
    {
        _notificacionUsuarioRepository = notificacionUsuarioRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var idUsuario = ObtenerIdUsuario();
        var notificaciones = _notificacionUsuarioRepository.ConsultarParaSidebar(idUsuario);

        await Clients.Caller.NuevaConexion(notificaciones);
        await base.OnConnectedAsync();
    }

    public async Task MarcarComoVistas(List<int> idNotificacionesUsuario)
    {
        var idUsuario = ObtenerIdUsuario();

        _notificacionUsuarioRepository.MarcarComoVistas(idNotificacionesUsuario);
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