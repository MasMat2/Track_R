using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class AccesoRepository : Repository<Acceso>, IAccesoRepository
    {
        public AccesoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Acceso Consultar(int idAcceso)
        {
            var acceso =
                from a in context.Acceso
                .Include(a => a.IdTipoAccesoNavigation)
                where a.IdAcceso == idAcceso
                select a;

            return acceso.First();
        }
    }
}
