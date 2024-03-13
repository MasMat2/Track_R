using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Entidad
    {
        public Entidad()
        {
            EntidadEstructura = new HashSet<EntidadEstructura>();
        }

        public int IdEntidad { get; set; }
        public string? Clave { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<EntidadEstructura> EntidadEstructura { get; set; }
    }
}
