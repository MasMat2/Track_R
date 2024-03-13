using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ReciboDetalle
    {
        public ReciboDetalle()
        {
            UrgenciaTratamiento = new HashSet<UrgenciaTratamiento>();
        }

        public int IdReciboDetalle { get; set; }
        public int IdRecibo { get; set; }
        public int IdPresentacion { get; set; }
        public int IdImpuesto { get; set; }
        public int IdTipoDescuento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Descuento { get; set; }

        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Recibo IdReciboNavigation { get; set; } = null!;
        public virtual TipoDescuento IdTipoDescuentoNavigation { get; set; } = null!;
        public virtual ICollection<UrgenciaTratamiento> UrgenciaTratamiento { get; set; }
    }
}
