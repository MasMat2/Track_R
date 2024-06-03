using Microsoft.EntityFrameworkCore;
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

    public string? ConsultarClave(int idNotificacion)
    {
        return context.Notificacion
            .Where(n => n.IdNotificacion == idNotificacion)
            .Include(n => n.IdTipoNotificacionNavigation)
            .Select( n => n.IdTipoNotificacionNavigation.Clave)
            .FirstOrDefault();
    }
}