using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class ImpuestoDetalleRepository : Repository<ImpuestoDetalle>, IImpuestoDetalleRepository
    {
        public ImpuestoDetalleRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ImpuestoDetalleGridDto> ConsultarTodosPorImpuestoParaGrid(int idImpuesto)
        {
            return context.ImpuestoDetalle
                .OrderBy(id => id.Descripcion)
                .Where(id => id.IdImpuesto == idImpuesto)
                .Select(id => new ImpuestoDetalleGridDto
                {
                    IdImpuestoDetalle = id.IdImpuestoDetalle,
                    Descripcion = id.Descripcion,
                    CuentaCargoNombre = id.IdCuentaContableCargoNavigation.NumeroNombre(),
                    CuentaAbonoNombre = id.IdCuentaContableAbonoNavigation.NumeroNombre(),
                    Valor = id.Valor,
                    MovimientoContrario = id.MovimientoContrario,
                    Retencion = id.Retencion,
                    ClaveImpuestoSat = id.IdTipoImpuestoNavigation.Clave,

                })
                .ToList();
        }

        public IEnumerable<ImpuestoDetalle> ConsultarPorImpuesto(int idImpuesto)
        {
            return context.ImpuestoDetalle
                    .Include(id => id.IdCuentaContableAbonoNavigation)
                    .Include(id => id.IdCuentaContableCargoNavigation)
                .Where(id => id.IdImpuesto == idImpuesto)
                .ToList();
        }

        public ImpuestoDetalleDto ConsultarDto(int idImpuestoDetalle)
        {
            return context.ImpuestoDetalle
                .Where(id => id.IdImpuestoDetalle == idImpuestoDetalle)
                .Select(id => new ImpuestoDetalleDto
                {
                    Descripcion = id.Descripcion,
                    IdImpuestoDetalle = id.IdImpuestoDetalle,
                    IdImpuesto = id.IdImpuesto,
                    IdTipoImpuesto = id.IdTipoImpuesto,
                    Valor = id.Valor,
                    IdCuentaContableAbono = id.IdCuentaContableAbono,
                    IdCuentaContableCargo = id.IdCuentaContableCargo,
                    MovimientoContrario = id.MovimientoContrario,
                    Retencion = id.Retencion,
                    ClaveImpuestoSat = id.IdTipoImpuestoNavigation.Clave
                })
                .FirstOrDefault();
        }

        public ImpuestoDetalle Consultar(int idImpuestoDetalle)
        {
            return context.ImpuestoDetalle
                .Where(id => id.IdImpuestoDetalle == idImpuestoDetalle)
                .FirstOrDefault();
        }

        public ImpuestoDetalle ConsultarDependencias(int idImpuestoDetalle)
        {
            return context.ImpuestoDetalle
                .Include(id => id.PolizaDetalle)
                .Include(id => id.PolizaAplicadaDetalle)
                .Where(id => id.IdImpuestoDetalle == idImpuestoDetalle)
                .FirstOrDefault();
        }
    }
}
