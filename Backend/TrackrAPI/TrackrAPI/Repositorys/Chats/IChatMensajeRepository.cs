using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public interface IChatMensajeRepository : IRepository<ChatMensaje>
{
    public IEnumerable<ChatMensaje> ObtenerMensajePorChat(int IdChat);
}

