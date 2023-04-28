using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class JerarquiaAccesoEstructura
    {
        public JerarquiaAccesoEstructura()
        {
            InverseIdJerarquiaAccesoEstructuraPadreNavigation = new HashSet<JerarquiaAccesoEstructura>();
        }

        public int IdJerarquiaAccesoEstructura { get; set; }
        public int? IdJerarquiaAcceso { get; set; }
        public int? IdAcceso { get; set; }
        public int? IdJerarquiaAccesoEstructuraPadre { get; set; }

        public virtual Acceso? IdAccesoNavigation { get; set; }
        public virtual JerarquiaAccesoEstructura? IdJerarquiaAccesoEstructuraPadreNavigation { get; set; }
        public virtual JerarquiaAcceso? IdJerarquiaAccesoNavigation { get; set; }
        public virtual ICollection<JerarquiaAccesoEstructura> InverseIdJerarquiaAccesoEstructuraPadreNavigation { get; set; }
    }
}
