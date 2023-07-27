using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoProveedor
    {
        public TipoProveedor()
        {
            Proveedor = new HashSet<Proveedor>();
        }

        public int IdTipoProveedor { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
