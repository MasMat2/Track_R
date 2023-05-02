using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class UbicacionVenta
    {
        public UbicacionVenta()
        {
            NotaVenta = new HashSet<NotaVenta>();
            PuntoVenta = new HashSet<PuntoVenta>();
            Recibo = new HashSet<Recibo>();
        }

        public int IdUbicacionVenta { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdPuntoVenta { get; set; }

        public virtual PuntoVenta IdPuntoVentaNavigation { get; set; } = null!;
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<PuntoVenta> PuntoVenta { get; set; }
        public virtual ICollection<Recibo> Recibo { get; set; }
    }
}
