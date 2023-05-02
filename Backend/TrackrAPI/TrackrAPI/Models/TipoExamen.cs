using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoExamen
    {
        public TipoExamen()
        {
            ContenidoExamen = new HashSet<ContenidoExamen>();
            ProgramacionExamen = new HashSet<ProgramacionExamen>();
        }

        public int IdTipoExamen { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public int? TotalPreguntas { get; set; }
        public double? Duracion { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<ContenidoExamen> ContenidoExamen { get; set; }
        public virtual ICollection<ProgramacionExamen> ProgramacionExamen { get; set; }
    }
}
