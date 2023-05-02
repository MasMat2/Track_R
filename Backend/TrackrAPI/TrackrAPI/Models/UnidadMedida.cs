using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int IdUnidadMedida { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
