using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ViaAdministracion
    {
        public ViaAdministracion()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int IdViaAdministracion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
