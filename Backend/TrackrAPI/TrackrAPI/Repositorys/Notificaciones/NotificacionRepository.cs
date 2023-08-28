using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public class NotificacionRepository : Repository<Notificacion>, INotificacionRepository
{
    public NotificacionRepository(TrackrContext context) : base(context)
    {
    }

    public Notificacion? Consultar(int idNotificacion)
    {
        return context.Notificacion
            .Where(n => n.IdNotificacion == idNotificacion)
            .FirstOrDefault();
    }
}