using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FlujoDetalleResponsable
    {
        public int IdFlujoDetalleResponsable { get; set; }
        public int IdFlujoDetalle { get; set; }
        public int IdUsuario { get; set; }

        public virtual FlujoDetalle IdFlujoDetalleNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
