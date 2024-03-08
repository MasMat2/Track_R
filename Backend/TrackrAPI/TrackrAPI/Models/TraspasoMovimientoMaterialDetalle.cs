using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TraspasoMovimientoMaterialDetalle
    {
        public int IdTraspasoMovimientoMaterialDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public int IdArticulo { get; set; }
        public int IdTraspasoMovimientoMaterial { get; set; }
        public string? LoteOrigen { get; set; }
        public string? LoteDestino { get; set; }
        public int? IdUbicacionOrigen { get; set; }
        public int? IdUbicacionDestino { get; set; }
        public int? IdArticuloDestino { get; set; }
        public decimal? CantidadDestino { get; set; }

        public virtual Articulo? IdArticuloDestinoNavigation { get; set; }
        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual TraspasoMovimientoMaterial IdTraspasoMovimientoMaterialNavigation { get; set; } = null!;
        public virtual Ubicacion? IdUbicacionDestinoNavigation { get; set; }
        public virtual Ubicacion? IdUbicacionOrigenNavigation { get; set; }
    }
}
