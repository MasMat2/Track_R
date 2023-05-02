using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FlujoDetalleAplicadoResponsable
    {
        public int IdFlujoDetalleAplicadoResponsable { get; set; }
        public int IdFlujoDetalleAplicado { get; set; }
        public int IdUsuario { get; set; }

        public virtual FlujoDetalleAplicado IdFlujoDetalleAplicadoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
