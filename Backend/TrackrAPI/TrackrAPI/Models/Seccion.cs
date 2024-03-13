using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Seccion
    {
        public Seccion()
        {
            EntidadEstructura = new HashSet<EntidadEstructura>();
            SeccionCampo = new HashSet<SeccionCampo>();
        }

        public int IdSeccion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Clave { get; set; }
        public int? Orden { get; set; }
        public bool? EsTabla { get; set; }

        public virtual ICollection<EntidadEstructura> EntidadEstructura { get; set; }
        public virtual ICollection<SeccionCampo> SeccionCampo { get; set; }
    }
}
