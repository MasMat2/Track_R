using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAntecedenteTratamiento
    {
        public int IdExpedienteAntecedenteTratamiento { get; set; }
        public int IdExpediente { get; set; }
        public string Tratamiento { get; set; } = null!;

        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
    }
}
