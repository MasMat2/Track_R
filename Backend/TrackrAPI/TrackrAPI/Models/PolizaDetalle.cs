using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PolizaDetalle
    {
        public PolizaDetalle()
        {
            PolizaAplicadaDetalle = new HashSet<PolizaAplicadaDetalle>();
        }

        public int IdPolizaDetalle { get; set; }
        public string? Concepto { get; set; }
        public decimal? Cargo { get; set; }
        public decimal? Abono { get; set; }
        public int Renglon { get; set; }
        public int IdPoliza { get; set; }
        public int IdCuentaContable { get; set; }
        public int? IdAuxiliar { get; set; }
        public int? IdImpuestoDetalle { get; set; }
        public int? IdImpuesto { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual CuentaContable IdCuentaContableNavigation { get; set; } = null!;
        public virtual ImpuestoDetalle? IdImpuestoDetalleNavigation { get; set; }
        public virtual Impuesto? IdImpuestoNavigation { get; set; }
        public virtual Poliza IdPolizaNavigation { get; set; } = null!;
        public virtual ICollection<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; }
    }
}
