using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IAyudaSeccionRepository: IRepository<AyudaSeccion>
    {
        public IEnumerable<AyudaSeccionSelectorDto> ConsultarParaSelector();
        public IEnumerable<AyudaSeccionGridDto> ConsultarTodosParaGrid();
        public AyudaSeccion Consultar(int idAyudaSeccion);
        public AyudaSeccion ConsultarPorClave(string clave);
        public AyudaSeccion ConsultarPorNombre(string nombre);
        public AyudaSeccion ConsultarPorOrden(int orden);
    }
}
