using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class BalanceCuentaContable
    {
        public int IdBalanceCuentaContable { get; set; }
        public decimal Cargo { get; set; }
        public decimal Abono { get; set; }
        public decimal SaldoInicial { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int IdCuentaContable { get; set; }
        public int IdCompania { get; set; }
        public bool? EsPresupuesto { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual CuentaContable IdCuentaContableNavigation { get; set; } = null!;
    }
}
