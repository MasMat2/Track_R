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

    public ExpedienteRecomendacionGeneralService(
        IExpedienteRecomendacionRepository expedienteRecomendacionRepository,
        ExpedienteRecomendacionValidatorService expedienteRecomendacionValidator,
        IExpedienteTrackrRepository expedienteTrackrRepository,
        NotificacionPacienteService notificacionPacienteService,
        IUsuarioRepository usuarioRepository
    )
    {
        _expedienteRecomendacionRepository = expedienteRecomendacionRepository;
        _expedienteRecomendacionValidator = expedienteRecomendacionValidator;
        _expedienteTrackrRepository = expedienteTrackrRepository;
        _notificacionPacienteService = notificacionPacienteService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task Agregar(ExpedienteRecomendacionFormDTO expedienteRecomendacionFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregar(expedienteRecomendacionFormDTO);
        int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(expedienteRecomendacionFormDTO.IdUsuario).IdExpediente;
        var doctor = _usuarioRepository.Consultar(expedienteRecomendacionFormDTO.IdDoctor);

        var notificacion = new NotificacionCapturaDTO
        (
            doctor.Nombre,
            expedienteRecomendacionFormDTO.Descripcion ?? string.Empty,
            1
        );

        var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, expedienteRecomendacionFormDTO.IdUsuario);

        var recomendacion = new ExpedienteRecomendaciones
        {
            Descripcion = expedienteRecomendacionFormDTO.Descripcion ?? string.Empty,
            FechaRealizacion = DateTime.UtcNow,
            IdExpediente = idExpediente,
            IdUsuarioDoctor = expedienteRecomendacionFormDTO.IdDoctor,
            IdNotificacion = notificacionInsertada.IdNotificacion,
            RecomendacionGeneral = true
        };
        _expedienteRecomendacionRepository.Agregar(recomendacion);
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
}
