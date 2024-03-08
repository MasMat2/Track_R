using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SubtipoCuentaContable
    {
        public SubtipoCuentaContable()
        {
            CuentaContable = new HashSet<CuentaContable>();
        }

        public int IdSubtipoCuentaContable { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdTipoCuentaContable { get; set; }

        public virtual TipoCuentaContable IdTipoCuentaContableNavigation { get; set; } = null!;
        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
    }
}
