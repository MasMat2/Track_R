using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ConfiguracionVigencia
    {
        public int IdConfiguracionVigencia { get; set; }
        public int IdTipoVigencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdCompania { get; set; }
        public int? PeriodoGracia { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual TipoVigencia IdTipoVigenciaNavigation { get; set; } = null!;
    }
}
