using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public interface IChatRepository : IRepository<Chat>
{
    public Chat? Consultar(int IdChat);
    public IEnumerable<Chat> ConsultarChats(int idPersona);
}

