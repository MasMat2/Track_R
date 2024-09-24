using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.GestionExamen;

public class ContenidoExamenValidatorService
{
    private readonly IContenidoExamenRepository _contenidoExamenRepository;
    private readonly IReactivoRepository _reactivoRepository;

    public ContenidoExamenValidatorService(
        IContenidoExamenRepository contenidoExamenRepository,
        IReactivoRepository reactivoRepository)
    {
        _contenidoExamenRepository = contenidoExamenRepository;
        _reactivoRepository = reactivoRepository;
    }

    private readonly string MensajeClaveRequerido = "La clave es requerida";
    private readonly string MensajeContenidoNivelDuplicado = "El contenido del examen ya existe";

    private readonly string MensajeExistencia = "El Cuestionario que se requería actualizar no existe";

    private static readonly int LongitudClave = 5;
    private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son {LongitudClave} caracteres";

    private readonly string MensajeCantidadReactivos = "No hay suficientes reactivos para agregar el cuestionario";

    public void ValidarAgregar(ContenidoExamen contenidoExamen)
    {
        ValidarRequerido(contenidoExamen);
        ValidarCantidadReactivos(contenidoExamen);
        ValidarDuplicado(contenidoExamen);
    }

    public void ValidarEditar(ContenidoExamen contenidoExamen)
    {
        ValidarRequerido(contenidoExamen);
        ValidarExistencia(contenidoExamen.IdContenidoExamen);
        ValidarCantidadReactivos(contenidoExamen);
    }

    public void ValidarDuplicado(ContenidoExamen contenidoExamen)
    {
        if (_contenidoExamenRepository.ConsultarDuplicado(contenidoExamen) != null)
        {
            throw new CdisException(MensajeContenidoNivelDuplicado);
        }
    }

    public void ValidarEliminar(int idContenidoExamen)
    {
        ValidarExistencia(idContenidoExamen);
    }

    public void ValidarRequerido(ContenidoExamen contenidoExamen)
    {
        Validator.ValidarLongitudMaximaString(contenidoExamen.Clave, LongitudClave, MensajeClaveLongitud);
    }

    public void ValidarExistencia(int idContenidoExamen)
    {
        if (_contenidoExamenRepository.Consultar(idContenidoExamen) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarCantidadReactivos(ContenidoExamen contenidoExmaen)
    {
        int totalPreguntas = _reactivoRepository.ConsultarCantidadReactivos(contenidoExmaen.IdAsignatura, contenidoExmaen.IdNivelExamen);

        if(contenidoExmaen.TotalPreguntas > totalPreguntas)
        {
            throw new CdisException(MensajeCantidadReactivos);
        }
    }
}
