using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public class NotificacionUsuarioRepository : Repository<NotificacionUsuario>, INotificacionUsuarioRepository
{
    public NotificacionUsuarioRepository(TrackrContext context) : base(context)
    {
    }

    private IQueryable<NotificacionUsuario> ConsultarPorUsuario(int idUsuario)
    {
        const int limite = 10;

        return context.NotificacionUsuario
            .Where(n => n.IdUsuario == idUsuario)
            .OrderByDescending(n => n.IdNotificacionNavigation.FechaAlta)
            .Take(limite);
    }

    public IEnumerable<NotificacionPacienteDTO> ConsultarPorPaciente(int idUsuario)
    {
        return ConsultarPorUsuario(idUsuario)
            .Select(nu => new NotificacionPacienteDTO(
                nu.IdNotificacionUsuario,
                nu.IdNotificacion,
                nu.IdUsuario,
                nu.IdNotificacionNavigation.Titulo,
                nu.IdNotificacionNavigation.Mensaje,
                nu.IdNotificacionNavigation.FechaAlta,
                nu.Visto,
                nu.IdNotificacionNavigation.IdTipoNotificacion
            ));
    }

    public IEnumerable<NotificacionDoctorDTO> ConsultarPorDoctor(int idUsuario)
    {
        var notificacionesUsuario = ConsultarPorUsuario(idUsuario)
            .Include(n => n.IdNotificacionNavigation.NotificacionDoctor)
            .AsEnumerable();

        return notificacionesUsuario
            .Select(nu => new NotificacionDoctorDTO(
                nu.IdNotificacionUsuario,
                nu.IdNotificacion,
                nu.IdUsuario,
                nu.IdNotificacionNavigation.Titulo,
                nu.IdNotificacionNavigation.Mensaje,
                nu.IdNotificacionNavigation.FechaAlta,
                nu.Visto,
                nu.IdNotificacionNavigation.IdTipoNotificacion,
                nu.IdNotificacionNavigation.NotificacionDoctor.FirstOrDefault()?.IdPaciente ?? 0
            ));
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