using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Notificaciones;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionUsuarioService
{
    private readonly INotificacionUsuarioRepository _notificacionUsuarioRepository;

    public NotificacionUsuarioService(INotificacionUsuarioRepository notificacionUsuarioRepository)
    {
        _notificacionUsuarioRepository = notificacionUsuarioRepository;
    }

    private NotificacionUsuario GenerarNotificacionUsuario(int idNotificacion, int idUsuario)
    {
        var notificacionUsuario = new NotificacionUsuario
        {
            IdNotificacion = idNotificacion,
            IdUsuario = idUsuario,
            Visto = false
        };

        return notificacionUsuario;
    }

    private NotificacionUsuarioDto Mapear(NotificacionUsuario notificacionUsuario)
    {
        var notificacionUsuarioDto = new NotificacionUsuarioDto(
            notificacionUsuario.IdNotificacionUsuario,
            notificacionUsuario.IdNotificacion,
            notificacionUsuario.IdUsuario,
            notificacionUsuario.Visto);

        return notificacionUsuarioDto;
    }

    public IEnumerable<NotificacionPacienteDTO> ConsultarPorPaciente(int idUsuario)
    {
        return _notificacionUsuarioRepository.ConsultarPorPaciente(idUsuario);
    }

    public async Task<IEnumerable<NotificacionDoctorDTO>> ConsultarPorDoctor(int idUsuario)
    {
        return await _notificacionUsuarioRepository.ConsultarPorDoctor(idUsuario);
    }

    public NotificacionUsuarioDto Agregar(int idNotificacion, int idUsuario)
    {
        var notificacionUsuario = GenerarNotificacionUsuario(idNotificacion, idUsuario);

        _notificacionUsuarioRepository.Agregar(notificacionUsuario);

        return Mapear(notificacionUsuario);
    }

    public IEnumerable<NotificacionUsuarioDto> Agregar(int idNotificacion, List<int> idsUsuario)
    {
        var notificacionesUsuario = idsUsuario
            .Select((idUsuario) => GenerarNotificacionUsuario(idNotificacion, idUsuario));

        _notificacionUsuarioRepository.Agregar(notificacionesUsuario);

        return notificacionesUsuario.Select(nu => Mapear(nu));
    }

    public void MarcarComoVistas(List<int> idNotificacionUsuario)
    {
        _notificacionUsuarioRepository.MarcarComoVistas(idNotificacionUsuario);
    }
}