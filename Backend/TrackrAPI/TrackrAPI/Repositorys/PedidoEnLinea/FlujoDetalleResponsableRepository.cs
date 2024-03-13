using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public class FlujoDetalleResponsableRepository : Repository<FlujoDetalleResponsable>, IFlujoDetalleResponsableRepository
    {
        public FlujoDetalleResponsableRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public FlujoDetalleResponsable Consultar(int idFlujoDetalleResponsable)
        {
            return context.FlujoDetalleResponsable
                .Where(f => f.IdFlujoDetalleResponsable == idFlujoDetalleResponsable)
                .FirstOrDefault();
        }

        public List<FlujoDetalleResponsable> ConsultarPorFlujoDetalle(int idFlujoDetalle)
        {
            return context.FlujoDetalleResponsable
                .Where(fr => fr.IdFlujoDetalle == idFlujoDetalle)
                .ToList();
        }
    }
}
