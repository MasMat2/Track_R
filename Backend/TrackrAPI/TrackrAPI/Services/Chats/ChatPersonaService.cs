using TrackrAPI.Dtos.Chats;
using TrackrAPI.Helpers;
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

    public void agregarPersonaChat(ChatPersonaFormDTO chatPersonaFormDTO, int idUsuario)
    {
        var idPersonas = chatPersonaFormDTO.IdPersonas;
        for(var i = 0; i < idPersonas.Count; i++)
        {
            var chatPersona = new ChatPersona
            {
                IdChat = chatPersonaFormDTO.IdChat,
                IdPersona = idPersonas[i],
                IdTipo = idPersonas[i] == idUsuario? GeneralConstant.IdTipoUsuarioChatAdmin : chatPersonaFormDTO.IdTipo,
            };
            _chatPersonaRepository.Agregar(chatPersona);
        }
        /*foreach(var idPersona in idPersonas)
        {
            var chatPersona = new ChatPersona
            {
                IdChat = chatPersonaFormDTO.IdChat,
                IdPersona = idPersona,
                IdTipo = chatPersonaFormDTO.IdTipo
            };

            _chatPersonaRepository.Agregar(chatPersona);
        }*/
    }

    public List<int> ObtenerPacientesPorPadecimiento(int idPadecimiento)
    {
        return _chatPersonaRepository.ObtenerPacientesPorPadecimiento(idPadecimiento);
    }

    public List<ChatPersonaSelectorDTO> ObtenerPersonasChatSelector(int idChat)
    {
        Console.WriteLine(idChat);
        return _chatPersonaRepository.ConsultarPersonasPorChat(idChat)
                                      .Select(x => new ChatPersonaSelectorDTO
                                      {
                                          IdUsuario = x.IdPersona,
                                          Nombre = x.IdPersonaNavigation.Nombre + " " + x.IdPersonaNavigation.ApellidoPaterno + " " + x.IdPersonaNavigation.ApellidoMaterno
                                      })
                                      .ToList();
    }
}

