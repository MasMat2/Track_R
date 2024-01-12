using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Chats;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Hubs;

public class ChatMensajeHub : Hub<IChatMensajeHub>
{
    private readonly ChatMensajeService _chatMensajeService;
    private readonly UsuarioService _usuarioService;
    private readonly IChatPersonaRepository _chatPersonaRepository;
    public ChatMensajeHub(ChatMensajeService chatMensajeService,
                          UsuarioService usuarioService,
                          IChatPersonaRepository chatPersonaRepository)
    {
        _chatMensajeService = chatMensajeService;
        _usuarioService = usuarioService;
        _chatPersonaRepository = chatPersonaRepository;
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
        _chatMensajeService.NuevoMensaje(mensaje);

        var user = _usuarioService.ConsultarDto(mensaje.IdPersona);
        mensaje.NombrePersona = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;

        var idPersonas = _chatPersonaRepository.ConsultarPersonasPorChat(mensaje.IdChat).Select(x => x.IdPersona).ToList();

        foreach(var persona in idPersonas)
        {
            Clients.User(persona.ToString()).NuevoMensaje(mensaje);
        }

        //await Clients.All.NuevoMensaje(mensaje);
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

