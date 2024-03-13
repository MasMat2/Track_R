using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class RegimenFiscal
    {
        public RegimenFiscal()
        {
            Compania = new HashSet<Compania>();
            ComplementoPago = new HashSet<ComplementoPago>();
            FacturaIdRegimenFiscalNavigation = new HashSet<Factura>();
            FacturaIdRegimenFiscalReceptorNavigation = new HashSet<Factura>();
            Hospital = new HashSet<Hospital>();
            SatMovimiento = new HashSet<SatMovimiento>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdRegimenFiscal { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPago { get; set; }
        public virtual ICollection<Factura> FacturaIdRegimenFiscalNavigation { get; set; }
        public virtual ICollection<Factura> FacturaIdRegimenFiscalReceptorNavigation { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<SatMovimiento> SatMovimiento { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
