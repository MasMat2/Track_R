using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public interface IExamenReactivoRepository : IRepository<ExamenReactivo>
{
    public ExamenReactivo? Consultar(int idExamenReactivo);
    public IEnumerable<ExamenReactivo> ConsultarGeneral();
    public IEnumerable<ExamenReactivo> ConsultarTodosParaSelector();
    public IEnumerable<ExamenReactivoDto> ConsultarReactivosExamen(int idExamen);
    public IEnumerable<ExamenReactivoExcelDto> ConsultarReactivosExamenExcel(int idProgramacionExamen);
    public DatosExamenReactivoExcelDto obtenerDatosParaRespuestasExcel(int idExamen);
}
