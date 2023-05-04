using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Dtos.PedidoEnLinea;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public interface IFlujoDetalleRepository : IRepository<FlujoDetalle>
    {
        public FlujoDetalle Consultar(int idFlujoDetalle);
        public FlujoDetalle ConsultarPrimerFlujoPorPresentacion(int idPresentacion, int idCompania, string claveOpcionVenta);
        public IEnumerable<FlujoDetalleDto> ConsultarPorFlujo(int idFlujo);
        public IEnumerable<FlujoDetalleGridDto> ConsultarParaGrid(int idFlujo);
        public bool TieneDependencias(int idFlujoDetalle);
    }
}
