using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class VersionPoliza
    {
        public VersionPoliza()
        {
            JerarquiaColumna = new HashSet<JerarquiaColumna>();
            Poliza = new HashSet<Poliza>();
        }

        public int IdVersionPoliza { get; set; }
        public int Numero { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<JerarquiaColumna> JerarquiaColumna { get; set; }
        public virtual ICollection<Poliza> Poliza { get; set; }
    }
}
