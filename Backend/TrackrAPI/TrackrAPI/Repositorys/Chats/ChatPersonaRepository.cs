using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public class ChatPersonaRepository : Repository<ChatPersona>, IChatPersonaRepository
{
    public ChatPersonaRepository(TrackrContext context) : base(context) { }

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
