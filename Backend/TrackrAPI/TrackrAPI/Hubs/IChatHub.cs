using TrackrAPI.Models;

namespace TrackrAPI.Hubs;

public interface IChatHub
{
    Task NuevaConexion(IEnumerable<ChatDTO> chats);
    Task NuevoChat(Chat chat, List<int> idPersonas);
    Task CargarChats(IEnumerable<ChatDTO> chats);
    Task EliminarChat(int idChat);
}

