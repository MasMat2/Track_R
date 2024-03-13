using TrackrAPI.Models;
using TrackrAPI.Repositorys;
using TrackrAPI.Repositorys.GestionCaja;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionCaja
{
    public class MetodoPagoRepository : Repository<MetodoPago>, IMetodoPagoRepository
    {
        public MetodoPagoRepository(TrackrContext context) : base(context) {
            base.context = context;
        }

        public IEnumerable<MetodoPago> ConsultarTodos()
        {
            return context.MetodoPago;
        }

        public MetodoPago ConsultarPorClave(string clave)
        {
            return context.MetodoPago
                .Where(mp => mp.Clave == clave)
                .FirstOrDefault();
        }
    }
}
