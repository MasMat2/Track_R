using TrackrAPI.Models;
using TrackrAPI.Repositorys.Examen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.Examen;

public class NivelExamenValidatorService
{
    private readonly INivelExamenRepository _nivelExamenRepository;

    public NivelExamenValidatorService(INivelExamenRepository nivelExamenRepository)
    {
        _nivelExamenRepository = nivelExamenRepository;
    }

    private readonly string MensajeDescripcionRequerido = "La descripcion es requerido";
    private readonly string MensajeClaveRequerida = "La clave es requerida";

    private static readonly int LongitudDescripcion = 50;
    private static readonly int LongitudClave = 5;

    private readonly string MensajeDescripcionLongitud = $"La longitud máxima de la descripción son {LongitudDescripcion } caracteres";
    private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

    private readonly string MensajeDependencia = "El nivel examen tiene reactivos agregados y no se puede eliminar";

    private readonly string MensajeExistencia = "El nivel examen que se requería actualizar no existe";

    private readonly string MensajeDuplicado = "El nivel examen que intenta agregar ya existe";

    public void ValidarAgregar(NivelExamen nivelExamen)
    {
        ValidarRequerido(nivelExamen);
    }

    public void ValidarEditar(NivelExamen nivelExamen)
    {
        ValidarRequerido(nivelExamen);
        ValidarExistencia(nivelExamen.IdNivelExamen);
    }

    public void ValidarEliminar(int idNivelExamen)
    {
        ValidarExistencia(idNivelExamen);
        ValidarDependencia(idNivelExamen);
    }

    public void ValidarRequerido(NivelExamen nivelExamen)
    {
        Validator.ValidarRequerido(
            nivelExamen.Descripcion,
            MensajeDescripcionRequerido);

        Validator.ValidarRequerido(
            nivelExamen.Clave,
            MensajeClaveRequerida);

        Validator.ValidarLongitudMaximaString(
            nivelExamen.Clave,
            LongitudClave,
            MensajeClaveLongitud);

        Validator.ValidarLongitudMaximaString(
            nivelExamen.Clave,
            LongitudDescripcion,
            MensajeDescripcionLongitud);

    }

    public void ValidarExistencia(int idNivelExamen)
    {
        if (_nivelExamenRepository.Consultar(idNivelExamen) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarDependencia(int idNivelExamen)
    {
        NivelExamen nivelExamen = _nivelExamenRepository.ConsultarConDependencias(idNivelExamen)!;

        if (nivelExamen.Reactivo.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }

    public void ValidarDuplicado(NivelExamen nivelExamen)
    {
        if (_nivelExamenRepository.ConsultarDuplicado(nivelExamen) != null )
        {
            throw new CdisException(MensajeDuplicado);
        }
    }
}
