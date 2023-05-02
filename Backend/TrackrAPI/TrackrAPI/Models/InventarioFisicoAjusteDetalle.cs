using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class InventarioFisicoAjusteDetalle
    {
        public int IdInventarioFisicoAjusteDetalle { get; set; }
        public int IdInventarioFisicoAjuste { get; set; }
        public int IdInventarioFisicoDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public string RazonAjuste { get; set; } = null!;

        public virtual InventarioFisicoAjuste IdInventarioFisicoAjusteNavigation { get; set; } = null!;
        public virtual InventarioFisicoDetalle IdInventarioFisicoDetalleNavigation { get; set; } = null!;
    }
}
