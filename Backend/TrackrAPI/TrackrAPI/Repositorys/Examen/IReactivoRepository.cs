using TrackrAPI.Dtos.Examen;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Examen;

public interface IReactivoRepository : IRepository<Reactivo>
{
    public ReactivoDto? Consultar(int idReactivo);
    public IEnumerable<ReactivoGridDto> ConsultarGeneral();
    public IEnumerable<ReactivoGridDto> ConsultarTodosParaSelector();
    public ReactivoDto? ConsultarImagen(int idReactivo);
    public IEnumerable<Reactivo> ConsultarReactivosAleatorio(int idAsignatura, int idNivelExamen, int cantidadPreguntas);
    public string ConsultarRespuestaCorrecta(int idReactivo);
    public int ConsultarCantidadReactivos(int idAsignatura, int idNivelExamen);
}
