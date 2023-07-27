using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatProductoServicio
    {
        public SatProductoServicio()
        {
            Concepto = new HashSet<Concepto>();
        }

        public int IdSatProductoServicio { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Concepto> Concepto { get; set; }
    }
}
