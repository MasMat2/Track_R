using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAuxiliar
    {
        public int IdExpedienteAuxiliar { get; set; }
        public int IdExpedienteAdministrativo { get; set; }
        public string NumeroAuxiliar { get; set; } = null!;
        public string CodigoTipoAuxiliar { get; set; } = null!;

        public virtual ExpedienteAdministrativo IdExpedienteAdministrativoNavigation { get; set; } = null!;
    }
}
