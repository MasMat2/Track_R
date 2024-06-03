using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;
public interface IRespuestaRepository : IRepository<Respuesta>
{
    public Respuesta Consultar(int idRespuesta);
    public IEnumerable<RespuestaDto> ConsultarTodosPorReactivo(int idReactivo);
    public RespuestaDto? ConsultarParaFormulario(int idRespuesta);
    public RespuestaDto ConsultarRespuestaContestada(int idReactivo , string clave);
    
}
