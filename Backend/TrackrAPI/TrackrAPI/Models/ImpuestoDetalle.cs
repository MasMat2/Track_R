using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ImpuestoDetalle
    {
        public ImpuestoDetalle()
        {
            PolizaAplicadaDetalle = new HashSet<PolizaAplicadaDetalle>();
            PolizaDetalle = new HashSet<PolizaDetalle>();
        }

        public int IdImpuestoDetalle { get; set; }
        public decimal Valor { get; set; }
        public string? Descripcion { get; set; }
        public bool Retencion { get; set; }
        public bool MovimientoContrario { get; set; }
        public int IdImpuesto { get; set; }
        public int IdTipoImpuesto { get; set; }
        public int? IdCuentaContableCargo { get; set; }
        public int? IdCuentaContableAbono { get; set; }

        public virtual CuentaContable? IdCuentaContableAbonoNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContableCargoNavigation { get; set; }
        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
        public virtual TipoImpuesto IdTipoImpuestoNavigation { get; set; } = null!;
        public virtual ICollection<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; }
        public virtual ICollection<PolizaDetalle> PolizaDetalle { get; set; }
    }
}
