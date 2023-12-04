using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public class ChatMensajeVistoRepository : Repository<ChatMensajeVisto>, IChatMensajeVistoRepository
{
    public ChatMensajeVistoRepository(TrackrContext context) : base(context) { }

    public IEnumerable<ChatMensajeVisto> ObtenerPersonasVistasPorMensaje(int IdMensaje)
    {
        return context.ChatMensajeVisto
                      .Where(x => x.IdMensaje == IdMensaje)
                      .ToList();
    }
}

