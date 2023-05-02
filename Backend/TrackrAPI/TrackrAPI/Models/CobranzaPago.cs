using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CobranzaPago
    {
        public int IdCobranza { get; set; }
        public decimal Monto { get; set; }
        public int IdRemision { get; set; }
        public int IdLiquidacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Liquidacion IdLiquidacionNavigation { get; set; } = null!;
        public virtual Remision IdRemisionNavigation { get; set; } = null!;
    }
}
