using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatMetodoPago
    {
        public SatMetodoPago()
        {
            Factura = new HashSet<Factura>();
        }

        public int IdSatMetodoPago { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
