using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoPuntoVenta
    {
        public TipoPuntoVenta()
        {
            PuntoVenta = new HashSet<PuntoVenta>();
        }

        public int IdTipoPuntoVenta { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PuntoVenta> PuntoVenta { get; set; }
    }
}
