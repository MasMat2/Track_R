using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Icono
    {
        public Icono()
        {
            Acceso = new HashSet<Acceso>();
            EntidadEstructura = new HashSet<EntidadEstructura>();
            SeccionCampo = new HashSet<SeccionCampo>();
        }

        public int IdIcono { get; set; }
        public string Clase { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Acceso> Acceso { get; set; }
        public virtual ICollection<EntidadEstructura> EntidadEstructura { get; set; }
        public virtual ICollection<SeccionCampo> SeccionCampo { get; set; }
    }
}
