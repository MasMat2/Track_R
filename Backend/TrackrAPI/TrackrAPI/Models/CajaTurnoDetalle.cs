using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CajaTurnoDetalle
    {
        public int IdCajaTurnoDetalle { get; set; }
        public int IdFormaPago { get; set; }
        public int IdCajaTurno { get; set; }
        public decimal Monto { get; set; }

        public virtual CajaTurno IdCajaTurnoNavigation { get; set; } = null!;
        public virtual FormaPago IdFormaPagoNavigation { get; set; } = null!;
    }
}
