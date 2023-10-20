using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Notificaciones;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Services.Notificaciones;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteRecomendacionGeneralService
{
    private readonly IExpedienteRecomendacionRepository _expedienteRecomendacionRepository;
    private readonly ExpedienteRecomendacionValidatorService _expedienteRecomendacionValidator;
    private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly NotificacionPacienteService _notificacionPacienteService;
    private readonly IExpedientePadecimientoRepository _expedientePadecimientoRepository;

    public ExpedienteRecomendacionGeneralService(
        IExpedienteRecomendacionRepository expedienteRecomendacionRepository,
        ExpedienteRecomendacionValidatorService expedienteRecomendacionValidator,
        IExpedienteTrackrRepository expedienteTrackrRepository,
        NotificacionPacienteService notificacionPacienteService,
        IUsuarioRepository usuarioRepository,
        IExpedientePadecimientoRepository expedientePadecimientoRepository
    )
    {
        _expedienteRecomendacionRepository = expedienteRecomendacionRepository;
        _expedienteRecomendacionValidator = expedienteRecomendacionValidator;
        _expedienteTrackrRepository = expedienteTrackrRepository;
        _notificacionPacienteService = notificacionPacienteService;
        _usuarioRepository = usuarioRepository;
        _expedientePadecimientoRepository = expedientePadecimientoRepository;
    }

    public async Task AgregarPacientes(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        foreach (var usuario in expedienteRecomendacionGeneralFormDTO.Paciente)
        {
            int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(usuario).IdExpediente;
            var doctor = _usuarioRepository.Consultar(expedienteRecomendacionGeneralFormDTO.IdDoctor);

            var notificacion = new NotificacionCapturaDTO
            (
                doctor.Nombre,
                expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                1
            );

            var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, usuario);

            var recomendacion = new ExpedienteRecomendaciones
            {
                Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                FechaRealizacion = DateTime.UtcNow,
                IdExpediente = idExpediente,
                IdUsuarioDoctor = expedienteRecomendacionGeneralFormDTO.IdDoctor,
                IdNotificacion = notificacionInsertada.IdNotificacion,
                RecomendacionGeneral = true
            };
            _expedienteRecomendacionRepository.Agregar(recomendacion);

        }

        
    }

    public async Task AgregarTodos(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        List<ExpedienteTrackr> expedientes = _expedienteTrackrRepository.ConsultarExpedientes().ToList();

        foreach(var expediente in  expedientes)
        {
            var idUsuario = expediente.IdUsuario;
            var doctor = _usuarioRepository.Consultar(expedienteRecomendacionGeneralFormDTO.IdDoctor);
            var notificacion = new NotificacionCapturaDTO
            (
                doctor.Nombre,
                expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                1
            );

            var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, idUsuario);

            var recomendacion = new ExpedienteRecomendaciones
            {
                Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                FechaRealizacion = DateTime.UtcNow,
                IdExpediente = expediente.IdExpediente,
                IdUsuarioDoctor = expedienteRecomendacionGeneralFormDTO.IdDoctor,
                IdNotificacion = notificacionInsertada.IdNotificacion,
                RecomendacionGeneral = true
            };

            _expedienteRecomendacionRepository.Agregar(recomendacion);
        }
    }

    public async Task AgregarPorPadecimiento(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        var expedientes = _expedientePadecimientoRepository
                          .ConsultarPorPadecimiento(expedienteRecomendacionGeneralFormDTO.IdPadecimiento)
                          .Select(exp => exp.IdExpedienteNavigation)
                          .ToList();
        foreach(var expediente in expedientes)
        {
            var idUsuario = expediente.IdUsuario;
            var doctor = _usuarioRepository.Consultar(expedienteRecomendacionGeneralFormDTO.IdDoctor);
            var notificacion = new NotificacionCapturaDTO
            (
                doctor.Nombre,
                expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                1
            );
            var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, idUsuario);
            var recomendacion = new ExpedienteRecomendaciones
            {
                Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                FechaRealizacion = DateTime.UtcNow,
                IdExpediente = expediente.IdExpediente,
                IdUsuarioDoctor = expedienteRecomendacionGeneralFormDTO.IdDoctor,
                IdNotificacion = notificacionInsertada.IdNotificacion,
                RecomendacionGeneral = true
            };

            _expedienteRecomendacionRepository.Agregar(recomendacion);
        }

    }
}
