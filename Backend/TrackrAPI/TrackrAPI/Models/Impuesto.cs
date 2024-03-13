using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Impuesto
    {
        public Impuesto()
        {
            FacturaConcepto = new HashSet<FacturaConcepto>();
            GastoConcepto = new HashSet<GastoConcepto>();
            ImpuestoDetalle = new HashSet<ImpuestoDetalle>();
            ListaPrecioDetalle = new HashSet<ListaPrecioDetalle>();
            NotaFlujoDetalle = new HashSet<NotaFlujoDetalle>();
            NotaGastoDetalle = new HashSet<NotaGastoDetalle>();
            NotaVentaDetalle = new HashSet<NotaVentaDetalle>();
            OrdenCompraDetalle = new HashSet<OrdenCompraDetalle>();
            PolizaAplicadaDetalle = new HashSet<PolizaAplicadaDetalle>();
            PolizaDetalle = new HashSet<PolizaDetalle>();
            ReciboDetalle = new HashSet<ReciboDetalle>();
            RemisionPresentacion = new HashSet<RemisionPresentacion>();
        }

        public int IdImpuesto { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal PorcentajeNeto { get; set; }
        public int? IdCuentaContableRedondeo { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContableRedondeoNavigation { get; set; }
        public virtual ICollection<FacturaConcepto> FacturaConcepto { get; set; }
        public virtual ICollection<GastoConcepto> GastoConcepto { get; set; }
        public virtual ICollection<ImpuestoDetalle> ImpuestoDetalle { get; set; }
        public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalle { get; set; }
        public virtual ICollection<NotaFlujoDetalle> NotaFlujoDetalle { get; set; }
        public virtual ICollection<NotaGastoDetalle> NotaGastoDetalle { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalle { get; set; }
        public virtual ICollection<OrdenCompraDetalle> OrdenCompraDetalle { get; set; }
        public virtual ICollection<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; }
        public virtual ICollection<PolizaDetalle> PolizaDetalle { get; set; }
        public virtual ICollection<ReciboDetalle> ReciboDetalle { get; set; }
        public virtual ICollection<RemisionPresentacion> RemisionPresentacion { get; set; }
    }
}
