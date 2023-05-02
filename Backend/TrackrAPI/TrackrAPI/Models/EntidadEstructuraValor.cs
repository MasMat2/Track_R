using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EntidadEstructuraValor
    {
        public int IdEntidadEstructuraValor { get; set; }
        public int IdEntidadEstructura { get; set; }
        public string ClaveCampo { get; set; } = null!;
        public string? Valor { get; set; }
        public int? IdTabla { get; set; }

        public virtual EntidadEstructura IdEntidadEstructuraNavigation { get; set; } = null!;
    }
}
