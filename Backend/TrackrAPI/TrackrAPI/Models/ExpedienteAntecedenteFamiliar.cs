using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAntecedenteFamiliar
    {
        public int IdExpedienteAntecedenteFamiliar { get; set; }
        public int IdExpediente { get; set; }
        public int IdEnfermedad { get; set; }
        public int IdParentesco { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
        public virtual Parentesco IdParentescoNavigation { get; set; } = null!;
    }
}
