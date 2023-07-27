using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatFormaPago
    {
        public SatFormaPago()
        {
            Factura = new HashSet<Factura>();
            FormaPago = new HashSet<FormaPago>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdSatFormaPago { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<FormaPago> FormaPago { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
