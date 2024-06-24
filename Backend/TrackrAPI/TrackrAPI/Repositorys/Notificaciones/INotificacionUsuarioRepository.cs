using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Notificaciones;

public interface INotificacionUsuarioRepository : IRepository<NotificacionUsuario>
{
    public IEnumerable<NotificacionPacienteDTO> ConsultarPorPaciente(int idUsuario);
    public Task<IEnumerable<NotificacionDoctorDTO>> ConsultarPorDoctor(int idUsuario);
    public void MarcarComoVistas(List<int> idNotificacionUsuario);
}