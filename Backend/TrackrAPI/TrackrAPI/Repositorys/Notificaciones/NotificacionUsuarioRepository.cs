using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public class NotificacionUsuarioRepository : Repository<NotificacionUsuario>, INotificacionUsuarioRepository
{
    public NotificacionUsuarioRepository(TrackrContext context) : base(context)
    {
    }

    public IEnumerable<NotificacionUsuarioDto> ConsultarParaSidebar(int idUsuario)
    {
        const int limite = 10;

        return context.NotificacionUsuario
            .Where(n => n.IdUsuario == idUsuario)
            .OrderByDescending(n => n.IdNotificacionNavigation.FechaAlta)
            .Take(limite)
            .Select(n => new NotificacionUsuarioDto
            {
                IdNotificacionUsuario = n.IdNotificacionUsuario,
                IdNotificacion = n.IdNotificacion,
                IdUsuario = n.IdUsuario,
                FechaAlta = n.IdNotificacionNavigation.FechaAlta,
                Origen = n.IdNotificacionNavigation.Origen ?? string.Empty,
                Descripcion = n.IdNotificacionNavigation.Descripcion,
                Visto = n.Visto
            })
            .ToList();
    }

    public void MarcarComoVistas(List<int> idNotificacionUsuario)
    {
        var notificacionesUsuario = context.NotificacionUsuario
            .Where(n => idNotificacionUsuario.Contains(n.IdNotificacionUsuario))
            .ToList();

        foreach (var notificacionUsuario in notificacionesUsuario)
        {
            notificacionUsuario.Visto = true;
            context.NotificacionUsuario.Update(notificacionUsuario);
        }

        context.SaveChanges();
    }
}