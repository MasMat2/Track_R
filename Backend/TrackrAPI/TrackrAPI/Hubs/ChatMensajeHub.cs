using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using TrackrAPI.Dtos.Chats;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Chats;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Chats;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Hubs;

public class ChatMensajeHub : Hub<IChatMensajeHub>
{
    private readonly ChatMensajeService _chatMensajeService;
    private readonly UsuarioService _usuarioService;
    private readonly IChatPersonaRepository _chatPersonaRepository;
    private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;
    public ChatMensajeHub(ChatMensajeService chatMensajeService,
                          UsuarioService usuarioService,
                          IChatPersonaRepository chatPersonaRepository,
                          IAsistenteDoctorRepository asistenteDoctorRepository)
    {
        _chatMensajeService = chatMensajeService;
        _usuarioService = usuarioService;
        _chatPersonaRepository = chatPersonaRepository;
        _asistenteDoctorRepository = asistenteDoctorRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var idUsuario = ObtenerIdUsuario();
        var mensajes = _chatMensajeService.ObtenerMensajesPorChat(idUsuario);
        await Clients.Caller.NuevaConexion(mensajes);
    }

    public async Task NuevoMensaje(ChatMensajeDTO mensaje)
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted })){

        mensaje.IdPersona = this.ObtenerIdUsuario();
        int idArchivo = await _chatMensajeService.NuevoMensaje(mensaje);

        var user = _usuarioService.ConsultarDto(mensaje.IdPersona);
        mensaje.NombrePersona = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;
        mensaje.IdArchivo = idArchivo;

        var idPersonas = _chatPersonaRepository.ConsultarPersonasPorChat(mensaje.IdChat).Select(x => x.IdPersona).ToList();

        foreach (var persona in idPersonas)
        {
            await Clients.User(persona.ToString()).NuevoMensaje(mensaje);

            var idAsistentes = _asistenteDoctorRepository.ConsultarAsistentesPorDoctor(persona).Select(x => x.IdUsuario).ToList();

            if (idAsistentes != null)
            {
                foreach (var asistente in idAsistentes)
                {
                    await Clients.User(asistente.ToString()).NuevoMensaje(mensaje);
                }
            }
        }
        scope.Complete();
        }


    }

    public async Task AbandonarChat(int idChat)
    {
        var idPersona = this.ObtenerIdUsuario();

        _chatPersonaRepository.AbandonarChat(idChat, idPersona);

        await Clients.Caller.AbandonarChat(idChat);
    }

    private int ObtenerIdUsuario()
    {
        if (Context.UserIdentifier is null)
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        if (!int.TryParse(Context.UserIdentifier, out int idUsuario))
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        return idUsuario;
    }
}

