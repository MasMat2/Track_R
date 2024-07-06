using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public interface ITipoNotificacionRepository : IRepository<TipoNotificacion>
{

    public TipoNotificacion ConsultarPorClave(string clave);
}