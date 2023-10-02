using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoWidget
    {
        public TipoWidget()
        {
            EntidadEstructura = new HashSet<EntidadEstructura>();
        }

        public int IdTipoWidget { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<EntidadEstructura> EntidadEstructura { get; set; }
    }
}
