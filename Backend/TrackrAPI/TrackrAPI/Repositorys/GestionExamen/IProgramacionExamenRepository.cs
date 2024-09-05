using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public interface IProgramacionExamenRepository : IRepository<ProgramacionExamen>
{
    public ProgramacionExamenDto? Consultar(int idProgramacionExamen);
    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral(int idCompania);
    public IEnumerable<ProgramacionExamenGridDto> ConsultarGeneral(int idCompania, int idUsuarioSesion);
    public IEnumerable<ProgramacionExamenGridDto> ConsultarTodosParaSelector();
    public ProgramacionExamen? ConsultarConDependencias(int idProgramacionExamen);
}
