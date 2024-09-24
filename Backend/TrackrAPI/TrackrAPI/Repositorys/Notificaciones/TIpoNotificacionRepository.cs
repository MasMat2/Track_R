using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public class TipoNotificacionRepository : Repository<TipoNotificacion>, ITipoNotificacionRepository
{
    public TipoNotificacionRepository(TrackrContext context) : base(context)
    {
    }

    public TipoNotificacion ConsultarPorClave(string clave)
    {
        return context.TipoNotificacion
            .Where( tn => tn.Clave == clave)
            .FirstOrDefault();
    }

    public TipoNotificacion ConsultarPorId(int idTipoNotificacion)
    {
        return context.TipoNotificacion
            .Where( tn => tn.IdTipoNotificacion == idTipoNotificacion)
            .FirstOrDefault();
    }
}