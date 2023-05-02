using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class JerarquiaConfiguracion
    {
        public JerarquiaConfiguracion()
        {
            InverseIdJerarquiaConfiguracionRelacionadoNavigation = new HashSet<JerarquiaConfiguracion>();
        }

        public int IdJerarquiaConfiguracion { get; set; }
        public string Clave { get; set; } = null!;
        public string? Formula { get; set; }
        public bool AgregadoPorSistema { get; set; }
        public int? IdJerarquiaConfiguracionRelacionado { get; set; }
        public int IdJerarquiaEstructura { get; set; }
        public int IdJerarquiaColumna { get; set; }

        public virtual JerarquiaColumna IdJerarquiaColumnaNavigation { get; set; } = null!;
        public virtual JerarquiaConfiguracion? IdJerarquiaConfiguracionRelacionadoNavigation { get; set; }
        public virtual JerarquiaEstructura IdJerarquiaEstructuraNavigation { get; set; } = null!;
        public virtual ICollection<JerarquiaConfiguracion> InverseIdJerarquiaConfiguracionRelacionadoNavigation { get; set; }
    }
}
