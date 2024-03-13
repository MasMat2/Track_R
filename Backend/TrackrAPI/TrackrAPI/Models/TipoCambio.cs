using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoCambio
    {
        public int IdTipoCambio { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaInicio { get; set; }
        public int IdMoneda { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Moneda IdMonedaNavigation { get; set; } = null!;
    }
}
