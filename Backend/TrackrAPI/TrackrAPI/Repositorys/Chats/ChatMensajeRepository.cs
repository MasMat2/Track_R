using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public class ChatMensajeRepository : Repository<ChatMensaje>, IChatMensajeRepository
{
    public ChatMensajeRepository(TrackrContext context) : base(context) { }

    public IEnumerable<ChatMensaje> ObtenerMensajePorChat(int IdChat)
    {
        return context.ChatMensaje
                      .Include(x => x.IdPersonaNavigation)
                      .Where(x => x.IdChat == IdChat);
    }
}

