using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public interface IChatPersonaRepository : IRepository<ChatPersona>
{
    public IEnumerable<ChatPersona> ConsultarChatPersonas();
    public IEnumerable<ChatPersona> ConsultarPersonasPorChat(int IdChat);
    public IEnumerable<ChatPersona> ConsultarChatsPorPersona(int IdPersona);
}

