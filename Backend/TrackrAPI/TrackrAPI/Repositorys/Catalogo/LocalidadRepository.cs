using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class LocalidadRepository : Repository<Localidad>, ILocalidadRepository
    {
        public LocalidadRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Localidad Consultar(int idLocalidad)
        {
            return context.Localidad
                .Where(c => c.IdLocalidad == idLocalidad)
                .FirstOrDefault();
        }

        public IEnumerable<Localidad> ConsultarPorEstado(int idEstado)
        {
            return context.Localidad
                .Where(l => l.IdEstado == idEstado)
                .OrderBy(l => l.Nombre);
        }
    }
}
