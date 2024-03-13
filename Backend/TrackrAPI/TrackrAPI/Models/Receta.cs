using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Receta
    {
        public Receta()
        {
            RecetaDetalle = new HashSet<RecetaDetalle>();
        }

        public int IdReceta { get; set; }
        public string Numero { get; set; } = null!;
        public int IdCita { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Observaciones { get; set; } = null!;

        public virtual Cita IdCitaNavigation { get; set; } = null!;
        public virtual ICollection<RecetaDetalle> RecetaDetalle { get; set; }
    }
}
