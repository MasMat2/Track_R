using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Kardex
    {
        public Kardex()
        {
            MovimientoMaterialDetalle = new HashSet<MovimientoMaterialDetalle>();
        }

        public int IdKardex { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Lote { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdAlmacen { get; set; }
        public int IdArticulo { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaCaducidad { get; set; }

        public virtual Almacen? IdAlmacenNavigation { get; set; }
        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Ubicacion? IdUbicacionNavigation { get; set; }
        public virtual ICollection<MovimientoMaterialDetalle> MovimientoMaterialDetalle { get; set; }
    }
}
