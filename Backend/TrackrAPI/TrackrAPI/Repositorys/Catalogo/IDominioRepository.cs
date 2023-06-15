using TrackrAPI.Models;
using TrackrAPI.Dtos.Catalogo;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IDominioRepository : IRepository<Dominio>
    {
        public DominioDto? ConsultarDto(int idDominio);
        public Dominio? Consultar(int idDominio);
        public Dominio? Consultar(string nombre);
        public IEnumerable<DominioGridDto> ConsultarTodosParaGrid(int idUsuarioSesion);
        public Dominio? ConsultarDependencias(int idDominio);
        public IEnumerable<DominioDto> ConsultarTodosParaSelector();
    }
}
