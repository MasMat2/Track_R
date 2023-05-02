using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteSeccion
    {
        public ExpedienteSeccion()
        {
            ExpedienteCampo = new HashSet<ExpedienteCampo>();
            InverseIdExpedienteSeccionPadreNavigation = new HashSet<ExpedienteSeccion>();
        }

        public int IdExpedienteSeccion { get; set; }
        public string Nombre { get; set; } = null!;
        public int Orden { get; set; }
        public int? IdExpedienteSeccionPadre { get; set; }
        public int IdTipoExpediente { get; set; }
        public string? Clave { get; set; }

        public virtual ExpedienteSeccion? IdExpedienteSeccionPadreNavigation { get; set; }
        public virtual TipoExpediente IdTipoExpedienteNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteCampo> ExpedienteCampo { get; set; }
        public virtual ICollection<ExpedienteSeccion> InverseIdExpedienteSeccionPadreNavigation { get; set; }
    }
}
