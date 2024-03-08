using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteBitacora
    {
        public int IdExpedienteBitacora { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int IdExpediente { get; set; }
        public string Seccion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual Expediente IdExpedienteNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
