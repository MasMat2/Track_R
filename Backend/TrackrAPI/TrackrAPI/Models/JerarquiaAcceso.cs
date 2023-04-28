using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class JerarquiaAcceso
    {
        public JerarquiaAcceso()
        {
            JerarquiaAccesoEstructura = new HashSet<JerarquiaAccesoEstructura>();
            JerarquiaAccesoTipoCompania = new HashSet<JerarquiaAccesoTipoCompania>();
        }

        public int IdJerarquiaAcceso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<JerarquiaAccesoEstructura> JerarquiaAccesoEstructura { get; set; }
        public virtual ICollection<JerarquiaAccesoTipoCompania> JerarquiaAccesoTipoCompania { get; set; }
    }
}
