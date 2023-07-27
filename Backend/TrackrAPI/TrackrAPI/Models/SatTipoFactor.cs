using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatTipoFactor
    {
        public SatTipoFactor()
        {
            FacturaConceptoImpuesto = new HashSet<FacturaConceptoImpuesto>();
        }

        public int IdSatTipoFactor { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<FacturaConceptoImpuesto> FacturaConceptoImpuesto { get; set; }
    }
}
