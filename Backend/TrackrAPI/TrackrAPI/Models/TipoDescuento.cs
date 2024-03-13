using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoDescuento
    {
        public TipoDescuento()
        {
            ListaPrecioDetalle = new HashSet<ListaPrecioDetalle>();
            NotaVentaDetalle = new HashSet<NotaVentaDetalle>();
            ReciboDetalle = new HashSet<ReciboDetalle>();
            TipoDescuentoDetalle = new HashSet<TipoDescuentoDetalle>();
        }

        public int IdTipoDescuento { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Estatus { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalle { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalle { get; set; }
        public virtual ICollection<ReciboDetalle> ReciboDetalle { get; set; }
        public virtual ICollection<TipoDescuentoDetalle> TipoDescuentoDetalle { get; set; }
    }
}
