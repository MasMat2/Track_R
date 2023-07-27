using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class JerarquiaAccesoTipoCompania
    {
        public int IdJerarquiaAccesoTipoCompania { get; set; }
        public int IdJerarquiaAcceso { get; set; }
        public int IdTipoCompania { get; set; }

        public virtual JerarquiaAcceso IdJerarquiaAccesoNavigation { get; set; } = null!;
        public virtual TipoCompania IdTipoCompaniaNavigation { get; set; } = null!;
    }
}
