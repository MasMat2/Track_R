using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteRecomendacionesGenerales
    {
        public ExpedienteRecomendacionesGenerales()
        {
            DetalleExpedienteRecomendacionesGenerales = new HashSet<DetalleExpedienteRecomendacionesGenerales>();
        }

        public int IdExpedienteRecomendacionesGenerales { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string? Descripcion { get; set; }
        public int IdAdministrador { get; set; }

        public virtual Usuario IdAdministradorNavigation { get; set; } = null!;
        public virtual ICollection<DetalleExpedienteRecomendacionesGenerales> DetalleExpedienteRecomendacionesGenerales { get; set; }
    }
}
