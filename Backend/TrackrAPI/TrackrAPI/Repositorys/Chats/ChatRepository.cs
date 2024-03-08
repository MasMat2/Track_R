using DocumentFormat.OpenXml.InkML;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Chats;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(TrackrContext context) : base(context)
    {

    }

    public Chat? Consultar(int IdChat)
    {
        return context.Chat
                      .Where(chat => chat.IdChat == IdChat)
                      .FirstOrDefault();
    }

    public IEnumerable<Chat> ConsultarChats(List<int> idChats)
    {

        return context.Chat.Where(x => idChats.Contains(x.IdChat)).ToList();

    }

    public void EliminarChat(int idChat)
    {
        // Obtener todas las notificaciones relacionadas con el chat
        var notificaciones = context.Notificacion.Where(x => x.IdChat == idChat).ToList();

        foreach (var notificacion in notificaciones)
        {
            // Eliminar las notificaciones relacionadas con pacientes
            var notificacionesPaciente = context.NotificacionUsuario.Where(np => np.IdNotificacion == notificacion.IdNotificacion).ToList();
            context.NotificacionUsuario.RemoveRange(notificacionesPaciente);

            // Eliminar las notificaciones relacionadas con doctores
            var notificacionesDoctor = context.NotificacionDoctor.Where(nd => nd.IdNotificacion == notificacion.IdNotificacion).ToList();
            context.NotificacionDoctor.RemoveRange(notificacionesDoctor);
        }

        // Eliminar las notificaciones principales
        context.Notificacion.RemoveRange(notificaciones);

        // Eliminar los mensajes, personas y el chat
        var mensajes = context.ChatMensaje.Where(x => x.IdChat == idChat).ToList();
        var personas = context.ChatPersona.Where(x => x.IdChat == idChat).ToList();
        var chat = context.Chat.Where(x => x.IdChat == idChat).SingleOrDefault();

        if (chat != null)
        {
            context.ChatMensaje.RemoveRange(mensajes);
            context.ChatPersona.RemoveRange(personas);
            context.Chat.Remove(chat);

            context.SaveChanges();
        }
    }
}

