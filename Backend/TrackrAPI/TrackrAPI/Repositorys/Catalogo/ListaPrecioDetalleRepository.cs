using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.CanalDistribucion;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ListaPrecioDetalleRepository : Repository<ListaPrecioDetalle>, IListaPrecioDetalleRepository
    {
        public ListaPrecioDetalleRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public ListaPrecioDetalle ConsultarVigentePorPresentacion(int idPresentacion, int idHospital)
        {
            var hoy = Utileria.ObtenerFechaActual();

            return context.ListaPrecioDetalle
                      .Include(e => e.IdImpuestoNavigation)
                      .Where(e => e.IdPresentacion == idPresentacion
                      && e.IdListaPrecioNavigation.ListaPrecioClinica.Any(lpc => lpc.IdClinica == idHospital)
                      && hoy.Date >= e.IdListaPrecioNavigation.FechaInicioVigencia.Date
                      && hoy.Date <= e.IdListaPrecioNavigation.FechaFinVigencia.Date)
                      .OrderBy(e => e.Clave)
                      .Select(e => e)
                      .FirstOrDefault();
        }

        public IEnumerable<ListaPrecioDetalleGridDto> ConsultarPorListaPrecioParaGrid(int idListaPrecio, int idArticulo)
        {
            return context.ListaPrecioDetalle
                      .Where(e => e.IdListaPrecio == idListaPrecio
                      && (idArticulo == 0 || e.IdPresentacionNavigation.PresentacionArticulo.Any(pa => pa.IdArticulo == idArticulo)))
                      .OrderBy(e => e.IdPresentacionNavigation.Clave)
                      .Select(e => new ListaPrecioDetalleGridDto
                      {
                          IdListaPrecioDetalle = e.IdListaPrecioDetalle,
                          Clave = e.IdPresentacionNavigation.Clave,
                          Sku = e.IdPresentacionNavigation.Sku,
                          NombrePresentacion = e.IdPresentacionNavigation.Nombre,
                          PrecioBase = e.PrecioBase,
                          Kit = e.IdPresentacionNavigation.EsKit,
                          TipoIVA = e.IdImpuestoNavigation.Nombre,
                          TipoComision = e.IdComisionNavigation.Nombre,
                          TipoDescuento = e.IdDescuentoNavigation.Nombre
                      })
                      .ToList();
        }

        public IEnumerable<PresentacionCanalDto> ConsultarPorListaPrecioParaCanal(int idListaPrecio)
        {
            var presentacionCanal = context.ListaPrecioDetalle
                    .Where(e => e.IdListaPrecio == idListaPrecio)
                    .Select(e => new PresentacionCanalDto
                    {
                        idPresentacion = e.IdPresentacion,
                        codigoBarras = e.IdPresentacionNavigation.Sku,
                        // idProducto = e.IdPresentacionNavigation.PresentacionArticulo.Where(pa => pa.IdPresentacion == e.IdPresentacion).FirstOrDefault().IdArticuloNavigation.IdArticulo,
                        nombreTipoProducto = e.IdPresentacionNavigation.PresentacionArticulo
                            .Where(pa => pa.IdPresentacion == e.IdPresentacion).FirstOrDefault().IdArticuloNavigation.IdCategoriaNavigation.Nombre,
                        nombreSubTipoProducto = e.IdPresentacionNavigation.PresentacionArticulo
                            .Where(pa => pa.IdPresentacion == e.IdPresentacion).FirstOrDefault().IdArticuloNavigation.IdSubCategoriaNavigation.Nombre,
                        nombreProducto = e.IdPresentacionNavigation.PresentacionArticulo
                            .Where(pa => pa.IdPresentacion == e.IdPresentacion).FirstOrDefault().IdArticuloNavigation.Nombre,
                        medida = e.IdPresentacionNavigation.PresentacionArticulo
                            .Where(pa => pa.IdPresentacion == e.IdPresentacion).FirstOrDefault().IdArticuloNavigation.IdUnidadMedidaNavigation.Nombre,
                        nombre = e.IdPresentacionNavigation.Nombre,
                        nombreConFormato = e.IdPresentacionNavigation.Nombre
                    })
                    //.OrderBy(p => p.idProducto)
                    .ToList();

            var pres = presentacionCanal;
            return pres;
        }

        public IEnumerable<ListaPrecioDetalleGridDto> ConsultarPorListaPrecioPorPresentacionParaGrid(int idListaPrecio, List<int> presentacionList)
        {
            return context.ListaPrecioDetalle
                      .Where(e => e.IdListaPrecio == idListaPrecio && presentacionList.Any(p => p == e.IdPresentacion))
                      .OrderBy(e => e.IdPresentacionNavigation.Clave)
                      .Select(e => new ListaPrecioDetalleGridDto
                      {
                          IdListaPrecioDetalle = e.IdListaPrecioDetalle,
                          Clave = e.IdPresentacionNavigation.Clave,
                          Sku = e.IdPresentacionNavigation.Sku,
                          NombrePresentacion = e.IdPresentacionNavigation.Nombre,
                          PrecioBase = e.PrecioBase,
                          Kit = e.IdPresentacionNavigation.EsKit,
                          TipoIVA = e.IdImpuestoNavigation.Nombre,
                          TipoComision = e.IdComisionNavigation.Nombre,
                          TipoDescuento = e.IdDescuentoNavigation.Nombre,
                          IdDescuento = e.IdDescuento
                      })
                      .ToList();
        }

        public IEnumerable<ListaPrecioDetalleGridDto> ConsultarPorFiltro(ListaPrecioDetalleFiltroDto filtro)
        {
            filtro.Presentacion = filtro.Presentacion != null ? filtro.Presentacion.ToLower().RemoveDiacritics() : filtro.Presentacion;

            var presentaciones = context.ListaPrecioDetalle
                      .Where(e => e.IdListaPrecio == filtro.IdListaPrecio &&
                      (filtro.IdArea == null || filtro.IdArea == e.IdPresentacionNavigation.IdArea))
                      .Select(e => new ListaPrecioDetalleGridDto
                      {
                          IdListaPrecioDetalle = e.IdListaPrecioDetalle,
                          Clave = e.IdPresentacionNavigation.Clave,
                          Sku = e.IdPresentacionNavigation.Sku,
                          NombrePresentacion = e.IdPresentacionNavigation.Nombre,
                          PrecioBase = e.PrecioBase,
                          Kit = e.IdPresentacionNavigation.EsKit,
                          TipoIVA = e.IdImpuestoNavigation.Nombre,
                          TipoComision = e.IdComisionNavigation.Nombre,
                          TipoDescuento = e.IdDescuentoNavigation.Nombre,
                          IdDescuento = e.IdDescuento
                      })
                      .OrderBy(e => e.NombrePresentacion)
                      .ToList();

            return presentaciones.Where(e => string.IsNullOrWhiteSpace(filtro.Presentacion) || e.NombrePresentacion.ToLower().RemoveDiacritics().Contains(filtro.Presentacion));
        }

        public ListaPrecioDetalleDto ConsultarDto(int idListaPrecioDetalle)
        {
            return context.ListaPrecioDetalle
                .Where(lpd => lpd.IdListaPrecioDetalle == idListaPrecioDetalle)
                .Select(lpd => new ListaPrecioDetalleDto
                {
                    IdListaPrecioDetalle = lpd.IdListaPrecioDetalle,
                    Clave = lpd.Clave,
                    FechaAlta = lpd.FechaAlta,
                    PrecioBase = lpd.PrecioBase,
                    IdImpuesto = lpd.IdImpuesto,
                    IdComision = lpd.IdComision,
                    IdListaPrecio = lpd.IdListaPrecio,
                    IdPresentacion = lpd.IdPresentacion,
                    IdDescuento = lpd.IdDescuento,
                    IdUsuarioAlta = lpd.IdUsuarioAlta,
                    NombreUsuarioAlta = lpd.IdUsuarioAltaNavigation.Nombre + " " +
                    lpd.IdUsuarioAltaNavigation.ApellidoPaterno + " " +
                    lpd.IdUsuarioAltaNavigation.ApellidoMaterno
                })
                .FirstOrDefault();
        }

        public ListaPrecioDetalle Consultar(int idListaPrecioDetalle)
        {
            return context.ListaPrecioDetalle
                .Where(lpd => lpd.IdListaPrecioDetalle == idListaPrecioDetalle)
                .FirstOrDefault();
        }

        public ListaPrecioDetalle ConsultarPorListaPrecioPorPresentacion(int idListaPrecio, int idPresentacion)
        {
            return context.ListaPrecioDetalle
                .Where(lpd => lpd.IdListaPrecio == idListaPrecio && lpd.IdPresentacion == idPresentacion)
                .FirstOrDefault();
        }

        public ListaPrecioDetalle ConsultarPorPresentacion(int idPresentacion)
        {
            return context.ListaPrecioDetalle.Where(c => c.IdPresentacion == idPresentacion).FirstOrDefault();
        }

        public IEnumerable<ListaPrecioDetalle> ConsultarPorListaPrecio(int idListaPrecio)
        {
            return context.ListaPrecioDetalle
                .Where(c => c.IdListaPrecio == idListaPrecio)
                .ToList();
        }

        public IEnumerable<PrecioListaPresentacionCanalDto> ConsultarVigenteParaCanal(int idListaPrecio)
        {
            var hoy = Utileria.ObtenerFechaActual();

            return context.ListaPrecioDetalle
                .Where(c => c.IdListaPrecio == idListaPrecio
                    && hoy.Date >= c.IdListaPrecioNavigation.FechaInicioVigencia.Date
                    && hoy.Date <= c.IdListaPrecioNavigation.FechaFinVigencia.Date)
                .Select(lpd => new PrecioListaPresentacionCanalDto
                {
                    idPresentacion = lpd.IdPresentacion,
                    precio = lpd.PrecioBase
                }).ToList();

        }
    }
}
