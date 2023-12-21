using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Services.Chats;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub<IChatHub>
{
    private readonly IChatRepository _chatRepository;
    private readonly ChatService _chatService;

    public ChatHub(IChatRepository chatRepository,
                   ChatService chatService)
    {
        _chatRepository = chatRepository;
        _chatService = chatService;
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
        _chatService.NuevoChat(chat, idPersonas,ObtenerIdUsuario());
       await Clients.All.NuevoChat(chat, idPersonas);
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

