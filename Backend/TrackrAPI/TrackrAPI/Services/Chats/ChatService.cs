﻿using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.SignalR;
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

    public IEnumerable<Chat> ConsultarChats(int IdPersona)
    {
        var idChats = _chatPersonaRepository.ConsultarChatsPorPersona(IdPersona).Select(x => x.IdChat).ToList();
        var chats = _chatRepository.ConsultarChats(idChats);

        foreach(var chat in chats)
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
                    }
                }
            }
        }

        return chats;
    }
}