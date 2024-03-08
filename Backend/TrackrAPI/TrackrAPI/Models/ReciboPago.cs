using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ReciboPago
    {
        public int IdReciboPago { get; set; }
        public int IdRecibo { get; set; }
        public int IdPago { get; set; }
        public int? IdReciboEnglobado { get; set; }
        public int? IdNotaFlujo { get; set; }

        public virtual NotaFlujo? IdNotaFlujoNavigation { get; set; }
        public virtual Pago IdPagoNavigation { get; set; } = null!;
        public virtual Recibo IdReciboNavigation { get; set; } = null!;
    }
}
