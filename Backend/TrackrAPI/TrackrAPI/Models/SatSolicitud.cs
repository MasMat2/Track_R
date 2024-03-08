using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatSolicitud
    {
        public int IdSatSolicitud { get; set; }
        public string Folio { get; set; } = null!;
        public string RfcSolicitante { get; set; } = null!;
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public bool? Completado { get; set; }
        public int IdLocacion { get; set; }

        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
    }
}
