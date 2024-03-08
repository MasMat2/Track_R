using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public interface IFlujoDetalleResponsableRepository : IRepository<FlujoDetalleResponsable>
    {
        public FlujoDetalleResponsable Consultar(int idFlujoDetalleResponsable);
        public List<FlujoDetalleResponsable> ConsultarPorFlujoDetalle(int idFlujoDetalle);
    }
}
