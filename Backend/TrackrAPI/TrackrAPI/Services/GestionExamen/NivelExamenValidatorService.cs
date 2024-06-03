using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.GestionExamen;

public class NivelExamenValidatorService
{
    private readonly INivelExamenRepository _nivelExamenRepository;

    public NivelExamenValidatorService(INivelExamenRepository nivelExamenRepository)
    {
        _nivelExamenRepository = nivelExamenRepository;
    }

    private readonly string MensajeDescripcionRequerido = "La descripción es requerida";
    private readonly string MensajeClaveRequerida = "La clave es requerida";

    private static readonly int LongitudDescripcion = 50;
    private static readonly int LongitudClave = 5;

    private readonly string MensajeDescripcionLongitud = $"La longitud máxima de la descripción son {LongitudDescripcion } caracteres";
    private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

    private readonly string MensajeDependencia = "La Complejidad del cuestionario tiene reactivos asociados y no se puede eliminar";

    private readonly string MensajeExistencia = "La Complejidad del cuestionario que se requería actualizar no existe";

    private readonly string MensajeDuplicado = "La Complejidad del cuestionario que intenta agregar ya existe";

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
