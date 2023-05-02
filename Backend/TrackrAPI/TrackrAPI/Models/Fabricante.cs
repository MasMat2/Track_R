using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Fabricante
    {
        public Fabricante()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int IdFabricante { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
