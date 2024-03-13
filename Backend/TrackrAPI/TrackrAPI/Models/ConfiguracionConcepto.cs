using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ConfiguracionConcepto
    {
        public int IdConfiguracionConcepto { get; set; }
        public int IdConcepto { get; set; }
        public int IdTipoConcepto { get; set; }

        public virtual Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual TipoConcepto IdTipoConceptoNavigation { get; set; } = null!;
    }
}
