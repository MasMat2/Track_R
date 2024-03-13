using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class OrdenCompra
    {
        public OrdenCompra()
        {
            MovimientoMaterial = new HashSet<MovimientoMaterial>();
            OrdenCompraDetalle = new HashSet<OrdenCompraDetalle>();
        }

        public int IdOrdenCompra { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public DateTime FechaRequerida { get; set; }
        public DateTime FechaEntregaProveedor { get; set; }
        public string Concepto { get; set; } = null!;
        public string? Observaciones { get; set; }
        public int IdUsuarioComprador { get; set; }
        public int IdEstatusOrdenCompra { get; set; }
        public int IdAlmacen { get; set; }
        public bool Aprobada { get; set; }
        public int? IdDomicilio { get; set; }
        public int IdUsuarioProveedor { get; set; }

        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
        public virtual Domicilio? IdDomicilioNavigation { get; set; }
        public virtual EstatusOrdenCompra IdEstatusOrdenCompraNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioCompradorNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioProveedorNavigation { get; set; } = null!;
        public virtual ICollection<MovimientoMaterial> MovimientoMaterial { get; set; }
        public virtual ICollection<OrdenCompraDetalle> OrdenCompraDetalle { get; set; }
    }
}
