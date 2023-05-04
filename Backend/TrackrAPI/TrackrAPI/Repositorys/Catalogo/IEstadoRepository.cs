using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IEstadoRepository : IRepository<Estado>
    {
        IEnumerable<EstadoGridDto> ConsultarTodosParaGrid();
        IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais);
        IEnumerable<Estado> ConsultarPorPais(int idPais);
        public IEnumerable<EstadoSelectorDto> ConsultarGeneral();
        EstadoDto ConsultarDto(int idEstado);
        Estado Consultar(int idEstado);
        Estado Consultar(string nombre, int idPais);
        Estado ConsultarDependencias(int idEstado);
    }
}
