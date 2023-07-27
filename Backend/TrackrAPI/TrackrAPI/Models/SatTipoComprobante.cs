using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatTipoComprobante
    {
        public SatTipoComprobante()
        {
            Factura = new HashSet<Factura>();
        }

        public int IdSatTipoComprobante { get; set; }
        public string Clave { get; set; } = null!;
        public string? Nombre { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
