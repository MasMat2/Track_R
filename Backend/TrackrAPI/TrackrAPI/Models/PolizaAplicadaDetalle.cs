using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PolizaAplicadaDetalle
    {
        public PolizaAplicadaDetalle()
        {
            InverseIdPartidaVivaNavigation = new HashSet<PolizaAplicadaDetalle>();
        }

        public int IdPolizaAplicadaDetalle { get; set; }
        public string? Concepto { get; set; }
        public decimal? Cargo { get; set; }
        public decimal? Abono { get; set; }
        public int Renglon { get; set; }
        public int IdPolizaAplicada { get; set; }
        public int IdCuentaContable { get; set; }
        public int? IdAuxiliar { get; set; }
        public int? IdImpuestoDetalle { get; set; }
        public int? IdImpuesto { get; set; }
        public int? IdPartidaViva { get; set; }
        public bool EstaViva { get; set; }
        public int? IdPolizaDetalle { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual CuentaContable IdCuentaContableNavigation { get; set; } = null!;
        public virtual ImpuestoDetalle? IdImpuestoDetalleNavigation { get; set; }
        public virtual Impuesto? IdImpuestoNavigation { get; set; }
        public virtual PolizaAplicadaDetalle? IdPartidaVivaNavigation { get; set; }
        public virtual PolizaAplicada IdPolizaAplicadaNavigation { get; set; } = null!;
        public virtual PolizaDetalle? IdPolizaDetalleNavigation { get; set; }
        public virtual ICollection<PolizaAplicadaDetalle> InverseIdPartidaVivaNavigation { get; set; }
    }
}
