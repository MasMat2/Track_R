using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedientePadecimiento
    {
        public int IdExpedientePadecimiento { get; set; }
        public int IdExpediente { get; set; }
        public int IdPadecimiento { get; set; }
        public DateTime FechaDiagnostico { get; set; }
        public int IdUsuarioDoctor { get; set; }

        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
        public virtual EntidadEstructura IdPadecimientoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioDoctorNavigation { get; set; } = null!;
    }
}
