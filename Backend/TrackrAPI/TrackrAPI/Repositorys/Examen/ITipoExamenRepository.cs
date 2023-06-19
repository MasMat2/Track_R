using TrackrAPI.Dtos.Examen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Examen;

public interface ITipoExamenRepository: IRepository<TipoExamen>
{
    public TipoExamenDto? Consultar(int idTipoExamen);
    public IEnumerable<TipoExamenGridDto> ConsultarGeneral();
    public IEnumerable<TipoExamenGridDto> ConsultarTodosParaSelector();
    public TipoExamen? ConsultarConDependencias(int idTipoExamen);
    public TipoExamen? ConsultarDuplicado(TipoExamen tipoExamen);
}
