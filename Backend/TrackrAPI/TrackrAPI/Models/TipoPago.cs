using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoPago
    {
        public TipoPago()
        {
            Pago = new HashSet<Pago>();
        }

        public int IdTipoPago { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdConcepto { get; set; }

        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
    }
}
