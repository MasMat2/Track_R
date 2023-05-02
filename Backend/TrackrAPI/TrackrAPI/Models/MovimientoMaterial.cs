using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class MovimientoMaterial
    {
        public MovimientoMaterial()
        {
            Liquidacion = new HashSet<Liquidacion>();
            MovimientoMaterialDetalle = new HashSet<MovimientoMaterialDetalle>();
            NotaGasto = new HashSet<NotaGasto>();
            NotaVenta = new HashSet<NotaVenta>();
            TraspasoMovimientoMaterialIdMovimientoMaterialEntradaNavigation = new HashSet<TraspasoMovimientoMaterial>();
            TraspasoMovimientoMaterialIdMovimientoMaterialSalidaNavigation = new HashSet<TraspasoMovimientoMaterial>();
        }

        public int IdMovimientoMaterial { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaMovimiento { get; set; }
        public int IdTipoMovimientoMaterial { get; set; }
        public int IdAlmacen { get; set; }
        public int IdUsuarioAlmacenista { get; set; }
        public int? IdOrdenCompra { get; set; }
        public int IdEstatusMovimientoMaterial { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaContable { get; set; }
        public bool? EntregaCompleta { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdUsuarioProveedor { get; set; }
        public int? IdExpedienteAdministrativo { get; set; }

        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual EstatusMovimientoMaterial IdEstatusMovimientoMaterialNavigation { get; set; } = null!;
        public virtual ExpedienteAdministrativo? IdExpedienteAdministrativoNavigation { get; set; }
        public virtual OrdenCompra? IdOrdenCompraNavigation { get; set; }
        public virtual TipoMovimientoMaterial IdTipoMovimientoMaterialNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAlmacenistaNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioProveedorNavigation { get; set; }
        public virtual ICollection<Liquidacion> Liquidacion { get; set; }
        public virtual ICollection<MovimientoMaterialDetalle> MovimientoMaterialDetalle { get; set; }
        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterial> TraspasoMovimientoMaterialIdMovimientoMaterialEntradaNavigation { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterial> TraspasoMovimientoMaterialIdMovimientoMaterialSalidaNavigation { get; set; }
    }
}
