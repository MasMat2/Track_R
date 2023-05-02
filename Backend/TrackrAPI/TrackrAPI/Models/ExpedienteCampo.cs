using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteCampo
    {
        public ExpedienteCampo()
        {
            ExpedienteCampoValor = new HashSet<ExpedienteCampoValor>();
        }

        public int IdExpedienteCampo { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdDominio { get; set; }
        public int IdExpedienteSeccion { get; set; }
        public bool Requerido { get; set; }
        public int? Orden { get; set; }
        public int TamanoColumna { get; set; }
        public bool? Deshabilitado { get; set; }

        public virtual Dominio IdDominioNavigation { get; set; } = null!;
        public virtual ExpedienteSeccion IdExpedienteSeccionNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteCampoValor> ExpedienteCampoValor { get; set; }
    }
}
