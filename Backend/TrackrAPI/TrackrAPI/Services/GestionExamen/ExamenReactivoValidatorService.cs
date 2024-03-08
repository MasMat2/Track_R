using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.GestionExamen;

public class ExamenReactivoValidatorService
{
    private readonly IExamenReactivoRepository _examenReactivoRepository;

    public ExamenReactivoValidatorService(IExamenReactivoRepository examenReactivoRepository)
    {
        this._examenReactivoRepository = examenReactivoRepository;
    }

    private readonly string MensajeClaveRequerido = "La clave es requerida";
    private readonly string MensajeResponsableRequerida = "El responsable es requerido";
    private readonly string MensajeFechaHoraRequerida = "La fecha y hora de examen es requerida";
    private readonly string MensajeTipoExamenRequerida = "El tipo de examen es requerido";

    private readonly string MensajeDependencia = "El examen programado tiene participantes";

    private readonly string MensajeExistencia = "La examen que se requería actualizar no existe";

    public void ValidarAgregar(ExamenReactivo examenReactivo)
    {
        ValidarRequerido(examenReactivo);
    }

    public void ValidarEditar(ExamenReactivo examenReactivo)
    {
        ValidarRequerido(examenReactivo);
        ValidarExistencia(examenReactivo.IdExamenReactivo);
    }

    public void ValidarEliminar(int idExamenReactivo)
    {
       //ExamenReactivo examenReactivo = examenReactivoRepository.ConsultarConDependencias(idExamenReactivo);
       ValidarExistencia(idExamenReactivo);
       //ValidarDependencia(examenReactivo);
    }

    public void ValidarRequerido(ExamenReactivo examenReactivo)
    {
        /*
        Validator.ValidarRequerido(examenReactivo.Clave, MensajeClaveRequerido);
        Validator.ValidarRequerido(examenReactivo.IdUsuarioResponsable, MensajeResponsableRequerida);
        Validator.ValidarRequerido(examenReactivo.FechaExamen, MensajeFechaHoraRequerida);
        Validator.ValidarRequerido(examenReactivo.HoraExamen, MensajeFechaHoraRequerida);
        Validator.ValidarRequerido(examenReactivo.IdTipoExamen, MensajeTipoExamenRequerida);
        */
    }

    public void ValidarExistencia(int idAsignacion)
    {
        if (_examenReactivoRepository.Consultar(idAsignacion) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }
    /*
    public void ValidarDependencia(ExamenReactivo examenReactivo)
    {
        if (examenReactivo.Examen.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }
    */
}
