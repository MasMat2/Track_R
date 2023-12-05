using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Services.Chats;

public class ChatMensajeService
{
    private readonly IChatMensajeRepository _chatMensajeRepository;
    private readonly IHubContext<ChatMensajeHub,IChatMensajeHub> _hubContext;
    private readonly IChatPersonaRepository _chatPersonaRepository;

    public ChatMensajeService(IChatMensajeRepository chatMensajeRepository,
                              IHubContext<ChatMensajeHub,IChatMensajeHub> hubContext,
                              IChatPersonaRepository chatPersonaRepository)
    {
        _chatMensajeRepository = chatMensajeRepository;
        _hubContext = hubContext;
        _chatPersonaRepository = chatPersonaRepository;
    }

    public IEnumerable<IEnumerable<ChatMensaje>> ObtenerMensajesPorChat(int IdPersona)
    {
        var idChats = _chatPersonaRepository.ConsultarChatsPorPersona(IdPersona);
        IEnumerable<IEnumerable<ChatMensaje>> chats = new List<IEnumerable<ChatMensaje>>();

        foreach(var idChat in idChats) {
            var aux = _chatMensajeRepository.ObtenerMensajePorChat(idChat.IdChat);
            chats = chats.Append(aux);
        }
        
        return chats;
    }

    public void NuevoMensaje(ChatMensaje mensaje)
    {
        _chatMensajeRepository.Agregar(mensaje);
    }
}

