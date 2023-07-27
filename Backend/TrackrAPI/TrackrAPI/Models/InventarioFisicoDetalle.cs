using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class InventarioFisicoDetalle
    {
        public InventarioFisicoDetalle()
        {
            InventarioFisicoAjusteDetalle = new HashSet<InventarioFisicoAjusteDetalle>();
        }

        public int IdInventarioFisicoDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public int? IdUbicacion { get; set; }
        public int IdArticulo { get; set; }
        public int IdInventarioFisico { get; set; }
        public string? Lote { get; set; }
        public decimal ValorTotalDiferencia { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual InventarioFisico IdInventarioFisicoNavigation { get; set; } = null!;
        public virtual Ubicacion? IdUbicacionNavigation { get; set; }
        public virtual ICollection<InventarioFisicoAjusteDetalle> InventarioFisicoAjusteDetalle { get; set; }
    }
}
