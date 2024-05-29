

using TrackrAPI.Dtos.General;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;
public interface IClasificacionPreguntaRepository : IRepository<ClasificacionPregunta>
{
    public ClasificacionPreguntaFormularioDto Consultar(int idClasificacionPregunta);
    public IEnumerable<ClasificacionPreguntaGridDto> ConsultarParaGrid();
    public void Eliminar(int idClasificacionPregunta);
    public IEnumerable<SimpleSelectorDto> ConsultarTodosParaSelector();
}
