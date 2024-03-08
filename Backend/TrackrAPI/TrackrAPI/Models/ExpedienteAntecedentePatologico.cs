using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAntecedentePatologico
    {
        public int IdExpedienteAntecedentePatologico { get; set; }
        public int IdExpediente { get; set; }
        public int IdEnfermedad { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
    }
}
