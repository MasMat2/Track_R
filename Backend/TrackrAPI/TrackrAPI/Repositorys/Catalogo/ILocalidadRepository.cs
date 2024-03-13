using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface ILocalidadRepository : IRepository<Localidad>
    {
        public Localidad Consultar(int idLocalidad);
        public IEnumerable<Localidad> ConsultarPorEstado(int idEstado);
    }
}
