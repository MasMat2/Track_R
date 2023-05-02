using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class RemisionPresentacion
    {
        public int IdRemisionPresentacion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int IdRemision { get; set; }
        public int IdPresentacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public int? IdImpuesto { get; set; }
        public decimal? PrecioBase { get; set; }

        public virtual Impuesto? IdImpuestoNavigation { get; set; }
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Remision IdRemisionNavigation { get; set; } = null!;
    }
}
