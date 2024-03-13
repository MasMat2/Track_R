using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Moneda
    {
        public Moneda()
        {
            Caja = new HashSet<Caja>();
            Compania = new HashSet<Compania>();
            ComplementoPagoDetalle = new HashSet<ComplementoPagoDetalle>();
            ComplementoPagoIdMonedaDrNavigation = new HashSet<ComplementoPago>();
            ComplementoPagoIdMonedaNavigation = new HashSet<ComplementoPago>();
            ListaPrecio = new HashSet<ListaPrecio>();
            NotaFlujo = new HashSet<NotaFlujo>();
            NotaGasto = new HashSet<NotaGasto>();
            Poliza = new HashSet<Poliza>();
            TipoCambio = new HashSet<TipoCambio>();
        }

        public int IdMoneda { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Simbolo { get; set; } = null!;

        public virtual ICollection<Caja> Caja { get; set; }
        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<ComplementoPagoDetalle> ComplementoPagoDetalle { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagoIdMonedaDrNavigation { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPagoIdMonedaNavigation { get; set; }
        public virtual ICollection<ListaPrecio> ListaPrecio { get; set; }
        public virtual ICollection<NotaFlujo> NotaFlujo { get; set; }
        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
        public virtual ICollection<Poliza> Poliza { get; set; }
        public virtual ICollection<TipoCambio> TipoCambio { get; set; }
    }
}
