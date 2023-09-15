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
        public int? IdIcono { get; set; }
        public int? IdTipoWidget { get; set; }

        public virtual Icono? IdIconoNavigation { get; set; }
        public virtual TipoWidget? IdTipoWidgetNavigation { get; set; }
        public virtual ICollection<EntidadEstructura> EntidadEstructura { get; set; }
    }
}
