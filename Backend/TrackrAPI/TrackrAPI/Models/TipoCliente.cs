using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoCliente
    {
        public TipoCliente()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdTipoCliente { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
