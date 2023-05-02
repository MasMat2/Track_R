using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Remision
    {
        public Remision()
        {
            CobranzaPago = new HashSet<CobranzaPago>();
            RemisionPresentacion = new HashSet<RemisionPresentacion>();
        }

        public int IdRemision { get; set; }
        public string Folio { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdLiquidacion { get; set; }
        public bool Habilitado { get; set; }
        public int IdEstatusRemision { get; set; }
        public DateTime Fecha { get; set; }
        public int IdDomicilioSucursal { get; set; }
        public int IdMetodoPago { get; set; }
        public int? IdFactura { get; set; }

        public virtual Domicilio IdDomicilioSucursalNavigation { get; set; } = null!;
        public virtual EstatusRemision IdEstatusRemisionNavigation { get; set; } = null!;
        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual Liquidacion IdLiquidacionNavigation { get; set; } = null!;
        public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;
        public virtual ICollection<CobranzaPago> CobranzaPago { get; set; }
        public virtual ICollection<RemisionPresentacion> RemisionPresentacion { get; set; }
    }
}
