using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TraspasoMovimientoMaterial
    {
        public TraspasoMovimientoMaterial()
        {
            TraspasoMovimientoMaterialDetalle = new HashSet<TraspasoMovimientoMaterialDetalle>();
        }

        public int IdTraspasoMovimientoMaterial { get; set; }
        public string Folio { get; set; } = null!;
        public int? IdMovimientoMaterialEntrada { get; set; }
        public int? IdMovimientoMaterialSalida { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public int IdUsuarioAlmacenista { get; set; }
        public DateTime? FechaTraspaso { get; set; }
        public string? Observaciones { get; set; }

        public virtual Almacen IdAlmacenDestinoNavigation { get; set; } = null!;
        public virtual Almacen IdAlmacenOrigenNavigation { get; set; } = null!;
        public virtual MovimientoMaterial? IdMovimientoMaterialEntradaNavigation { get; set; }
        public virtual MovimientoMaterial? IdMovimientoMaterialSalidaNavigation { get; set; }
        public virtual Usuario IdUsuarioAlmacenistaNavigation { get; set; } = null!;
        public virtual ICollection<TraspasoMovimientoMaterialDetalle> TraspasoMovimientoMaterialDetalle { get; set; }
    }
}
