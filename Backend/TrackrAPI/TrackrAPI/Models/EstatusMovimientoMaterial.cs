using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusMovimientoMaterial
    {
        public EstatusMovimientoMaterial()
        {
            MovimientoMaterial = new HashSet<MovimientoMaterial>();
        }

        public int IdEstatusMovimientoMaterial { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<MovimientoMaterial> MovimientoMaterial { get; set; }
    }
}
