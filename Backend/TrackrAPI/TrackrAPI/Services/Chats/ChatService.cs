using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Helpers;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Services.Chats;

public class ChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHub, IChatHub> hubContext;

    public ChatService(IChatRepository chatRepository, IHubContext<ChatHub, IChatHub> hubContext)
    {
        _chatRepository = chatRepository;
        this.hubContext = hubContext;
    }

    public Chat AgregarChat()
    {
        var chat = new Chat()
        {
            Habilitado = true,
            Fecha = DateTime.Now,
        };

        _chatRepository.Agregar(chat);

        return chat;
    }

    private async Task EnviarChat(Chat chat)
    {
        await hubContext.Clients.All.NuevoChat(chat);
    }
}