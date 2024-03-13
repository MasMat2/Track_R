using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class UrgenciaTratamiento
    {
        public int IdUrgenciaTratamiento { get; set; }
        public int IdUrgencia { get; set; }
        public int IdPresentacionTratamiento { get; set; }
        public string Numero { get; set; } = null!;
        public int? IdReciboDetalle { get; set; }

        public virtual Presentacion IdPresentacionTratamientoNavigation { get; set; } = null!;
        public virtual ReciboDetalle? IdReciboDetalleNavigation { get; set; }
        public virtual Urgencia IdUrgenciaNavigation { get; set; } = null!;
    }
}
