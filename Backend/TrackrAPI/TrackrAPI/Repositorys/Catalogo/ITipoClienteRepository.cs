using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ITipoClienteRepository : IRepository<TipoCliente>
    {
        List<TipoCliente> ConsultarPorCompania(int idCompania);
    }
}
