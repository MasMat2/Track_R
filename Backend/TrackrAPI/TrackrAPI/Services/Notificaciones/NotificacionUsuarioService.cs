using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Notificaciones;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Services.Notificaciones;

public class NotificacionUsuarioService
{
    private readonly INotificacionUsuarioRepository _notificacionUsuarioRepository;
    private readonly UsuarioService _usuarioService;
    private readonly IAsistenteDoctorRepository _asistenteDoctorRepository;

    public NotificacionUsuarioService(INotificacionUsuarioRepository notificacionUsuarioRepository, UsuarioService usuarioService, IAsistenteDoctorRepository asistenteDoctorRepository)
    {
        _notificacionUsuarioRepository = notificacionUsuarioRepository;
        _usuarioService = usuarioService;
        _asistenteDoctorRepository = asistenteDoctorRepository;
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

    public async Task<NotificacionUsuarioDto> ConsultarDto(int idNotificacionUsuario)
    {
        var notificacionUsuario = await  _notificacionUsuarioRepository.Consultar(idNotificacionUsuario);
        notificacionUsuario.IdUsuario = (int) notificacionUsuario.IdNotificacionNavigation.IdPersona;
        return Mapear(notificacionUsuario);
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
        int idCompania = _usuarioService.ConsultarDto(idUsuario).IdCompania;
        bool esAsistente = _usuarioService.EsAsistente(idCompania, idUsuario);

        List<int> idDoctorList = new();
        if(esAsistente)
        {
            idDoctorList = _asistenteDoctorRepository.ConsultarDoctoresPorAsistente(idUsuario)
                                                     .Select(ad => ad.IdUsuario).ToList();
                                                     
        }else{
            idDoctorList.Add(idUsuario);
        }
        return await _notificacionUsuarioRepository.ConsultarPorDoctor(idDoctorList);
    }

    public NotificacionUsuarioDto Agregar(int idNotificacion, int idUsuario)
    {
        var notificacionUsuario = GenerarNotificacionUsuario(idNotificacion, idUsuario);

        var notificacionAgregada =_notificacionUsuarioRepository.Agregar(notificacionUsuario);
        notificacionUsuario.IdNotificacionUsuario = notificacionAgregada.IdNotificacionUsuario;

        return Mapear(notificacionUsuario);
    }

    public IEnumerable<NotificacionUsuarioDto> Agregar(int idNotificacion, List<int> idsUsuario)
    {
        var notificacionesUsuario = idsUsuario
            .Select((idUsuario) => GenerarNotificacionUsuario(idNotificacion, idUsuario));
            
        List<NotificacionUsuarioDto> notificacionesUsuarioDto = new();

        foreach(var notificacionUsuario in notificacionesUsuario)
        {
            var notificacionAgregada = _notificacionUsuarioRepository.Agregar(notificacionUsuario);
            
            notificacionesUsuarioDto.Add(Mapear(notificacionAgregada));
        }

   
        return notificacionesUsuarioDto;
    }

    public void MarcarComoVistas(List<int> idNotificacionUsuario, bool tomaTomada)
    {
        _notificacionUsuarioRepository.MarcarComoVistas(idNotificacionUsuario, tomaTomada);
    }
}