using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public interface IChatPersonaRepository : IRepository<ChatPersona>
{
    public IEnumerable<ChatPersona> ConsultarChatPersonas();
    public IEnumerable<ChatPersona> ConsultarPersonasPorChat(int IdChat);
    public Task<IEnumerable<ChatPersona>> ConsultarPersonasPorChatAsync(int IdChat);
    public IEnumerable<ChatPersona> ConsultarChatsPorPersona(int IdPersona);
    public List<int> ObtenerPacientesPorPadecimiento(int IdPadecimiento);
    public int ConsultarIdCreador(int idChat);
    public void AbandonarChat(int idChat, int idPersona);
}

