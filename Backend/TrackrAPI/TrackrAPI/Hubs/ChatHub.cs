using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatHub : Hub<IChatHub>
{
    private readonly IChatRepository _chatRepository;

    public ChatHub(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var chats = _chatRepository.ConsultarChats();


        await Clients.Caller.NuevaConexion(chats);
        await base.OnConnectedAsync();
    }
}

