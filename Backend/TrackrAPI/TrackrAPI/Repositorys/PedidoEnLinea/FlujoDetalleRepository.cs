using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Data;
using TrackrAPI.Dtos.PedidoEnLinea;

namespace TrackrAPI.Repositorys.PedidoEnLinea
{
    public class FlujoDetalleRepository : Repository<FlujoDetalle>, IFlujoDetalleRepository
    {
        public FlujoDetalleRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public FlujoDetalle ConsultarPrimerFlujoPorPresentacion(int idPresentacion, int idCompania, string claveOpcionVenta)
        {
            int idFlujo;
            var configuracionOpcionVenta = context.ConfiguracionOpcionVenta
                .Include(c => c.IdOpcionVentaNavigation)
                .Where(c => c.IdPresentacion == idPresentacion && c.IdOpcionVentaNavigation.Clave == claveOpcionVenta)
                .FirstOrDefault();

            if (configuracionOpcionVenta != null)
            {
                idFlujo = configuracionOpcionVenta.IdFlujo;
            }
            else // Si la presentación no cuenta con la configuración de opcion de venta
            {
                // Se consulta el flujo estándar default de la compañía
                idFlujo = context.Flujo.Where(f => f.IdCompania == idCompania && f.EsDefault == true)
                          .FirstOrDefault().IdFlujo;
            }

            return context.FlujoDetalle
                .Include(fd => fd.IdFlujoNavigation)
                .Where(fd => fd.IdFlujo == idFlujo)
                .OrderBy(fd => fd.Orden)
                .FirstOrDefault();
        }

        public FlujoDetalle Consultar(int idFlujoDetalle)
        {
            return context.FlujoDetalle
                .Include(f => f.FlujoDetalleResponsable)
                    .ThenInclude(fdr => fdr.IdUsuarioNavigation)
                .Where(fd => fd.IdFlujoDetalle == idFlujoDetalle)
                .FirstOrDefault();
        }

        public IEnumerable<FlujoDetalleDto> ConsultarPorFlujo(int idFlujo)
        {
            return context.FlujoDetalle.Where(e => e.IdFlujo == idFlujo)
                .Select(f => new FlujoDetalleDto
                {
                    IdFlujo = f.IdFlujo,
                    IdFlujoDetalle = f.IdFlujoDetalle,
                    IdRol = f.IdRol,
                    IdEstatusPedido = f.IdEstatusPedido,
                    Orden = f.Orden,
                    NombreEstatusPedido = f.IdEstatusPedidoNavigation.Nombre,
                    NombreRol = f.IdRolNavigation.Nombre,
                    ClaveEstatusPedido = f.IdEstatusPedidoNavigation.Clave,
                    SolicitarResponsable = f.SolicitarResponsable
                })
                .ToList();
        }

        public IEnumerable<FlujoDetalleGridDto> ConsultarParaGrid(int idFlujo)
        {
            return context.FlujoDetalle
                .Where(fd => fd.IdFlujo == idFlujo)
                .Select(fd => new FlujoDetalleGridDto
                {
                    IdFlujoDetalle = fd.IdFlujoDetalle,
                    NombreRol = fd.IdRolNavigation.Nombre,
                    NombreEstatusPedido = fd.IdEstatusPedidoNavigation.Nombre,
                    Orden = fd.Orden
                })
                .OrderBy(fd => fd.Orden);
        }

        public bool TieneDependencias(int idFlujoDetalle)
        {
            FlujoDetalle flujoDetalle = context.FlujoDetalle
                .Where(fd => fd.IdFlujoDetalle == idFlujoDetalle)
                .Include(fd => fd.FlujoDetalleAplicado)
                .FirstOrDefault();

            if (flujoDetalle == null)
                return false;

            if (flujoDetalle.FlujoDetalleAplicado.Any())
                return true;

            return false;
        }
    }
}
