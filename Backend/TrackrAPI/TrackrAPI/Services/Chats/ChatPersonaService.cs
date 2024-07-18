using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Services.Chats;

public class ChatPersonaService
{
    private readonly IChatPersonaRepository _chatPersonaRepository;
    private readonly UsuarioService _usuarioService;

    public ChatPersonaService(IChatPersonaRepository chatPersonaRepository, UsuarioService usuarioService)
    {
        _chatPersonaRepository = chatPersonaRepository;
        _usuarioService = usuarioService;
    }
    public void agregarPersonaChat(ChatPersonaFormDTO chatPersonaFormDTO, int idUsuario)
    {
        var idPersonas = chatPersonaFormDTO.IdPersonas;
        for (var i = 0; i < idPersonas.Count; i++)
        {
            var chatPersona = new ChatPersona
            {
                IdChat = chatPersonaFormDTO.IdChat,
                IdPersona = idPersonas[i],
                IdTipo = idPersonas[i] == idUsuario ? GeneralConstant.IdTipoUsuarioChatAdmin : chatPersonaFormDTO.IdTipo,
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
    public async Task<List<ChatPersonaSelectorDTO>> ObtenerPersonasChatSelector(int idChat)
    {
        var personas = await _chatPersonaRepository.ConsultarPersonasPorChatAsync(idChat);

        var chatPersonasDto = new List<ChatPersonaSelectorDTO>();

        foreach (var persona in personas)
        {
            var dto = new ChatPersonaSelectorDTO
            {
                IdUsuario = persona.IdPersona,
                Nombre = persona.IdPersonaNavigation.Nombre,
                ImagenBase64 = await _usuarioService.ObtenerBytesImagenUsuario(persona.IdPersona)
            };
            chatPersonasDto.Add(dto);
        }

        return chatPersonasDto;
    }

    public void AbandonarChat(int idChat, int idPersona)
    {
        _chatPersonaRepository.AbandonarChat(idChat, idPersona);
    }
}

