using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ReciboEnglobado
    {
        public int IdReciboEnglobado { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public bool? EsPorcentajeDescuentoGlobal { get; set; }
        public decimal? ValorCapturadoDescuentoGlobal { get; set; }
        public decimal? DescuentoGlobal { get; set; }
        public string? MotivoDescuento { get; set; }
        public string? Folio { get; set; }

        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
    }
}
