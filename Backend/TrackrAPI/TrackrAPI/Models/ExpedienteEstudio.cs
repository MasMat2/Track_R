using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteEstudio
    {
        public int IdExpedienteEstudio { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdExpediente { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public string? ArchivoUrl { get; set; }
        public int? IdArchivo { get; set; }

        public virtual Archivo? IdArchivoNavigation { get; set; }
        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
    }
}
