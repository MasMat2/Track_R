using TrackrAPI.Dtos.Chats;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Services.Chats;

public class ChatMensajeVistoService
{
    private readonly IChatMensajeVistoRepository _chatMensajeVistoRepository;

    public ChatMensajeVistoService(IChatMensajeVistoRepository chatMensajeVistoRepository)
    {
        _chatMensajeVistoRepository = chatMensajeVistoRepository;
    }

    public void agregarPersonaMensajeVisto(ChatMensajeVistoFormDTO chatMensajeVistoFormDTO)
    {
        var mensajeVisto = new ChatMensajeVisto
        {
            IdMensaje = chatMensajeVistoFormDTO.IdMensaje,
            IdPersona = chatMensajeVistoFormDTO.IdPersona,
        };

        _chatMensajeVistoRepository.Agregar(mensajeVisto);
    }
}

