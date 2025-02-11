﻿using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Repositorys.Chats;

public class ChatPersonaRepository : Repository<ChatPersona>, IChatPersonaRepository
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly UsuarioService _usuarioService;
    private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;
    public ChatPersonaRepository(TrackrContext context,
                                 IUsuarioRepository usuarioRepository,
                                 IAsistenteDoctorRepository asistenteDoctorRepository,
                                 UsuarioService usuarioService) : base(context)
                        
    {
        _usuarioRepository = usuarioRepository;
        _asistenteDoctorRepository = asistenteDoctorRepository;
        _usuarioService = usuarioService;
    }

    public IEnumerable<ChatPersona> ConsultarChatPersonas()
    {
        return context.ChatPersona.ToList();
    }

    public IEnumerable<ChatPersona> ConsultarPersonasPorChat(int IdChat)
    {
        return context.ChatPersona
                      .Include(x => x.IdPersonaNavigation)
                      .ThenInclude( p => p.IdPerfilNavigation)
                      .Where(x => x.IdChat == IdChat)
                      .ToList();
    }
    public async Task<IEnumerable<ChatPersona>> ConsultarPersonasPorChatAsync(int IdChat)
    {
        return  await context.ChatPersona
                      .Include(x => x.IdPersonaNavigation)
                      .Where(x => x.IdChat == IdChat)
                      .ToListAsync();
    }

    public IEnumerable<ChatPersona> ConsultarChatsPorPersona(int IdPersona)
    {
        //Agregar la logica del asistente
        var user = _usuarioRepository.ConsultarDto(IdPersona);
        var esAsistente = _usuarioService.EsAsistente(user.IdCompania , user.IdUsuario);

        if (esAsistente)
        {
            var idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(user.IdUsuario)
                                                          .Select(ad => ad.IdUsuario).ToList();
            idDoctorList.Add(user.IdUsuario);
            return context.ChatPersona
                      .Where(x => idDoctorList.Contains(x.IdPersona))
                      .ToList();
        }

        return context.ChatPersona
                      .Where(x => x.IdPersona == IdPersona)
                      .ToList();
    }

    public List<int> ObtenerPacientesPorPadecimiento(int IdPadecimiento)
    {
        return context.ExpedientePadecimiento
                      .Where(x => x.IdPadecimiento == IdPadecimiento)
                      .Include(x => x.IdExpedienteNavigation)
                      .Select(x => x.IdExpedienteNavigation.IdUsuario)
                      .ToList();
    }
    public int ConsultarIdCreador(int idChat)
    {
        var idCreador = context.ChatPersona
                      .Where(x => x.IdChat == idChat && x.IdTipoNavigation.Clave == GeneralConstant.ClaveTipoUsuarioChatAdmin)
                      .Select(x => x.IdPersona)
                      .FirstOrDefault();

        return idCreador;
    }

    public void AbandonarChat(int idChat, int idPersona)
    {
        var persona = context.ChatPersona
                               .Where(x => x.IdChat == idChat && x.IdPersona == idPersona)
                               .FirstOrDefault();

        if (persona != null)
        {
            context.ChatPersona.Remove(persona);
            context.SaveChanges();
        }
    }
}
