using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.CanalDistribucion;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IListaPrecioDetalleRepository : IRepository<ListaPrecioDetalle>
    {
        IEnumerable<ListaPrecioDetalleGridDto> ConsultarPorListaPrecioParaGrid(int idListaPrecio, int idArticulo);
        IEnumerable<ListaPrecioDetalleGridDto> ConsultarPorListaPrecioPorPresentacionParaGrid(int idListaPrecio, List<int> presentacionList);
        IEnumerable<ListaPrecioDetalleGridDto> ConsultarPorFiltro(ListaPrecioDetalleFiltroDto filtro);
        IEnumerable<PresentacionCanalDto> ConsultarPorListaPrecioParaCanal(int idListaPrecio);
        ListaPrecioDetalleDto ConsultarDto(int idListaPrecioDetalle);
        ListaPrecioDetalle Consultar(int idListaPrecioDetalle);
        ListaPrecioDetalle ConsultarVigentePorPresentacion(int idPresentacion, int idHospital);
        ListaPrecioDetalle ConsultarPorListaPrecioPorPresentacion(int idListaPrecio, int idPresentacion);
        ListaPrecioDetalle ConsultarPorPresentacion(int idPresentacion);
        IEnumerable<ListaPrecioDetalle> ConsultarPorListaPrecio(int idListaPrecio);
        IEnumerable<PrecioListaPresentacionCanalDto> ConsultarVigenteParaCanal(int idListaPrecio);
    }
}
