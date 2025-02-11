﻿using TrackrAPI.Dtos.GestionExpediente;
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
    private readonly IExpedienteRecomendacionGeneralRepository _expedienteRecomendacionGeneralRepository;
    private readonly IDetalleExpedienteRecomendacionGeneral _detalleExpedienteRecomendacionGeneral;
    private readonly DetalleExpedienteRecomendacionGeneralService _detalleExpedienteRecomendacionGeneralService;

    public ExpedienteRecomendacionGeneralService(
        IExpedienteRecomendacionRepository expedienteRecomendacionRepository,
        ExpedienteRecomendacionValidatorService expedienteRecomendacionValidator,
        IExpedienteTrackrRepository expedienteTrackrRepository,
        NotificacionPacienteService notificacionPacienteService,
        IUsuarioRepository usuarioRepository,
        IExpedientePadecimientoRepository expedientePadecimientoRepository,
        IExpedienteRecomendacionGeneralRepository expedienteRecomendacionGeneralRepository,
        IDetalleExpedienteRecomendacionGeneral detalleExpedienteRecomendacionGeneral,
        DetalleExpedienteRecomendacionGeneralService detalleExpedienteRecomendacionGeneralService
    )
    {
        _expedienteRecomendacionRepository = expedienteRecomendacionRepository;
        _expedienteRecomendacionValidator = expedienteRecomendacionValidator;
        _expedienteTrackrRepository = expedienteTrackrRepository;
        _notificacionPacienteService = notificacionPacienteService;
        _usuarioRepository = usuarioRepository;
        _expedientePadecimientoRepository = expedientePadecimientoRepository;
        _expedienteRecomendacionGeneralRepository = expedienteRecomendacionGeneralRepository;
        _detalleExpedienteRecomendacionGeneral = detalleExpedienteRecomendacionGeneral;
        _detalleExpedienteRecomendacionGeneralService = detalleExpedienteRecomendacionGeneralService;
    }

    public async Task AgregarPacientes(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        var doctor = _usuarioRepository.Consultar(expedienteRecomendacionGeneralFormDTO.IdDoctor);
        var recomendacionGeneral = new ExpedienteRecomendacionesGenerales
        {
            Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion,
            FechaRealizacion = DateTime.UtcNow,
            IdAdministrador = expedienteRecomendacionGeneralFormDTO.IdDoctor,
            Tipo = 3
        };
        _expedienteRecomendacionGeneralRepository.Agregar(recomendacionGeneral);

        foreach (var usuario in expedienteRecomendacionGeneralFormDTO.Paciente)
        {
            int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(usuario).IdExpediente;
            var notificacion = new NotificacionCapturaDTO
            (
                doctor.Nombre,
                expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                null,
                1,
                doctor.IdUsuario,
                null,
                null
            );

            var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, usuario);
            var detalleRecomendacionGeneral = new DetalleExpedienteRecomendacionesGenerales
            {
                IdExpediente = idExpediente,
                IdNotificacion = notificacionInsertada.IdNotificacion,
                IdExpedienteRecomendacionesGenerales = recomendacionGeneral.IdExpedienteRecomendacionesGenerales
            };

            _detalleExpedienteRecomendacionGeneral.Agregar(detalleRecomendacionGeneral);
        }
    }

    public async Task AgregarTodos(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        List<ExpedienteTrackr> expedientes = _expedienteTrackrRepository.ConsultarExpedientes().ToList();

        var doctor = _usuarioRepository.Consultar(expedienteRecomendacionGeneralFormDTO.IdDoctor);
        var recomendacionGeneral = new ExpedienteRecomendacionesGenerales
        {
            Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion,
            FechaRealizacion = DateTime.UtcNow,
            IdAdministrador = expedienteRecomendacionGeneralFormDTO.IdDoctor,
            Tipo = 1
        };
        _expedienteRecomendacionGeneralRepository.Agregar(recomendacionGeneral);
        foreach (var expediente in expedientes)
        {
            var idUsuario = expediente.IdUsuario;
            var notificacion = new NotificacionCapturaDTO
            (
                doctor.Nombre,
                expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                null,
                1,
                doctor.IdUsuario,
                null,
                null
            );

            var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, idUsuario);
            var detalleRecomendacionGeneral = new DetalleExpedienteRecomendacionesGenerales
            {
                IdExpediente = expediente.IdExpediente,
                IdNotificacion = notificacionInsertada.IdNotificacion,
                IdExpedienteRecomendacionesGenerales = recomendacionGeneral.IdExpedienteRecomendacionesGenerales
            };

            _detalleExpedienteRecomendacionGeneral.Agregar(detalleRecomendacionGeneral);
        }
    }

    public async Task AgregarPorPadecimiento(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        var expedientes = _expedientePadecimientoRepository
                          .ConsultarPorPadecimiento(expedienteRecomendacionGeneralFormDTO.IdPadecimiento)
                          .Select(exp => exp.IdExpedienteNavigation)
                          .ToList();
        var doctor = _usuarioRepository.Consultar(expedienteRecomendacionGeneralFormDTO.IdDoctor);
        var recomendacionGeneral = new ExpedienteRecomendacionesGenerales
        {
            Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion,
            FechaRealizacion = DateTime.UtcNow,
            IdAdministrador = expedienteRecomendacionGeneralFormDTO.IdDoctor,
            Tipo = 2
        };
        _expedienteRecomendacionGeneralRepository.Agregar(recomendacionGeneral);

        foreach (var expediente in expedientes)
        {
            var idUsuario = expediente.IdUsuario;
            var notificacion = new NotificacionCapturaDTO
            (
                doctor.Nombre,
                expedienteRecomendacionGeneralFormDTO.Descripcion ?? string.Empty,
                null,
                1,
                doctor.IdUsuario,
                null,
                null
            );
            var notificacionInsertada = await _notificacionPacienteService.Notificar(notificacion, idUsuario);
            var detalleRecomendacionGeneral = new DetalleExpedienteRecomendacionesGenerales
            {
                IdExpediente = expediente.IdExpediente,
                IdNotificacion = notificacionInsertada.IdNotificacion,
                IdExpedienteRecomendacionesGenerales = recomendacionGeneral.IdExpedienteRecomendacionesGenerales
            };
            _detalleExpedienteRecomendacionGeneral.Agregar(detalleRecomendacionGeneral);
        }
    }

    public IEnumerable<ExpedienteRecomendacionGeneralGridDTO> obtenerGrid()
    {
        return _expedienteRecomendacionGeneralRepository.ConsultarGrid();
    }

    public void Editar(ExpedienteRecomendacionGeneralFormDTO expedienteRecomendacionGeneralFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregarGeneral(expedienteRecomendacionGeneralFormDTO);
        var recomendacionGeneral = _expedienteRecomendacionGeneralRepository.Consultar((int)expedienteRecomendacionGeneralFormDTO.IdExpedienteRecomendacionesGenerales);

        recomendacionGeneral.Descripcion = expedienteRecomendacionGeneralFormDTO.Descripcion;

        _expedienteRecomendacionGeneralRepository.Editar(recomendacionGeneral);
    }

    public ExpedienteRecomendacionGeneralFormDTO Consultar(int IdExpedienteRecomendacionGeneral)
    {
        var recomendacion = _expedienteRecomendacionGeneralRepository.Consultar(IdExpedienteRecomendacionGeneral);

        return new ExpedienteRecomendacionGeneralFormDTO
        {
            IdExpedienteRecomendacionesGenerales = (int)recomendacion.IdExpedienteRecomendacionesGenerales,
            Tipo = (int)recomendacion.Tipo,
            Descripcion = recomendacion.Descripcion,
            IdDoctor = recomendacion.IdAdministrador,
            Fecha = recomendacion.FechaRealizacion
        };
    }

    public void Eliminar(int IdExpedienteRecomendacionGeneral)
    {
        _expedienteRecomendacionValidator.ValidarEliminarGeneral(IdExpedienteRecomendacionGeneral);
        var recomendacion = _expedienteRecomendacionGeneralRepository.Consultar(IdExpedienteRecomendacionGeneral);
        _detalleExpedienteRecomendacionGeneralService.eliminarDetalle(recomendacion.IdExpedienteRecomendacionesGenerales);
        _expedienteRecomendacionGeneralRepository.Eliminar(recomendacion);
    }
}
