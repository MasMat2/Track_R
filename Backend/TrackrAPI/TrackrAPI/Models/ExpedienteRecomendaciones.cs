using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteRecomendaciones
    {
        public int IdExpedienteRecomendaciones { get; set; }
        public int IdExpediente { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string? Descripcion { get; set; }
        public int IdUsuarioDoctor { get; set; }
        public int? IdNotificacion { get; set; }

        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
        public virtual Notificacion? IdNotificacionNavigation { get; set; }
        public virtual Usuario IdUsuarioDoctorNavigation { get; set; } = null!;
    }
}
