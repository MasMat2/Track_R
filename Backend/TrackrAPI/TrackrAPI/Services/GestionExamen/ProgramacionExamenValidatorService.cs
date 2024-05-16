using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.GestionExamen;

public class ProgramacionExamenValidatorService
{
    private readonly IProgramacionExamenRepository _programacionExamenRepository;

    public ProgramacionExamenValidatorService(IProgramacionExamenRepository programacionExamenRepository)
    {
        _programacionExamenRepository = programacionExamenRepository;
    }

    private readonly string MensajeClaveRequerido = "La clave es requerida";
    private readonly string MensajeResponsableRequerida = "El responsable es requerido";
    private readonly string MensajeFechaHoraRequerida = "La fecha y hora del cuestionario es requerida";
    private readonly string MensajeTipoExamenRequerida = "El tipo cuestionario es requerido";

    private readonly string MensajeDependencia = "El Cuestionario programado tiene participantes";

    private readonly string MensajeExistencia = "El Cuestionario que se requería actualizar no existe";

    public void ValidarAgregar(ProgramacionExamen programacionExamen)
    {
        ValidarRequerido(programacionExamen);
    }

    public void ValidarEditar(ProgramacionExamen programacionExamen)
    {
        ValidarRequerido(programacionExamen);
        ValidarExistencia(programacionExamen.IdProgramacionExamen);
    }

    public void ValidarEliminar(int idProgramacionExamen)
    {
       ValidarExistencia(idProgramacionExamen);
       ValidarDependencia(idProgramacionExamen);
    }

    public void ValidarRequerido(ProgramacionExamen programacionExamen)
    {
        Validator.ValidarRequerido(programacionExamen.IdUsuarioResponsable, MensajeResponsableRequerida);
        Validator.ValidarRequerido(programacionExamen.FechaExamen, MensajeFechaHoraRequerida);
        Validator.ValidarRequerido(programacionExamen.HoraExamen, MensajeFechaHoraRequerida);
        Validator.ValidarRequerido(programacionExamen.IdTipoExamen, MensajeTipoExamenRequerida);
    }

    public void ValidarExistencia(int idAsignacion)
    {
        if (_programacionExamenRepository.Consultar(idAsignacion) == null)
        {
            throw new CdisException(MensajeExistencia);
        }
    }

    public void ValidarDependencia(int idProgramacionExamen)
    {
        ProgramacionExamen programacionExamen = _programacionExamenRepository.ConsultarConDependencias(idProgramacionExamen)!;

        if (programacionExamen.Examen.Any())
        {
            throw new CdisException(MensajeDependencia);
        }
    }
}
