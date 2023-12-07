using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(TrackrContext context) : base(context)
    {

    }

    public Chat ? Consultar (int IdChat)
    {
        return context.Chat
                      .Where(chat => chat.IdChat == IdChat)
                      .FirstOrDefault();
    }

    public IEnumerable<Chat> ConsultarChats(int idPersona)
    {
        var idChats = context.ChatPersona.Where(x => x.IdPersona == idPersona).Select(x => x.IdChat).ToList();
        return context.Chat.Where(x => idChats.Contains(x.IdChat)).ToList();
    }
}

