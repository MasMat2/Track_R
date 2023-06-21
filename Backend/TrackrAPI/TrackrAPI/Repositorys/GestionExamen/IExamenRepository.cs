using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;

public interface IExamenRepository : IRepository<Examen>
{
    public Examen? Consultar(int idExamen);
    public IEnumerable<Examen> ConsultarGeneral(int idProgramacionExamen);
    public IEnumerable<Examen> ConsultarTodosParaSelector(int idProgramacionExamen);
    public IEnumerable<ExamenGridDto> ConsultarMisExamenes(int idUsuario);
    public ExamenDto? ConsultarMiExamen(int idExamen);
    public IEnumerable<ExamenCalificacionDto> ConsultarCalificaciones(int idProgramacionExamen);
}
