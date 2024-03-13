using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ConfiguracionOpcionVenta
    {
        public int IdConfiguracionOpcionVenta { get; set; }
        public int IdPresentacion { get; set; }
        public int IdOpcionVenta { get; set; }
        public int IdFlujo { get; set; }

        public virtual Flujo IdFlujoNavigation { get; set; } = null!;
        public virtual OpcionVenta IdOpcionVentaNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
    }
}
