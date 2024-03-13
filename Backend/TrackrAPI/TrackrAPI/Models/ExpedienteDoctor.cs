using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteDoctor
    {
        public int IdExpedienteDoctor { get; set; }
        public int IdExpediente { get; set; }
        public int IdUsuarioDoctor { get; set; }

        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioDoctorNavigation { get; set; } = null!;
    }
}
