using TrackrAPI.Dtos.Chats;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Services.Chats;

public class ChatPersonaService
{
    private readonly IChatPersonaRepository _chatPersonaRepository;

    public ChatPersonaService(IChatPersonaRepository chatPersonaRepository)
    {
        _chatPersonaRepository = chatPersonaRepository;
    }

    public void agregarPersonaChat(ChatPersonaFormDTO chatPersonaFormDTO)
    {
        var idPersonas = chatPersonaFormDTO.IdPersonas;
        foreach(var idPersona in idPersonas)
        {
            var chatPersona = new ChatPersona
            {
                IdChat = chatPersonaFormDTO.IdChat,
                IdPersona = idPersona,
                IdTipo = chatPersonaFormDTO.IdTipo
            };

            _chatPersonaRepository.Agregar(chatPersona);
        }
    }

    public List<int> ObtenerPacientesPorPadecimiento(int idPadecimiento)
    {
        return _chatPersonaRepository.ObtenerPacientesPorPadecimiento(idPadecimiento);
    }
}

