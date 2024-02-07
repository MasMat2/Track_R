using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Repositorys.Chats;

public class ChatPersonaRepository : Repository<ChatPersona>, IChatPersonaRepository
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;
    public ChatPersonaRepository(TrackrContext context,
                                 IUsuarioRepository usuarioRepository,
                                 IAsistenteDoctorRepository asistenteDoctorRepository) : base(context) { 
        _usuarioRepository = usuarioRepository;
        _asistenteDoctorRepository = asistenteDoctorRepository;
    }

    public IEnumerable<ChatPersona> ConsultarChatPersonas()
    {
        return context.ChatPersona.ToList();
    }

    public IEnumerable<ChatPersona> ConsultarPersonasPorChat(int IdChat)
    {
        return context.ChatPersona
                      .Include(x => x.IdPersonaNavigation)
                      .Where(x => x.IdChat == IdChat)
                      .ToList();
    }

    public IEnumerable<ChatPersona> ConsultarChatsPorPersona(int IdPersona)
    {
        //Agregar la logica del asistente
        var user = _usuarioRepository.ConsultarDto(IdPersona);
        var esAsistente = _usuarioRepository.ConsultarPorPerfil(user.IdCompania, GeneralConstant.ClavePerfilAsistente)
                                            .Any((usuario) => usuario.IdUsuario == user.IdUsuario);

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
        var idCreador =  context.ChatPersona
                      .Where(x => x.IdChat == idChat && x.IdTipoNavigation.Clave == GeneralConstant.ClaveTipoUsuarioChatAdmin)
                      .Select(x => x.IdPersona)
                      .FirstOrDefault();

        return idCreador;
    }
}
