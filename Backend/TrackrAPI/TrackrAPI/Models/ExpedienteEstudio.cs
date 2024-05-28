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
        public byte[]? Archivo { get; set; }
        public string ArchivoTipoMime { get; set; } = null!;
        public string ArchivoNombre { get; set; } = null!;
        public string? ArchivoUrl { get; set; }

        public virtual ExpedienteTrackr IdExpedienteNavigation { get; set; } = null!;
    }
}
