
using TrackrAPI.Dtos.GestionExamen;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionExamen;

namespace TrackrAPI.Services.GestionExamen;
public class RespuestaService
{
    private readonly IRespuestaRepository respuestaRepository;

    public RespuestaService(
        IRespuestaRepository respuestaRepository)
    {
        this.respuestaRepository = respuestaRepository;
    }

    public RespuestaDto? ConsultarParaFormulario(int idRespuesta)
    {
        RespuestaDto? respuesta = respuestaRepository.ConsultarParaFormulario(idRespuesta);

        if (respuesta == null)
        {
            return null;
        }
        return respuesta;
    }

    public IEnumerable<RespuestaDto> ConsultarTodosPorReactivo(int idReactivo)
    {
        return respuestaRepository.ConsultarTodosPorReactivo(idReactivo);
    }

    public int Agregar(Respuesta respuesta)
    {
        respuestaRepository.Agregar(respuesta);
        return respuesta.IdRespuesta;
    }

    public void Editar(Respuesta respuesta)
    {
        respuestaRepository.Editar(respuesta);
    }

    public void Eliminar(int idRespuesta)
    {
        Respuesta respuesta = respuestaRepository.Consultar(idRespuesta);
        respuestaRepository.Eliminar(respuesta);
    }

}
