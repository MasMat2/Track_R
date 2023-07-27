using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public interface IContenidoExamenRepository : IRepository<ContenidoExamen>
{
    public ContenidoExamenDto? Consultar(int idContenidoExamen);
    public IEnumerable<ContenidoExamenGridDto> ConsultarGeneral(int idTipoExamen);
    public IEnumerable<ContenidoExamenGridDto> ConsultarTodosParaSelector();
    public IEnumerable<ContenidoExamen> ConsultarTodosNoFormato(int idTipoExamen);
}
