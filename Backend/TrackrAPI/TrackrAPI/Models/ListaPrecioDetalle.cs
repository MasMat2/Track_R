using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ListaPrecioDetalle
    {
        public int IdListaPrecioDetalle { get; set; }
        public string? Clave { get; set; }
        public DateTime FechaAlta { get; set; }
        public decimal PrecioBase { get; set; }
        public int IdImpuesto { get; set; }
        public int IdComision { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdListaPrecio { get; set; }
        public int IdPresentacion { get; set; }
        public int IdDescuento { get; set; }

        public virtual TipoComision IdComisionNavigation { get; set; } = null!;
        public virtual TipoDescuento IdDescuentoNavigation { get; set; } = null!;
        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
        public virtual ListaPrecio IdListaPrecioNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
    }
}
