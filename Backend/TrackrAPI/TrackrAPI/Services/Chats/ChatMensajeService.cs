﻿using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Archivos;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Hubs;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Archivos;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Services.Archivos;
using TrackrAPI.Services.Notificaciones;
using TrackrAPI.Helpers;
using MimeTypes;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Services.Sftp;
using System.Transactions;
using TrackrAPI.Repositorys.Notificaciones;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Chats;

public class ChatMensajeService
{
    private readonly IChatMensajeRepository _chatMensajeRepository;
    private readonly IHubContext<ChatMensajeHub, IChatMensajeHub> _hubContext;
    private readonly IChatPersonaRepository _chatPersonaRepository;
    private readonly NotificacionDoctorService _notificacionDoctorService;
    private readonly NotificacionPacienteService _notificacionPacienteService;
    private readonly IArchivoRepository _archivoRepository;
    private readonly ArchivoService _archivoService;
    private readonly SimpleAES _simpleAES;
    private readonly SftpService _sftpService;
    private readonly ITipoNotificacionRepository _tipoNotificacionRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public ChatMensajeService(IChatMensajeRepository chatMensajeRepository,
                              IHubContext<ChatMensajeHub, IChatMensajeHub> hubContext,
                              IChatPersonaRepository chatPersonaRepository,
                              NotificacionDoctorService notificacionService,
                              IArchivoRepository archivoRepository,
                              ArchivoService archivoService,
                              SimpleAES simpleAES,
                              SftpService sftpService,
                              ITipoNotificacionRepository tipoNotificacionRepository,
                              NotificacionPacienteService notificacionPacienteService,
                              IUsuarioRepository usuarioRepository)
    {
        _chatMensajeRepository = chatMensajeRepository;
        _hubContext = hubContext;
        _chatPersonaRepository = chatPersonaRepository;
        _notificacionDoctorService = notificacionService;
        _archivoRepository = archivoRepository;
        _archivoService = archivoService;
        _simpleAES = simpleAES;
        _sftpService = sftpService;
        _tipoNotificacionRepository = tipoNotificacionRepository;
        _notificacionPacienteService = notificacionPacienteService;
        _usuarioRepository = usuarioRepository;
    }

    public IEnumerable<IEnumerable<ChatMensajeDTO>> ObtenerMensajesPorChat(int IdPersona)
    {
        var idChats = _chatPersonaRepository.ConsultarChatsPorPersona(IdPersona);
        IEnumerable<IEnumerable<ChatMensajeDTO>> chats = new List<IEnumerable<ChatMensajeDTO>>();

        foreach (var idChat in idChats)
        {
            var aux = _chatMensajeRepository.ObtenerMensajePorChat(idChat.IdChat)
                                            .Select(x => new ChatMensajeDTO
                                            {
                                                IdChatMensaje = x.IdChatMensaje,
                                                IdChat = x.IdChat,
                                                Fecha = x.Fecha,
                                                IdPersona = x.IdPersona,
                                                Mensaje = _simpleAES.DecryptString(x.Mensaje),
                                                NombrePersona = x.IdPersonaNavigation.Nombre + "  " + x.IdPersonaNavigation.ApellidoPaterno + " " + x.IdPersonaNavigation.ApellidoMaterno,
                                                IdArchivo = (x.IdArchivo != null) ? (int)x.IdArchivo : 0,
                                                ArchivoNombre = (x.IdArchivo != null) ? _archivoService.GetFileName((int)x.IdArchivo) : null,
                                                ArchivoTipoMime = (x.IdArchivo != null) ? _archivoService.GetFileMime((int)x.IdArchivo) : null

                                            });

            chats = chats.Append(aux);
        }

        return chats;
    }

    public async Task<int> NuevoMensaje(ChatMensajeDTO mensaje)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, 
                                                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                                                TransactionScopeAsyncFlowOption.Enabled))
        {

        int? idArchivo = null;

        var usuarioEmisor = _usuarioRepository.Consultar(mensaje.IdPersona);
        var idsPacientesChat = _chatPersonaRepository.ConsultarPersonasPorChat(mensaje.IdChat)
                                                    .Where(cP => cP.IdPersona != usuarioEmisor.IdUsuario && cP.IdPersonaNavigation.IdPerfilNavigation.Clave == GeneralConstant.ClavePerfilPaciente)
                                                    .Select(x => x.IdPersona)
                                                    .Distinct()
                                                    .ToList();

        var idsMedicosChat = _chatPersonaRepository.ConsultarPersonasPorChat(mensaje.IdChat)
                                                    .Where(cP => cP.IdPersona != usuarioEmisor.IdUsuario && cP.IdPersonaNavigation.IdPerfilNavigation.Clave == GeneralConstant.ClavePerfilMedico)
                                                    .Select(x => x.IdPersona)
                                                    .Distinct()
                                                    .ToList();

        int idTipoNotificacion;
        string claveTipoNotificacion;

        if(mensaje.EsVideoChat == true)
        {
            var notificacionVideo = _tipoNotificacionRepository.ConsultarPorClave(GeneralConstant.ClaveNotificacionVideo);
            idTipoNotificacion = notificacionVideo.IdTipoNotificacion;
            claveTipoNotificacion = GeneralConstant.ClaveNotificacionVideo;


        }
        else
        {
            var notificacionChat = _tipoNotificacionRepository.ConsultarPorClave(GeneralConstant.ClaveNotificacionChat);
            idTipoNotificacion = notificacionChat.IdTipoNotificacion;
            claveTipoNotificacion = GeneralConstant.ClaveNotificacionChat;
        }

     
        var notificacionDoctor = new NotificacionDoctorCapturaDTO(mensaje.Mensaje, null, idTipoNotificacion, mensaje.IdPersona, mensaje.IdPersona, mensaje.IdChat, null);
        var notificacionPaciente = new NotificacionCapturaDTO(usuarioEmisor.ObtenerNombreCompleto(), mensaje.Mensaje, null, idTipoNotificacion, mensaje.IdPersona, mensaje.IdChat, null);

        await _notificacionDoctorService.Notificar(notificacionDoctor, idsMedicosChat);
        await _notificacionPacienteService.Notificar(notificacionPaciente, idsPacientesChat);

        //Subir si existe el archivo
        if (mensaje.ArchivoTipoMime != null)
        {
            idArchivo = this.GuardarArchivo(mensaje.Archivo, mensaje.ArchivoNombre, mensaje.ArchivoTipoMime, mensaje.IdPersona);

            /*var archivo = new Archivo
            {
                Archivo1 = Convert.FromBase64String(mensaje.Archivo.Substring(mensaje.Archivo.LastIndexOf(',') + 1)),
                ArchivoNombre = mensaje.ArchivoNombre,
                ArchivoTipoMime = mensaje.ArchivoTipoMime,
                FechaRealizacion = (DateTime)mensaje.FechaRealizacion,
                IdUsuario = mensaje.IdPersona,
                Nombre = mensaje.Nombre
            };

            _archivoRepository.Agregar(archivo);
            idArchivo = archivo.IdArchivo;*/
        }

        var mensajeAux = new ChatMensaje
        {
            Fecha = mensaje.Fecha,
            Mensaje = mensaje.Mensaje,
            IdChat = mensaje.IdChat,
            IdPersona = mensaje.IdPersona,
        };


        var encriptedString = _simpleAES.EncryptToString(mensajeAux.Mensaje);
        mensajeAux.Mensaje = encriptedString;

        if (idArchivo != null)
        {
            mensajeAux.IdArchivo = idArchivo;
        }

        _chatMensajeRepository.Agregar(mensajeAux);
        scope.Complete();

        return (idArchivo != null) ? (int)idArchivo : 0;
        }
    }

    public int GuardarArchivo(string archivo,string nombre,string tipoMime, int idUsuario)
    {

        
            string nombreArchivo = $"{nombre}";
            string path = Path.Combine("Archivos", "Chat", nombreArchivo);
            var archivoBase64 = archivo.Substring(archivo.LastIndexOf(',') + 1);

            _sftpService.UploadBytesFile(path, archivoBase64);

            //Logica para agregar las fotos de perfil en la tabla archivo
            var archivoMensaje = new Archivo
            {
                Nombre = nombreArchivo,
                ArchivoNombre = nombreArchivo,
                ArchivoTipoMime = tipoMime,
                ArchivoUrl = path,
                FechaRealizacion = DateTime.Now,
                IdUsuario = idUsuario
            };


            var fileUploaded = _archivoRepository.Agregar(archivoMensaje);

        return fileUploaded.IdArchivo;


        
    }
}

