using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GastoConcepto
    {
        public int IdGastoConcepto { get; set; }
        public decimal Monto { get; set; }
        public int IdConcepto { get; set; }
        public int IdGasto { get; set; }
        public int IdImpuesto { get; set; }

        public virtual Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual Gasto IdGastoNavigation { get; set; } = null!;
        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
    }
}
