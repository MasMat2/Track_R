using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAntecedenteNoPatologico
    {
        public int IdExpedienteAntecedenteNoPatologico { get; set; }
        public int IdExpediente { get; set; }
        public int IdEnfermedad { get; set; }
        public string? Tipo { get; set; }
        public string CantidadDia { get; set; } = null!;
        public string Frecuencia { get; set; } = null!;
        public string? Inactividad { get; set; }
        public string? Observaciones { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
    }
}
