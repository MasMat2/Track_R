using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class VistaBalanzaComprobacion
    {
        public int IdCuentaContable { get; set; }
        public string? CuentaContable { get; set; }
        public decimal Cargo { get; set; }
        public decimal Abono { get; set; }
        public int? IdCompania { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
    }
}
