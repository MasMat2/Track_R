using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoDescuentoDetalle
    {
        public int IdTipoDescuentoDetalle { get; set; }
        public string DiasSemana { get; set; } = null!;
        public bool TodoElDia { get; set; }
        public TimeSpan HorarioInicial { get; set; }
        public TimeSpan HorarioFinal { get; set; }
        public decimal Porcentaje { get; set; }
        public int IdTipoDescuento { get; set; }

        public virtual TipoDescuento IdTipoDescuentoNavigation { get; set; } = null!;
    }
}
