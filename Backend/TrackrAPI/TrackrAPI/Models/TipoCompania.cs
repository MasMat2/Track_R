using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoCompania
    {
        public TipoCompania()
        {
            Compania = new HashSet<Compania>();
            JerarquiaAccesoTipoCompania = new HashSet<JerarquiaAccesoTipoCompania>();
        }

        public int IdTipoCompania { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<JerarquiaAccesoTipoCompania> JerarquiaAccesoTipoCompania { get; set; }
    }
}
