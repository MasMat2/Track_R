using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaFlujoPago
    {
        public int IdNotaFlujoPago { get; set; }
        public int IdNotaFlujo { get; set; }
        public int IdPago { get; set; }

        public virtual NotaFlujo IdNotaFlujoNavigation { get; set; } = null!;
        public virtual Pago IdPagoNavigation { get; set; } = null!;
    }
}
