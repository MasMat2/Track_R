using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IAccesoRepository : IRepository<Acceso>
    {
        Acceso Consultar(int idAcceso);
    }
}
