using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            ContenidoExamen = new HashSet<ContenidoExamen>();
            Reactivo = new HashSet<Reactivo>();
        }

        public int IdAsignatura { get; set; }
        public string? Clave { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<ContenidoExamen> ContenidoExamen { get; set; }
        public virtual ICollection<Reactivo> Reactivo { get; set; }
    }
}
