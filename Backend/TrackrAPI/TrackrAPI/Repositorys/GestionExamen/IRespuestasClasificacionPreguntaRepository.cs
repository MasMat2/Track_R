using TrackrAPI.Dtos.General;
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExamen;
public interface IRespuestasClasificacionPreguntaRepository : IRepository<RespuestasClasificacionPregunta>
{
    RespuestasClasificacionPreguntaInformacionGeneralDto ConsultarParaFormulario(int idRespuestasClasificacionPregunta);
    IEnumerable<RespuestasClasificacionPreguntaGridDto> ConsultarParaGrid(int idClasificacionPregunta);
    void Eliminar(int idRespuestasClasificacionPregunta);
    public IEnumerable<Respuesta>? ConsultarRespuestasPorClasificacion(int idClasificacionPregunta);
    public int? ConsultarIdClasificacionPorNombreRespuesta(string respuesta);
}

