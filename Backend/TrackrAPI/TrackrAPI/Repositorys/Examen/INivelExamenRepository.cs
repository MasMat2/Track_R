using TrackrAPI.Dtos.Examen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Examen;

public interface INivelExamenRepository : IRepository<NivelExamen>
{
    public NivelExamenDto? Consultar(int idNivelExamen);
    public IEnumerable<NivelExamenGridDto> ConsultarGeneral();
    public IEnumerable<NivelExamenGridDto> ConsultarTodosParaSelector();
    public NivelExamen? ConsultarConDependencias(int idNivelExamen);
    public NivelExamen? ConsultarDuplicado(NivelExamen nivelExamen);
}
