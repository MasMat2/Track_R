using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteRecomendacionService
{
    private readonly IExpedienteRecomendacionRepository _expedienteRecomendacionRepository;
    private readonly IExpedienteTrackrRepository _expedienteTrackrRepository;
    private readonly ExpedienteRecomendacionValidatorService _expedienteRecomendacionValidator;

    public ExpedienteRecomendacionService(
        IExpedienteRecomendacionRepository expedienteRecomendacionRepository,
        IExpedienteTrackrRepository expedienteTrackrRepository,
        ExpedienteRecomendacionValidatorService expedienteRecomendacionValidator)
    {
        _expedienteRecomendacionRepository = expedienteRecomendacionRepository;
        _expedienteTrackrRepository = expedienteTrackrRepository;
        _expedienteRecomendacionValidator = expedienteRecomendacionValidator;
    }

    public IEnumerable<ExpedienteRecomendacionGridDTO> ConsultarGridPorUsuario(int idUsuario)
    {
        return _expedienteRecomendacionRepository.ConsultarGridPorUsuario(idUsuario);
    }

    public ExpedienteRecomendacionFormDTO Consultar(int idExpedienteRecomendacion)
    {
        var recomendacionModel = _expedienteRecomendacionRepository.Consultar(idExpedienteRecomendacion);

        return new ExpedienteRecomendacionFormDTO
        {
            IdExpedienteRecomendacion = recomendacionModel.IdExpedienteRecomendaciones,
            Descripcion = recomendacionModel.Descripcion,
            IdUsuario = recomendacionModel.IdUsuarioDoctor,
            IdDoctor = recomendacionModel.IdUsuarioDoctor,
            Fecha = recomendacionModel.FechaRealizacion

        };
    }


    public void Agregar(ExpedienteRecomendacionFormDTO expedienteRecomendacionFormDTO)
    {
        _expedienteRecomendacionValidator.ValidarAgregar(expedienteRecomendacionFormDTO);

        int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(expedienteRecomendacionFormDTO.IdUsuario).IdExpediente;
        var recomendacion = new ExpedienteRecomendaciones
        {
            Descripcion = expedienteRecomendacionFormDTO.Descripcion ?? string.Empty,
            FechaRealizacion = DateTime.UtcNow,
            IdExpediente = idExpediente,
            IdUsuarioDoctor = expedienteRecomendacionFormDTO.IdDoctor
        };
        _expedienteRecomendacionRepository.Agregar(recomendacion);
    }

    public void Editar(ExpedienteRecomendacionFormDTO expedienteRecomendacionFormDTO)
    {

        _expedienteRecomendacionValidator.ValidarAgregar(expedienteRecomendacionFormDTO);

        int idExpediente = _expedienteTrackrRepository.ConsultarPorUsuario(expedienteRecomendacionFormDTO.IdUsuario).IdExpediente;

        var recomendacion = _expedienteRecomendacionRepository.Consultar(expedienteRecomendacionFormDTO.IdExpedienteRecomendacion);

        recomendacion.Descripcion = expedienteRecomendacionFormDTO.Descripcion;
        recomendacion.IdExpediente = idExpediente;

        _expedienteRecomendacionRepository.Editar(recomendacion);
    }

    public void Eliminar(int idExpedienteRecomendacion)
    {
        _expedienteRecomendacionValidator.ValidarEliminar(idExpedienteRecomendacion);
        var recomendacion = _expedienteRecomendacionRepository.Consultar(idExpedienteRecomendacion);
        _expedienteRecomendacionRepository.Eliminar(recomendacion);
    }
}