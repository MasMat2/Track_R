using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Farmacia;
using TrackrAPI.Dtos.Pdfs;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class NotaVentaDetalleRepository : Repository<NotaVentaDetalle>, INotaVentaDetalleRepository
    {
        public NotaVentaDetalleRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public NotaVentaDetalle Consultar(int idNotaVentaDetalle)
        {
            return context.NotaVentaDetalle.Where(e => e.IdNotaVentaDetalle == idNotaVentaDetalle).FirstOrDefault();
        }

        public IEnumerable<NotaVentaDetalle> ConsultarOrdenadosPorCantidadPresentacion(int cantidad, int idCompania)
        {
            return context.NotaVentaDetalle
                .Include(a => a.IdPresentacionNavigation)
                .Include(a => a.IdPresentacionNavigation.PresentacionImagen).ToList()
                .Where(a => !a.IdPresentacionNavigation.EsServicio && a.IdPresentacionNavigation.IdCompania == idCompania)
                .GroupBy(a => a.IdPresentacion)
                .Select(group => new NotaVentaDetalle
                {
                    IdPresentacion = group.Key,
                    Cantidad = group.Sum(c => c.Cantidad),
                    IdPresentacionNavigation = group.Where(a => a.IdPresentacion == group.Key).FirstOrDefault().IdPresentacionNavigation
                })
                .OrderByDescending(a => a.Cantidad).Take(cantidad);

        }

        public IEnumerable<NotaVentaDetalle> ConsultarPorNotaVentaGrid(int idNotaVenta)
        {
            return context.NotaVentaDetalle
                .Include(a => a.IdImpuestoNavigation)
                .Include(a => a.IdUbicacionNavigation)
                .Include(a => a.IdPresentacionNavigation)
                .Include(a => a.IdNotaVentaNavigation)
                .Include(a => a.IdNotaVentaNavigation.IdUsuarioAltaNavigation)
                .Include(a => a.Comision).ThenInclude(c => c.IdUsuarioNavigation)
                .Where(a => a.IdNotaVenta == idNotaVenta);
        }

        public IEnumerable<NotaVentaDetalle> ConsultarPorNotaVenta(int idNotaVenta)
        {
            return context.NotaVentaDetalle
                .Include(a => a.IdPresentacionNavigation)
                .Where(a => a.IdNotaVenta == idNotaVenta)
                .ToList();
        }

        public IEnumerable<NotaVentaDetalleDto> ConsultarPorRecibos(int[] idsRecibos)
        {
            return context.NotaVentaDetalle
                .Include(a => a.IdPresentacionNavigation)
                .Where(a => idsRecibos.Contains(a.IdNotaVentaNavigation.IdRecibo))
                .Select(nvd => new NotaVentaDetalleDto
                {
                    IdNotaVentaDetalle = nvd.IdNotaVentaDetalle,
                    Cantidad = nvd.Cantidad,
                    Importe = (double) nvd.Cantidad * (double) nvd.PrecioUnitario,
                    PrecioUnitario = nvd.PrecioUnitario,
                    NombrePresentacion = nvd.IdPresentacionNavigation.Nombre,
                    IdNotaVenta = nvd.IdNotaVenta,
                    IdPresentacion = nvd.IdPresentacion,
                    IdImpuesto = nvd.IdImpuesto,
                    Impuesto = nvd.Impuesto == null ? 0 : (decimal)nvd.Impuesto
                })
                .ToList();
        }

        public IEnumerable<NotaVentaDetalleDto> ConsultarPorNotas(int[] idsNotas)
        {
            return context.NotaVentaDetalle
                .Include(a => a.IdPresentacionNavigation)
                .Where(a => idsNotas.Contains(a.IdNotaVenta))
                .Select(nvd => new NotaVentaDetalleDto
                {
                    IdNotaVentaDetalle = nvd.IdNotaVentaDetalle,
                    Cantidad = nvd.Cantidad,
                    Importe = (double)nvd.Cantidad * (double)nvd.PrecioUnitario,
                    PrecioUnitario = nvd.PrecioUnitario,
                    NombrePresentacion = nvd.IdPresentacionNavigation.Nombre,
                    IdNotaVenta = nvd.IdNotaVenta,
                    IdPresentacion = nvd.IdPresentacion,
                    IdImpuesto = nvd.IdImpuesto,
                    Impuesto = nvd.Impuesto == null ? 0 : (decimal)nvd.Impuesto
                })
                .ToList();
        }

        public IEnumerable<NotaVentaDetalle> ConsultarPorRecibo(int idRecibo)
        {
            return context.NotaVentaDetalle
                .Include(a => a.IdImpuestoNavigation)
                .Where(a => a.IdNotaVentaNavigation.IdRecibo == idRecibo)
                .ToList();
        }

        public IEnumerable<NotaVentaDetalle> ConsultarCantidadDevuelta(int idNotaVentaPadreDevolucion, int idPresentacion)
        {
            return context.NotaVentaDetalle
                .Where(a => a.IdNotaVentaPadreDevolucion == idNotaVentaPadreDevolucion && a.IdPresentacion == idPresentacion);

        }

        public IEnumerable<NotaVentaDetalle> ConsultarFiltroParaGrid(NotaVentaDetalleFiltroDto filtro)
        {
            return context.NotaVentaDetalle
                .Include(nvd => nvd.IdNotaVentaNavigation)
                .Include(nvd => nvd.IdPresentacionNavigation)
                .Include(nvd => nvd.IdPresentacionNavigation.PresentacionArticulo)
                    .ThenInclude(pa => pa.IdArticuloNavigation.IdCategoriaNavigation)
                .Include(nvd => nvd.IdPresentacionNavigation.PresentacionArticulo)
                    .ThenInclude(pa => pa.IdArticuloNavigation.IdSubCategoriaNavigation)
                .Include(nvd => nvd.IdPresentacionNavigation.PresentacionArticulo)
                    .ThenInclude(pa => pa.IdArticuloNavigation.IdSubSubCategoriaNavigation)
                .Where(nvd =>
                       nvd.IdNotaVentaNavigation.IdReciboNavigation.IdHospital == filtro.IdLocacion &&
                       nvd.IdNotaVentaNavigation.IdEstatusNotaVentaNavigation.Clave == GeneralConstant.ClaveEstatusNotaVentaActiva &&
                       nvd.IdNotaVentaNavigation.IdTipoNotaVentaNavigation.Clave == GeneralConstant.ClaveTipoNotaDeVenta)
                .Where(nvd =>
                    (filtro.FechaInicial == null || ((DateTime)nvd.IdNotaVentaNavigation.FechaContable).Date >= ((DateTime)filtro.FechaInicial).Date) &&
                    (filtro.FechaFinal == null || ((DateTime)nvd.IdNotaVentaNavigation.FechaContable).Date <= ((DateTime)filtro.FechaFinal).Date) &&
                    ((string.IsNullOrEmpty(filtro.Presentacion) || filtro.Presentacion == "undefined") || nvd.IdPresentacionNavigation.Nombre.ToLower().Contains(filtro.Presentacion.ToLower())) &&
                    (filtro.IdCategoria == 0 || nvd.IdPresentacionNavigation.PresentacionArticulo.FirstOrDefault().IdArticuloNavigation.IdCategoria == filtro.IdCategoria)
                );
        }

        public IEnumerable<NotaVentaTotalAcumuladoDto> ConsultarTotalAcumuladoCategoriaParaGrid(NotaVentaDetalleFiltroDto filtro)
        {
            List<NotaVentaTotalAcumuladoDto> acumuladoList = new List<NotaVentaTotalAcumuladoDto>();

            var clasificaciones = new string[] { "Categoria", "SubCategoria", "SubSubCategoria" };
            var nombreConcepto = "";
            var nvdList = ConsultarFiltroParaGrid(filtro)
                .Select(nvd => new
                {
                    Categoria = nvd.IdPresentacionNavigation.ObtenerNombreCategoria(),
                    SubCategoria = nvd.IdPresentacionNavigation.ObtenerNombreSubCategoria(),
                    SubSubCategoria = nvd.IdPresentacionNavigation.ObtenerNombreSubSubCategoria(),
                    SubTotal = nvd.Cantidad * nvd.PrecioUnitario
                })
                .ToList();

            foreach (var clasificacion in clasificaciones)
            {
                switch (clasificacion)
                {
                    case "Categoria":
                        nombreConcepto = "Categoría";
                        break;
                    case "SubCategoria":
                        nombreConcepto = "SubCategoría";
                        break;
                    case "SubSubCategoria":
                        nombreConcepto = "Sub-SubCategoría";
                        break;

                    default:
                        break;
                }

                var concepto = new NotaVentaTotalAcumuladoDto
                {
                    Categoria = nombreConcepto,
                    EsConcepto = true
                };

                acumuladoList.Add(concepto);

                var group = nvdList
                    .GroupBy(nvd => nvd.GetType().GetProperty(clasificacion).GetValue(nvd, null))
                    .Select(g => new NotaVentaTotalAcumuladoDto
                    {
                        Categoria = (string)g.Key,
                        EsConcepto = false,
                        Total = g.Sum(a => a.SubTotal),
                        Cantidad = g.Count()
                    })
                    .OrderBy(s => s.Categoria)
                    .ToList();

                acumuladoList = acumuladoList.Union(group).ToList();
            }

            return acumuladoList;
        }

        public IEnumerable<NotaVentaDetalleReporteEstadoResultadoDto> ConsultarPorFiltroParaEstadoResultado(EstadoResultadoFiltroDto filtro, string claveTipoNotaVenta)
        {
            var detalles = context.NotaVentaDetalle
                            .Include(nvd => nvd.IdNotaVentaNavigation)
                            .Include(nvd => nvd.IdPresentacionNavigation)
                            .Include(nvd => nvd.IdPresentacionNavigation.PresentacionArticulo)
                                .ThenInclude(pa => pa.IdArticuloNavigation.IdCategoriaNavigation)
                            .Where(nvd => nvd.IdNotaVentaNavigation.IdEstatusNotaVentaNavigation.Clave == GeneralConstant.ClaveEstatusNotaVentaActiva &&
                                   nvd.IdNotaVentaNavigation.IdTipoNotaVentaNavigation.Clave == claveTipoNotaVenta &&
                                   nvd.IdNotaVentaNavigation.IdReciboNavigation.IdHospital == filtro.IdLocacion)
                            .Where(nvd =>
                                 nvd.IdNotaVentaNavigation.FechaContable >= filtro.FechaInicio.Date &&
                                (filtro.FechaFin == null || nvd.IdNotaVentaNavigation.FechaContable <= ((DateTime)filtro.FechaFin).Date)
                            )
                            .Select(nvd => new NotaVentaDetalleReporteEstadoResultadoDto
                            {
                                FechaAlta = nvd.IdNotaVentaNavigation.FechaAlta,
                                Cantidad = nvd.Cantidad,
                                NombrePresentacion = nvd.IdPresentacionNavigation.Nombre,
                                Categoria = nvd.IdPresentacionNavigation.ObtenerNombreCategoria(),
                                Importe = nvd.Cantidad * nvd.PrecioUnitario
                            });
            return detalles;
        }
    }
}
