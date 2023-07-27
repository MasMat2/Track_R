using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusExamen
    {
        public EstatusExamen()
        {
            Examen = new HashSet<Examen>();
        }

        public int IdEstatusExamen { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Examen> Examen { get; set; }
    }
}
