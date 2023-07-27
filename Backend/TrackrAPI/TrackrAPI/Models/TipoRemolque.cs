using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoRemolque
    {
        public TipoRemolque()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdTipoRemolque { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
