using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoExpediente
    {
        public TipoExpediente()
        {
            ExpedienteSeccion = new HashSet<ExpedienteSeccion>();
        }

        public int IdTipoExpediente { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedienteSeccion> ExpedienteSeccion { get; set; }
    }
}
