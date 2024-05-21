using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Services.GestionExamen;

public class ExamenValidatorService
{
    private readonly IExamenRepository _examenRepository;

    public ExamenValidatorService(IExamenRepository examenRepository)
    {
        _examenRepository = examenRepository;
    }

    private readonly string MensajeParticipanteRequerido = "El participante es requerido";

    private readonly string MensajeCantidadReactivos = "No hay suficientes reactivos para aplicar este cuestionario";

    private readonly string MensajeDependencia = "El cuestionario programado tiene participantes";

    private readonly string MensajeExistencia = "El cuestionario que se requería actualizar no existe";

    public void ValidarAgregar(Examen examen)
    {
        ValidarRequerido(examen);
    }

    public void ValidarEditar(Examen examen)
    {
        ValidarRequerido(examen);
        ValidarExistencia(examen.IdExamen);
    }

    public void ValidarEliminar(int idExamen)
    {
       ValidarExistencia(idExamen);
    }

    public void ValidarRequerido(Examen examen)
    {
        Validator.ValidarRequerido(examen.IdUsuarioParticipante, MensajeParticipanteRequerido);
    }

    public void ValidarExistencia(int idAsignacion)
    {
        if (_examenRepository.Consultar(idAsignacion) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarDependencia(Examen examen)
    {
        if (examen.ExamenReactivo.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }

    public void ValidarCantidadReactivos(ExamenDto examen, int totalReactivos)
    {
        if(examen.TotalPreguntas > totalReactivos)
        {
            throw new CdisException(MensajeCantidadReactivos);
        }
    }
}
