using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class AgrupadorCuentaContable
    {
        public AgrupadorCuentaContable()
        {
            Compania = new HashSet<Compania>();
            CuentaContable = new HashSet<CuentaContable>();
        }

        public int IdAgrupadorCuentaContable { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? IdCuentaContableCapital { get; set; }
        public int? IdCuentaContableResultado { get; set; }

        public virtual CuentaContable? IdCuentaContableCapitalNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContableResultadoNavigation { get; set; }
        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
    }
}
