using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IBancoRepository : IRepository<Banco>
    {
        public IEnumerable<BancoDto> ConsultarTodosParaSelector();
    }
}
