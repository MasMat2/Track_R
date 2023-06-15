using TrackrAPI.Dtos.Examen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Examen;

public interface IAsignaturaRepository: IRepository<Asignatura>
{
    public AsignaturaDto? Consultar(int idAsignatura);
    public IEnumerable<AsignaturaGridDto> ConsultarGeneral();
    public IEnumerable<AsignaturaGridDto> ConsultarTodosParaSelector();
    public Asignatura? ConsultarConDependencias(int idAsignatura);
    public Asignatura? ConsultarDuplicado(Asignatura asignatura);
}
