using TrackrAPI.Dtos.Chats;
using TrackrAPI.Models;

namespace TrackrAPI.Hubs;

public interface IChatMensajeHub
{
    Task NuevaConexion(IEnumerable<IEnumerable<ChatMensajeDTO>> mensajes);
    Task NuevoMensaje(ChatMensajeDTO mensaje);

}

