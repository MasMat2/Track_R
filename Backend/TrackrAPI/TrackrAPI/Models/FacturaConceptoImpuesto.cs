using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FacturaConceptoImpuesto
    {
        public int IdFacturaConceptoImpuesto { get; set; }
        public string ClaveImpuesto { get; set; } = null!;
        public string DescripcionImpuesto { get; set; } = null!;
        public bool EsRetencion { get; set; }
        public int IdFacturaConcepto { get; set; }
        public int IdSatTipoFactor { get; set; }
        public bool EsImpuestoLocal { get; set; }
        public decimal Base { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Importe { get; set; }

        public virtual FacturaConcepto IdFacturaConceptoNavigation { get; set; } = null!;
        public virtual SatTipoFactor IdSatTipoFactorNavigation { get; set; } = null!;
    }
}
