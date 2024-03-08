using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Repositorys.GestionExpediente;

namespace TrackrAPI.Services.GestionExpediente;

public class ExpedienteRecomendacionValidatorService
{
    public readonly IExpedienteRecomendacionRepository _expedienteRecomendacionRepository;
    public readonly IExpedienteRecomendacionGeneralRepository _expedienteRecomendacionGeneralRepository;

    public ExpedienteRecomendacionValidatorService(IExpedienteRecomendacionRepository expedienteRecomendacionRepository,
                                                   IExpedienteRecomendacionGeneralRepository expedienteRecomendacionGeneralRepository)
    {
        _expedienteRecomendacionRepository = expedienteRecomendacionRepository;
        _expedienteRecomendacionGeneralRepository = expedienteRecomendacionGeneralRepository;
    }

    private readonly string MensajeRecomendacionRequerida = "La recomendacion es requerida";
    private readonly string MensajeExistencia = "La recomendacion no existe";
    private static readonly int LongitudRecomendacion = 200;
    private readonly string MensajeRecomendacionLongitud = $"La longitud máxima de la recomendación es de {LongitudRecomendacion}.";

    public void ValidarAgregar(ExpedienteRecomendacionFormDTO recomendacion)
    {
        ValidarRequerido(recomendacion);
        ValidarRango(recomendacion);
    }
    public void ValidarEditar(ExpedienteRecomendacionFormDTO recomendacion)
    {
        ValidarExistencia(recomendacion.IdExpedienteRecomendacion);
        ValidarRequerido(recomendacion);
        ValidarRango(recomendacion);
    }
    public void ValidarEliminar(int idRecomendacion)
    {
        ValidarExistencia(idRecomendacion);
    }
    public void ValidarEliminarGeneral(int idRecomendacion)
    {
        ValidarExistenciaGeneral(idRecomendacion);
    }
    public void ValidarRango(ExpedienteRecomendacionFormDTO recomendacion)
    {
        Validator.ValidarLongitudRangoString(recomendacion.Descripcion, LongitudRecomendacion, MensajeRecomendacionLongitud);
    }

    public void ValidarRequerido(ExpedienteRecomendacionFormDTO recomendacion)
    {
        Validator.ValidarRequerido(recomendacion.Descripcion, MensajeRecomendacionRequerida);
    }

    public void ValidarExistencia(int idRecomendacion)
    {
        var recomendacion = _expedienteRecomendacionRepository.Consultar(idRecomendacion);

        if (recomendacion is null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarExistenciaGeneral(int idRecomendacion)
    {
        var recomendacion = _expedienteRecomendacionGeneralRepository.Consultar(idRecomendacion);

        if (recomendacion is null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }
    public void ValidarAgregarGeneral(ExpedienteRecomendacionGeneralFormDTO recomendacion)
    {
        ValidarRequeridoGeneral(recomendacion);
        ValidarRangoGeneral(recomendacion);
    }
    public void ValidarRangoGeneral(ExpedienteRecomendacionGeneralFormDTO recomendacion)
    {
        Validator.ValidarLongitudRangoString(recomendacion.Descripcion, LongitudRecomendacion, MensajeRecomendacionLongitud);
    }

    public void ValidarRequeridoGeneral(ExpedienteRecomendacionGeneralFormDTO recomendacion)
    {
        Validator.ValidarRequerido(recomendacion.Descripcion, MensajeRecomendacionRequerida);
    }

}