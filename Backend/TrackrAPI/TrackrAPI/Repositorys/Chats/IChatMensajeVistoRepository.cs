using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public interface IChatMensajeVistoRepository : IRepository<ChatMensajeVisto>
{
    public IEnumerable<ChatMensajeVisto> ObtenerPersonasVistasPorMensaje(int IdMensaje);
}

