using TrackrAPI.Models;

namespace TrackrAPI.Hubs;

public interface IChatHub
{
    Task NuevaConexion(IEnumerable<Chat> chats);
    Task NuevoChat(Chat chat);
}

