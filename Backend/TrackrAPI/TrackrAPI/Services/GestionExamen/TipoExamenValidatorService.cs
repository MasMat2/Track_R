using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.GestionExamen;

public class TipoExamenValidatorService
{
    private readonly ITipoExamenRepository _tipoExamenRepository;

    public TipoExamenValidatorService(ITipoExamenRepository tipoExamenRepository)
    {
        _tipoExamenRepository = tipoExamenRepository;
    }

    private readonly string MensajeClaveRequerida = "La clave es requerida";
    private static readonly int LongitudClave = 5;
    private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave } caracteres";

    private readonly string MensajeNombreRequerido = "El Tipo de Encuesta es requerido";
    private static readonly int LongitudNombre = 200;
    private readonly string MensajeNombreLongitud = $"La longitud máxima de la descripción son {LongitudNombre } caracteres";

    private readonly string MensajeDependencia = "El Tipo de Encuesta tiene información agregada y no se puede eliminar";

    private readonly string MensajeExistencia = "El Tipo de Encuesta que se requería actualizar no existe";

    private readonly string MensajeDuplicado = "El Tipo de Encuesta que intenta agregar ya existe";

    public void ValidarAgregar(TipoExamen tipoExamen)
    {
        ValidarRequerido(tipoExamen);
    }

    public void ValidarEditar(TipoExamen tipoExamen)
    {
        ValidarRequerido(tipoExamen);
        ValidarExistencia(tipoExamen.IdTipoExamen);
    }

    public void ValidarEliminar(int idTipoExamen)
    {
        ValidarExistencia(idTipoExamen);
        ValidarDependencia(idTipoExamen);
    }

    public void ValidarRequerido(TipoExamen tipoExamen)
    {
        Validator.ValidarRequerido(
            tipoExamen.Nombre,
            MensajeNombreRequerido);

        Validator.ValidarLongitudMaximaString(
            tipoExamen.Nombre,
            LongitudNombre,
            MensajeNombreLongitud);
    }

    public void ValidarExistencia(int idTipoExamen)
    {
        if (_tipoExamenRepository.Consultar(idTipoExamen) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarDependencia(int idTipoExamen)
    {
        TipoExamen tipoExamen = _tipoExamenRepository.ConsultarConDependencias(idTipoExamen)!;

        if (tipoExamen.ContenidoExamen.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }

    public void ValidarDuplicado(TipoExamen tipoExamen)
    {
        if (_tipoExamenRepository.ConsultarDuplicado(tipoExamen) != null)
        {
            throw new CdisException(MensajeDuplicado);
        }
    }
}
