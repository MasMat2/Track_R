using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoExpedienteAdministrativo
    {
        public TipoExpedienteAdministrativo()
        {
            ExpedienteAdministrativo = new HashSet<ExpedienteAdministrativo>();
        }

        public int IdTipoExpedienteAdministrativo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteAdministrativo> ExpedienteAdministrativo { get; set; }
    }
}
