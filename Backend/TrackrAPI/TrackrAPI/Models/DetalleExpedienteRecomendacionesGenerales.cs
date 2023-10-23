using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class DetalleExpedienteRecomendacionesGenerales
    {
        public int IdDetalleExpedienteRecomendacionesGenerales { get; set; }
        public int IdExpediente { get; set; }
        public int? IdNotificacion { get; set; }
        public int IdExpedienteRecomendacionesGenerales { get; set; }

        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
        public virtual ExpedienteRecomendacionesGenerales IdExpedienteRecomendacionesGeneralesNavigation { get; set; } = null!;
        public virtual Notificacion? IdNotificacionNavigation { get; set; }
    }
}
