using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoFlujo
    {
        public TipoFlujo()
        {
            Flujo = new HashSet<Flujo>();
        }

        public int IdTipoFlujo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Flujo> Flujo { get; set; }
    }
}
