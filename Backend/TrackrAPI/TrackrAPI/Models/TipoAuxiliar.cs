using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoAuxiliar
    {
        public TipoAuxiliar()
        {
            Auxiliar = new HashSet<Auxiliar>();
            Concepto = new HashSet<Concepto>();
            CuentaContable = new HashSet<CuentaContable>();
            Jerarquia = new HashSet<Jerarquia>();
        }

        public int IdTipoAuxiliar { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int? IdTipoCuentaContable { get; set; }

        public virtual TipoCuentaContable? IdTipoCuentaContableNavigation { get; set; }
        public virtual ICollection<Auxiliar> Auxiliar { get; set; }
        public virtual ICollection<Concepto> Concepto { get; set; }
        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
        public virtual ICollection<Jerarquia> Jerarquia { get; set; }
    }
}
