using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstudioImagenologia
    {
        public int IdEstudioImagenologia { get; set; }
        public int IdEstatusEstudioImagenologia { get; set; }
        public string? Observaciones { get; set; }
        public int? IdOrdenImagenologia { get; set; }

        public virtual EstatusEstudioImagenologia IdEstatusEstudioImagenologiaNavigation { get; set; } = null!;
        public virtual OrdenImagenologia? IdOrdenImagenologiaNavigation { get; set; }
    }
}
