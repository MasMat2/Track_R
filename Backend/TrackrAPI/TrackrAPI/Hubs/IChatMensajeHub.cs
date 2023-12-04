using TrackrAPI.Models;

namespace TrackrAPI.Hubs;

public interface IChatMensajeHub
{
    Task NuevaConexion(IEnumerable<ChatMensaje> mensajes);
    Task NuevoMensaje(ChatMensaje mensaje);

}

