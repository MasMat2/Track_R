using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public interface INotificacionRepository : IRepository<Notificacion>
{
    public Notificacion? Consultar(int idNotificacion);
}