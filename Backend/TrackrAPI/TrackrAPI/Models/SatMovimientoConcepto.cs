using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatMovimientoConcepto
    {
        public int IdSatMovimientoConcepto { get; set; }
        public int IdSatMovimiento { get; set; }
        public string? Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal MontoUnitario { get; set; }
        public decimal? Importe { get; set; }

        public virtual SatMovimiento IdSatMovimientoNavigation { get; set; } = null!;
    }
}
