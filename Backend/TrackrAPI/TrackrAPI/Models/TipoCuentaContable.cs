using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoCuentaContable
    {
        public TipoCuentaContable()
        {
            CuentaContable = new HashSet<CuentaContable>();
            SubtipoCuentaContable = new HashSet<SubtipoCuentaContable>();
            TipoAuxiliar = new HashSet<TipoAuxiliar>();
        }

        public int IdTipoCuentaContable { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
        public virtual ICollection<SubtipoCuentaContable> SubtipoCuentaContable { get; set; }
        public virtual ICollection<TipoAuxiliar> TipoAuxiliar { get; set; }
    }
}
