using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Services.Archivos;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Services.Chats;

public class ChatMensajeService
{
    private readonly IChatMensajeRepository _chatMensajeRepository;
    private readonly IHubContext<ChatMensajeHub,IChatMensajeHub> _hubContext;
    private readonly IChatPersonaRepository _chatPersonaRepository;
    private readonly NotificacionDoctorService _notificacionService;
    private readonly IArchivoRepository _archivoRepository;
    private readonly ArchivoService _archivoService;

    public ChatMensajeService(IChatMensajeRepository chatMensajeRepository,
                              IHubContext<ChatMensajeHub,IChatMensajeHub> hubContext,
                              IChatPersonaRepository chatPersonaRepository,
                              NotificacionDoctorService notificacionService,
                              IArchivoRepository archivoRepository,
                              ArchivoService archivoService)
    {
        _chatMensajeRepository = chatMensajeRepository;
        _hubContext = hubContext;
        _chatPersonaRepository = chatPersonaRepository;
        _notificacionService = notificacionService;
        _archivoRepository = archivoRepository;
        _archivoService = archivoService;
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
                                                NombrePersona = x.IdPersonaNavigation.Nombre + "  "+ x.IdPersonaNavigation.ApellidoPaterno + " "+x.IdPersonaNavigation.ApellidoMaterno,
                                                IdArchivo = (x.IdArchivo != null) ? (int) x.IdArchivo:0,
                                                ArchivoNombre = (x.IdArchivo != null) ? _archivoService.GetFileName((int)x.IdArchivo):null,

                                            });
           
            chats = chats.Append(aux);
        }
        
        return chats;
    }

    public int NuevoMensaje(ChatMensajeDTO mensaje)
    {
        int? idArchivo = null;
        var idsPersonasChat = _chatPersonaRepository.ConsultarPersonasPorChat(mensaje.IdChat)
                                                    .Where( cP => cP.IdPersona != mensaje.IdPersona)
                                                    .Select(x => x.IdPersona)
                                                    .Distinct()
                                                    .ToList();
        var notificacion = new NotificacionDoctorCapturaDTO(mensaje.Mensaje , 2 , mensaje.IdPersona,mensaje.IdPersona,mensaje.IdChat);
        
        _notificacionService.Notificar(notificacion, idsPersonasChat);
        
        //Subir si existe el archivo
        if (mensaje.ArchivoTipoMime != null)
        {

            var archivo = new Archivo
            {
                Archivo1 = Convert.FromBase64String(mensaje.Archivo.Substring(mensaje.Archivo.LastIndexOf(',') +1)),
                ArchivoNombre = mensaje.ArchivoNombre,
                ArchivoTipoMime = mensaje.ArchivoTipoMime,
                FechaRealizacion = (DateTime)mensaje.FechaRealizacion,
                IdUsuario = mensaje.IdPersona,
                Nombre = mensaje.Nombre
            };

            _archivoRepository.Agregar(archivo);
            idArchivo = archivo.IdArchivo;
        }

        var mensajeAux = new ChatMensaje
        {
            Fecha = mensaje.Fecha,
            Mensaje = mensaje.Mensaje,
            IdChat = mensaje.IdChat,
            IdPersona = mensaje.IdPersona,
        };


        if(idArchivo != null)
        {
            mensajeAux.IdArchivo = idArchivo;
        }

        _chatMensajeRepository.Agregar(mensajeAux);

        return (idArchivo != null) ? (int)idArchivo:0;
    }
}

