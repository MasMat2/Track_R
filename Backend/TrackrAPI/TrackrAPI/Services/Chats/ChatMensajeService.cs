using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Services.Chats;

public class ChatMensajeService
{
    private readonly IChatMensajeRepository _chatMensajeRepository;
    private readonly IHubContext<ChatMensajeHub,IChatMensajeHub> _hubContext;

    public ChatMensajeService(IChatMensajeRepository chatMensajeRepository,
                              IHubContext<ChatMensajeHub,IChatMensajeHub> hubContext)
    {
        _chatMensajeRepository = chatMensajeRepository;
        _hubContext = hubContext;
    }

    public IEnumerable<ChatMensaje> ObtenerMensajesPorChat(int IdChat)
    {

        return _chatMensajeRepository.ObtenerMensajePorChat(IdChat);
    }

    public void NuevoMensaje(ChatMensaje mensaje)
    {
        _chatMensajeRepository.Agregar(mensaje);
    }
}

