using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;

public class ProgramacionExamenService
{
    private readonly IProgramacionExamenRepository _programacionExamenRepository;
    private readonly ProgramacionExamenValidatorService _programacionExamenValidatorService;

    public ProgramacionExamenService(
        IProgramacionExamenRepository programacionExamenRepository,
        ProgramacionExamenValidatorService programacionExamenValidatorService)
    {
        _programacionExamenRepository = programacionExamenRepository;
        _programacionExamenValidatorService = programacionExamenValidatorService;
    }

    public ProgramacionExamenDto? Consultar(int idProgramacionExamen)
    {
        ProgramacionExamenDto? programacionExamen = _programacionExamenRepository.Consultar(idProgramacionExamen);
        return programacionExamen;
    }
    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral(int idCompania)
    {
        return _programacionExamenRepository.ConsultarGeneral(idCompania);
    }
    public IEnumerable<ProgramacionExamenGridDto> ConsultarTodosParaSelector()
    {
        return _programacionExamenRepository.ConsultarTodosParaSelector();
    }

    public int Agregar(ProgramacionExamen programacionExamen, int idCompania)
    {
        _programacionExamenValidatorService.ValidarAgregar(programacionExamen);
        programacionExamen.Clave = GenerarClave(idCompania);
        _programacionExamenRepository.Agregar(programacionExamen);
        return programacionExamen.IdProgramacionExamen;
    }

    private string GenerarClave(int idCompania)
    {
        var programacionExamen = _programacionExamenRepository.ConsultarGeneral(idCompania).OrderByDescending(x => x.IdProgramacionExamen).FirstOrDefault();

        return (programacionExamen.IdProgramacionExamen + 1).ToString();
    }

    public void Editar(ProgramacionExamen programacionExamen)
    {
        var programacionExamenExistente = _programacionExamenRepository.Consultar(programacionExamen.IdProgramacionExamen);
        if(programacionExamen is not null)
        {
            programacionExamen.FechaAlta = programacionExamenExistente.FechaAlta;
        }

        _programacionExamenValidatorService.ValidarEditar(programacionExamen);
        _programacionExamenRepository.Editar(programacionExamen);
    }

    public void Eliminar(int idProgramacionExamen)
    {
        ProgramacionExamenDto? programacionExamen = _programacionExamenRepository.Consultar(idProgramacionExamen);
        _programacionExamenValidatorService.ValidarEliminar(idProgramacionExamen);

        if (programacionExamen != null)
        {
            ProgramacionExamen programacionExamenMod = new()
            {
                IdProgramacionExamen = programacionExamen.IdProgramacionExamen,
                IdTipoExamen = programacionExamen.IdTipoExamen,
                IdUsuarioResponsable = programacionExamen.IdUsuarioResponsable,
                Clave = programacionExamen.Clave,
                Duracion = programacionExamen.Duracion,
                CantidadParticipantes = programacionExamen.CantidadParticipantes,
                FechaExamen = programacionExamen.FechaExamen,
                HoraExamen = programacionExamen.HoraExamen,
                FechaAlta = programacionExamen.FechaAlta,
                Estatus = false
            };

            _programacionExamenRepository.Editar(programacionExamenMod);
        }
    }
}
