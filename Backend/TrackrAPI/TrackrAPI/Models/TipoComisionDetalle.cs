using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoComisionDetalle
    {
        public TipoComisionDetalle()
        {
            Comision = new HashSet<Comision>();
        }

        public int IdTipoComisionDetalle { get; set; }
        public int IdTipoComision { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Porcentaje { get; set; }
        public int IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual TipoComision IdTipoComisionNavigation { get; set; } = null!;
        public virtual ICollection<Comision> Comision { get; set; }
    }
}
