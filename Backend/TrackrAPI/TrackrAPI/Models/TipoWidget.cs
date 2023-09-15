using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoWidget
    {
        public TipoWidget()
        {
            Entidad = new HashSet<Entidad>();
        }

        public int IdTipoWidget { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Entidad> Entidad { get; set; }
    }
}
