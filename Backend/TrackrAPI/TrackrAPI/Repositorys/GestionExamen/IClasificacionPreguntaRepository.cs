using RoadisAPI.Dtos.GestionExamen;
using RoadisAPI.Models;
using RoadisAPI.Dtos;

namespace RoadisAPI.Repositorys.Interfaces.GestionExamen
{
    public interface IClasificacionPreguntaRepository : IRepository<ClasificacionPregunta>
    {
        public ClasificacionPreguntaFormularioDto Consultar(int idClasificacionPregunta);
        public IEnumerable<ClasificacionPreguntaGridDto> ConsultarParaGrid();
        public void Eliminar(int idClasificacionPregunta);
        public IEnumerable<SimpleSelectorDto> ConsultarTodosParaSelector();
    }
}
