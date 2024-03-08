using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Farmacia;
using TrackrAPI.Dtos.Pdfs;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface INotaVentaDetalleRepository : IRepository<NotaVentaDetalle>
    {
        NotaVentaDetalle Consultar(int idNotaVentaDetalle);
        IEnumerable<NotaVentaDetalle> ConsultarOrdenadosPorCantidadPresentacion(int cantidad, int idCompania);
        IEnumerable<NotaVentaDetalle> ConsultarPorNotaVentaGrid(int idNotaVenta);
        IEnumerable<NotaVentaDetalle> ConsultarCantidadDevuelta(int idNotaVentaPadreDevolucion, int idPresentacion);
        IEnumerable<NotaVentaDetalle> ConsultarPorNotaVenta(int idNotaVenta);
        IEnumerable<NotaVentaDetalleDto> ConsultarPorRecibos(int[] idsRecibos);
        IEnumerable<NotaVentaDetalleDto> ConsultarPorNotas(int[] idsNotas);
        IEnumerable<NotaVentaDetalle> ConsultarPorRecibo(int idRecibo);
        IEnumerable<NotaVentaDetalle> ConsultarFiltroParaGrid(NotaVentaDetalleFiltroDto filtro);
        IEnumerable<NotaVentaTotalAcumuladoDto> ConsultarTotalAcumuladoCategoriaParaGrid(NotaVentaDetalleFiltroDto filtro);
        IEnumerable<NotaVentaDetalleReporteEstadoResultadoDto> ConsultarPorFiltroParaEstadoResultado(EstadoResultadoFiltroDto filtro, string claveTipoNotaVenta);
    }
}
