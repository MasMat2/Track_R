using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Services.Chats;

public class ChatMensajeService
{
    private readonly IChatMensajeRepository _chatMensajeRepository;
    private readonly IHubContext<ChatMensajeHub,IChatMensajeHub> _hubContext;
    private readonly IChatPersonaRepository _chatPersonaRepository;
    private readonly NotificacionDoctorService _notificacionService;

    public ChatMensajeService(IChatMensajeRepository chatMensajeRepository,
                              IHubContext<ChatMensajeHub,IChatMensajeHub> hubContext,
                              IChatPersonaRepository chatPersonaRepository,
                              NotificacionDoctorService notificacionService)
    {
        _chatMensajeRepository = chatMensajeRepository;
        _hubContext = hubContext;
        _chatPersonaRepository = chatPersonaRepository;
        _notificacionService = notificacionService;
    }

    public IEnumerable<IEnumerable<ChatMensajeDTO>> ObtenerMensajesPorChat(int IdPersona)
    {
        var idChats = _chatPersonaRepository.ConsultarChatsPorPersona(IdPersona);
        IEnumerable<IEnumerable<ChatMensajeDTO>> chats = new List<IEnumerable<ChatMensajeDTO>>();

        foreach(var idChat in idChats) {
            var aux = _chatMensajeRepository.ObtenerMensajePorChat(idChat.IdChat)
                                            .Select(x => new ChatMensajeDTO
                                            {
                                                IdChatMensaje = x.IdChatMensaje,
                                                IdChat = x.IdChat,
                                                Fecha = x.Fecha,
                                                IdPersona = x.IdPersona,
                                                Mensaje = x.Mensaje,
                                                NombrePersona = x.IdPersonaNavigation.Nombre + "  "+ x.IdPersonaNavigation.ApellidoPaterno + " "+x.IdPersonaNavigation.ApellidoMaterno
                                            });
            chats = chats.Append(aux);
        }
        
        return chats;
    }

    public async Task NuevoMensaje(ChatMensaje mensaje)
    {
        var idsPersonasChat = _chatPersonaRepository.ConsultarPersonasPorChat(mensaje.IdChat)
                                                    .Select(x => x.IdPersona)
                                                    .Distinct()
                                                    .ToList();
        var notificacion = new NotificacionDoctorCapturaDTO(mensaje.Mensaje , 2 , mensaje.IdPersona);
        
        await _notificacionService.Notificar(notificacion, idsPersonasChat);

        _chatMensajeRepository.Agregar(mensaje);
    }
}

