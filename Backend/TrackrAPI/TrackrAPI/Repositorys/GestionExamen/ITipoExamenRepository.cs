using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public interface ITipoExamenRepository: IRepository<TipoExamen>
{
    public TipoExamenDto? Consultar(int idTipoExamen);
    public IEnumerable<TipoExamenGridDto> ConsultarGeneral();
    public IEnumerable<TipoExamenGridDto> ConsultarTodosParaSelector();
    public TipoExamen? ConsultarConDependencias(int idTipoExamen);
    public TipoExamen? ConsultarDuplicado(TipoExamen tipoExamen);
}
