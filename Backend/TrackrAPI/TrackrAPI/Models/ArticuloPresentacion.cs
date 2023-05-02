using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ArticuloPresentacion
    {
        public ArticuloPresentacion()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int IdArticuloPresentacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
