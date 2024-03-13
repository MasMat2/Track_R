using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoPresentacion
    {
        public TipoPresentacion()
        {
            Presentacion = new HashSet<Presentacion>();
        }

        public int IdTipoPresentacion { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Presentacion> Presentacion { get; set; }
    }
}
