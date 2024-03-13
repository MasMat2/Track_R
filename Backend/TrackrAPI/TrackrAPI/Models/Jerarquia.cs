using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Jerarquia
    {
        public Jerarquia()
        {
            JerarquiaEstructura = new HashSet<JerarquiaEstructura>();
        }

        public int IdJerarquia { get; set; }
        public string Nombre { get; set; } = null!;
        public bool InvertirSigno { get; set; }
        public bool Estandar { get; set; }
        public int IdCompania { get; set; }
        public int IdTipoAuxiliar { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual TipoAuxiliar IdTipoAuxiliarNavigation { get; set; } = null!;
        public virtual ICollection<JerarquiaEstructura> JerarquiaEstructura { get; set; }
    }
}
