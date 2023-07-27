using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PuntoVenta
    {
        public PuntoVenta()
        {
            NotaVenta = new HashSet<NotaVenta>();
            UbicacionVenta = new HashSet<UbicacionVenta>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdPuntoVenta { get; set; }
        public int IdAlmacen { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? IdUbicacionVenta { get; set; }
        public string? Clave { get; set; }
        public int? IdTipoPuntoVenta { get; set; }
        public int? IdConcepto { get; set; }

        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual TipoPuntoVenta? IdTipoPuntoVentaNavigation { get; set; }
        public virtual UbicacionVenta? IdUbicacionVentaNavigation { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<UbicacionVenta> UbicacionVenta { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
