using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TratamientoToma
    {
        public int IdTomaTratamiento { get; set; }
        public int IdTratamientoRecordatorio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaToma { get; set; }
        public int? IdNotificacion { get; set; }

        public virtual Notificacion? IdNotificacionNavigation { get; set; }
        public virtual TratamientoRecordatorio IdTratamientoRecordatorioNavigation { get; set; } = null!;
    }
}
