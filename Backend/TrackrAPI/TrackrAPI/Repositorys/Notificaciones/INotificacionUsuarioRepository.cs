using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public interface INotificacionUsuarioRepository : IRepository<NotificacionUsuario>
{
    public IEnumerable<NotificacionUsuarioDto> ConsultarParaSidebar(int idUsuario);
    public void MarcarComoVistas(List<int> idNotificacionUsuario);
}