using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusFactura
    {
        public EstatusFactura()
        {
            ComplementoPago = new HashSet<ComplementoPago>();
            Factura = new HashSet<Factura>();
        }

        public int IdEstatusFactura { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ComplementoPago> ComplementoPago { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
