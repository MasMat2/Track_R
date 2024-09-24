using TrackrAPI.Models;
namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IUnidadesMedidaRepository : IRepository<UnidadesMedida>
    {
        Task<UnidadesMedida>? Consultar(int idEspecialidad);
        Task<IEnumerable<UnidadesMedida>> Consultar();
        Task<UnidadesMedida?> ConsultarPorNombre(string nombre);
    }
}