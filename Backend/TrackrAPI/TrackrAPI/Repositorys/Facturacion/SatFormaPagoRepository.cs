using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Facturacion
{
    public class SatFormaPagoRepository : Repository<SatFormaPago>, ISatFormaPagoRepository
    {
        public SatFormaPagoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public SatFormaPago Consultar(int idSatFormaPago)
        {
            return context.SatFormaPago
                    .Where(s => s.IdSatFormaPago == idSatFormaPago)
                    .FirstOrDefault();
        }
        public IEnumerable<SatFormaPago> ConsultarParaSelector()
        {
            return context.SatFormaPago
                .Select(f => new SatFormaPago
                {
                    IdSatFormaPago = f.IdSatFormaPago,
                    Clave = f.Clave,
                    Nombre = f.Nombre,
                }).ToList();
        }

        public SatFormaPago ConsultarPorClave(string clave)
        {
            return context.SatFormaPago
                .Where(s => s.Clave == clave)
                .FirstOrDefault();
        }

    }
}
