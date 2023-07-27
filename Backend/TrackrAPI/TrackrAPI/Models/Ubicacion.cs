using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            InventarioFisicoDetalle = new HashSet<InventarioFisicoDetalle>();
            Kardex = new HashSet<Kardex>();
            MovimientoMaterialDetalle = new HashSet<MovimientoMaterialDetalle>();
            NotaVentaDetalle = new HashSet<NotaVentaDetalle>();
            TraspasoMovimientoMaterialDetalleIdUbicacionDestinoNavigation = new HashSet<TraspasoMovimientoMaterialDetalle>();
            TraspasoMovimientoMaterialDetalleIdUbicacionOrigenNavigation = new HashSet<TraspasoMovimientoMaterialDetalle>();
        }

        public int IdUbicacion { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdAlmacen { get; set; }

        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
        public virtual ICollection<InventarioFisicoDetalle> InventarioFisicoDetalle { get; set; }
        public virtual ICollection<Kardex> Kardex { get; set; }
        public virtual ICollection<MovimientoMaterialDetalle> MovimientoMaterialDetalle { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalle { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterialDetalle> TraspasoMovimientoMaterialDetalleIdUbicacionDestinoNavigation { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterialDetalle> TraspasoMovimientoMaterialDetalleIdUbicacionOrigenNavigation { get; set; }
    }
}
