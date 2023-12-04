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
                      .Where(x => x.IdChat == IdChat)
                      .ToList();
    }

    public IEnumerable<ChatPersona> ConsultarChatsPorPersona(int IdPersona)
    {
        return context.ChatPersona
                      .Where(x => x.IdPersona == IdPersona)
                      .ToList();
    }
}
