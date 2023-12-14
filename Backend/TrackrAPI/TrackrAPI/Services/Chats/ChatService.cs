using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.SignalR;
using MimeTypes;
using TrackrAPI.Helpers;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Chats;

public class ChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHub, IChatHub> hubContext;
    private readonly IChatPersonaRepository _chatPersonaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public ChatService(IChatRepository chatRepository,
                       IHubContext<ChatHub,
                       IChatHub> hubContext,
                       IChatPersonaRepository chatPersonaRepository,
                       IUsuarioRepository usuarioRepository)
    {
        _chatRepository = chatRepository;
        this.hubContext = hubContext;
        _chatPersonaRepository = chatPersonaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public Chat AgregarChat(Chat chat)
    {
        chat.Habilitado = true;
        chat.Fecha = new DateTime();

        _chatRepository.Agregar(chat);

        return chat;
    }

    public void NuevoChat(Chat chat,List<int> idPersonas)
    {
        _chatRepository.Agregar(chat);
        //await hubContext.Clients.All.NuevoChat(chat,idPersonas);
    }

    public IEnumerable<ChatDTO> ConsultarChats(int IdPersona)
    {
        var idChats = _chatPersonaRepository.ConsultarChatsPorPersona(IdPersona).Select(x => x.IdChat).ToList();
        var chatsDto = _chatRepository.ConsultarChats(idChats).Select(x => new ChatDTO
        {
            IdChat = x.IdChat,
            Titulo = x.Titulo,
            Fecha = x.Fecha,
            Habilitado = x.Habilitado
        }).ToList();

        foreach(var chat in chatsDto)
        {
            if(_chatPersonaRepository.ConsultarPersonasPorChat(chat.IdChat).ToList().Count == 2)
            {
                var personas = _chatPersonaRepository.ConsultarPersonasPorChat(chat.IdChat).Select(x => x.IdPersona).ToList();
                foreach(var persona in personas)
                {
                    if(persona != IdPersona)
                    {
                        var user = _usuarioRepository.Consultar(persona);
                        chat.Titulo = "";
                        chat.Titulo = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;
                        var usuario = _usuarioRepository.ConsultarDto(persona);
                        if(usuario != null)
                        {
                            if (!string.IsNullOrEmpty(usuario.ImagenTipoMime))
                            {
                                string filePath = $"Archivos/Usuario/{usuario.IdUsuario}{MimeTypeMap.GetExtension(usuario.ImagenTipoMime)}";
                                if (File.Exists(filePath))
                                {
                                    byte[] imageArray = File.ReadAllBytes(filePath);
                                    chat.ImagenBase64 = Convert.ToBase64String(imageArray);
                                    chat.TipoMime = usuario.ImagenTipoMime;
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                var personas = _chatPersonaRepository.ConsultarPersonasPorChat(chat.IdChat).Select(x => x.IdPersona).ToList();
                var idCreador = personas.Last();
                var usuario = _usuarioRepository.ConsultarDto(idCreador);
                if (usuario != null)
                {
                    if (!string.IsNullOrEmpty(usuario.ImagenTipoMime))
                    {
                        string filePath = $"Archivos/Usuario/{usuario.IdUsuario}{MimeTypeMap.GetExtension(usuario.ImagenTipoMime)}";
                        if (File.Exists(filePath))
                        {
                            byte[] imageArray = File.ReadAllBytes(filePath);
                            chat.ImagenBase64 = Convert.ToBase64String(imageArray);
                            chat.TipoMime = usuario.ImagenTipoMime;
                        }
                        chat.IdCreadorChat = idCreador;
                    }
                }
            }
            /*
            int idChatCreador = _chatPersonaRepository.ConsultarIdCreador(chat.IdChat);
            var usuarioCreador = _usuarioRepository.ConsultarDto(idChatCreador);
            if (usuarioCreador != null)
            {

                if (!string.IsNullOrEmpty(usuarioCreador.ImagenTipoMime))
                {
                    string filePath = $"Archivos/Usuario/{usuarioCreador.IdUsuario}{MimeTypeMap.GetExtension(usuarioCreador.ImagenTipoMime)}";
                    if (File.Exists(filePath))
                    {
                        byte[] imageArray = File.ReadAllBytes(filePath);
                        chat.ImagenBase64 = Convert.ToBase64String(imageArray);
                        chat.TipoMime = usuarioCreador.ImagenTipoMime;
                    }
                        chat.IdCreadorChat = idChatCreador;
                }
            }
            */
        }
        
        return chatsDto;
    }
}