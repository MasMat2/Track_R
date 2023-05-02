using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FacturaConcepto
    {
        public FacturaConcepto()
        {
            FacturaConceptoImpuesto = new HashSet<FacturaConceptoImpuesto>();
        }

        public int IdFacturaConcepto { get; set; }
        public int IdFactura { get; set; }
        public int IdConcepto { get; set; }
        public string? Descripcion { get; set; }
        public int IdImpuesto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal MontoUnitario { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? ImpuestosRetenidos { get; set; }
        public decimal? Impuestos { get; set; }
        public int? IdPresentacion { get; set; }

        public virtual Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual Factura IdFacturaNavigation { get; set; } = null!;
        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
        public virtual Presentacion? IdPresentacionNavigation { get; set; }
        public virtual ICollection<FacturaConceptoImpuesto> FacturaConceptoImpuesto { get; set; }
    }
}
