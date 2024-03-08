using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteCampoValor
    {
        public int IdExpedienteCampoValor { get; set; }
        public string? Valor { get; set; }
        public int IdExpedienteCampo { get; set; }
        public int? IdTabla { get; set; }
        public string? Tabla { get; set; }

        public virtual ExpedienteCampo IdExpedienteCampoNavigation { get; set; } = null!;
    }
}
