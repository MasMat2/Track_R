using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoActivo
    {
        public TipoActivo()
        {
            Caja = new HashSet<Caja>();
        }

        public int IdTipoActivo { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? IdCuentaContable { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual CuentaContable? IdCuentaContableNavigation { get; set; }
        public virtual ICollection<Caja> Caja { get; set; }
    }
}
