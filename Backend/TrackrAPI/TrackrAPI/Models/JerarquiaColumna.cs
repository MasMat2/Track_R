using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class JerarquiaColumna
    {
        public JerarquiaColumna()
        {
            InverseIdJerarquiaColumnaRelacionadaNavigation = new HashSet<JerarquiaColumna>();
            JerarquiaConfiguracion = new HashSet<JerarquiaConfiguracion>();
        }

        public int IdJerarquiaColumna { get; set; }
        public string Nombre { get; set; } = null!;
        public string Letra { get; set; } = null!;
        public bool AgregadoPorSistema { get; set; }
        public int IdJerarquia { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public string? Acumula { get; set; }
        public bool? EsPorcentaje { get; set; }
        public int? IdVersionPoliza { get; set; }
        public int? IdJerarquiaColumnaRelacionada { get; set; }

        public virtual JerarquiaColumna? IdJerarquiaColumnaRelacionadaNavigation { get; set; }
        public virtual VersionPoliza? IdVersionPolizaNavigation { get; set; }
        public virtual ICollection<JerarquiaColumna> InverseIdJerarquiaColumnaRelacionadaNavigation { get; set; }
        public virtual ICollection<JerarquiaConfiguracion> JerarquiaConfiguracion { get; set; }
    }
}
