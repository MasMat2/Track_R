using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class OrdenCompraDetalle
    {
        public int IdOrdenCompraDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdArticulo { get; set; }
        public decimal CantidadRecibida { get; set; }
        public int IdImpuesto { get; set; }
        public decimal? Impuesto { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
        public virtual OrdenCompra IdOrdenCompraNavigation { get; set; } = null!;
    }
}
