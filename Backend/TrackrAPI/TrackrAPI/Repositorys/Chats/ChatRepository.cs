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

    public IEnumerable<Chat> ConsultarChats()
    {
        return context.Chat.ToList();
    }
}

