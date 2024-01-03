using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Models;
using TrackrAPI.Services.Chats;

namespace TrackrAPI.Hubs;

public class ChatMensajeHub : Hub<IChatMensajeHub>
{
    private readonly ChatMensajeService _chatMensajeService;

    public ChatMensajeHub(ChatMensajeService chatMensajeService)
    {
        _chatMensajeService = chatMensajeService;
    }

    public override async Task OnConnectedAsync()
    {
        var idUsuario = ObtenerIdUsuario();
        var mensajes = _chatMensajeService.ObtenerMensajesPorChat(idUsuario);
        await Clients.Caller.NuevaConexion(mensajes);
    }

    public async Task NuevoMensaje(ChatMensajeDTO mensaje)
    {
        Console.WriteLine(mensaje.ArchivoTipoMime);
        mensaje.IdPersona = this.ObtenerIdUsuario();
        await _chatMensajeService.NuevoMensaje(mensaje);
        await Clients.All.NuevoMensaje(mensaje);
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

