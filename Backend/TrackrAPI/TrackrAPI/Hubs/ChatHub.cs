using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Services.Chats;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub<IChatHub>
{
    private readonly IChatRepository _chatRepository;
    private readonly ChatService _chatService;
    private readonly ChatPersonaService _chatPersonaService;

    public ChatHub(IChatRepository chatRepository,
                   ChatService chatService,
                   ChatPersonaService chatPersonaService)
    {
        _chatRepository = chatRepository;
        _chatService = chatService;
        _chatPersonaService = chatPersonaService;
    }

    public override async Task OnConnectedAsync()
    {
        int idPersona = ObtenerIdUsuario();
        var chats = _chatService.ConsultarChats(idPersona);

        await Clients.Caller.NuevaConexion(chats);
        await base.OnConnectedAsync();
    }

    public async Task NuevaConexion(IEnumerable<ChatDTO> chat)
    {
        int idPersona = ObtenerIdUsuario();
        var chats = _chatService.ConsultarChats(idPersona);

        await Clients.Caller.NuevaConexion(chats);
        await base.OnConnectedAsync();
    }

    public async Task NuevoChat(Chat chat, List<int> idPersonas)
    {
        chat.Fecha = DateTime.Now;
        chat.Habilitado = true;
        _chatService.NuevoChat(chat, idPersonas, ObtenerIdUsuario());
        await Clients.Caller.NuevoChat(chat, idPersonas);

        foreach (var idPersona in idPersonas)
        {
            var chats = _chatService.ConsultarChats(idPersona);
            Clients.User(idPersona.ToString()).CargarChats(chats);
        }
    }

    public async Task CargarChats(IEnumerable<ChatDTO> chats)
    {
        int idPersona = ObtenerIdUsuario();
        var chatss = _chatService.ConsultarChats(idPersona);
        //await Clients.All.CargarChats(chatss);
    }

    public async Task EliminarChat(int idChat)
    {
        var personas = _chatPersonaService.ObtenerPersonasChatSelector(idChat);

        if (personas.Count > 1)
        {
            throw new CdisException("El chat solo puede ser eliminado cuando todos los participantes lo hayan abandonado");
        }

        _chatRepository.EliminarChat(idChat);

        await Clients.Caller.EliminarChat(idChat);
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

