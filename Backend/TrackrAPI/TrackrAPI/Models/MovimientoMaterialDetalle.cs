using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class MovimientoMaterialDetalle
    {
        public int IdMovimientoMaterialDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string? Lote { get; set; }
        public int IdArticulo { get; set; }
        public int IdMovimientoMaterial { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdKardex { get; set; }
        public int? IdConcepto { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual Kardex? IdKardexNavigation { get; set; }
        public virtual MovimientoMaterial IdMovimientoMaterialNavigation { get; set; } = null!;
        public virtual Ubicacion? IdUbicacionNavigation { get; set; }
    }
}
