using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class JerarquiaEstructura
    {
        public JerarquiaEstructura()
        {
            InverseIdJerarquiaEstructuraPadreNavigation = new HashSet<JerarquiaEstructura>();
            JerarquiaConfiguracion = new HashSet<JerarquiaConfiguracion>();
        }

        public int IdJerarquiaEstructura { get; set; }
        public string? Numero { get; set; }
        public string? Descripcion { get; set; }
        public int Nivel { get; set; }
        public string? Ruta { get; set; }
        public int IdJerarquia { get; set; }
        public int? IdCuentaContable { get; set; }
        public int? IdAuxiliar { get; set; }
        public int? IdJerarquiaEstructuraPadre { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContableNavigation { get; set; }
        public virtual JerarquiaEstructura? IdJerarquiaEstructuraPadreNavigation { get; set; }
        public virtual Jerarquia IdJerarquiaNavigation { get; set; } = null!;
        public virtual ICollection<JerarquiaEstructura> InverseIdJerarquiaEstructuraPadreNavigation { get; set; }
        public virtual ICollection<JerarquiaConfiguracion> JerarquiaConfiguracion { get; set; }
    }
}
