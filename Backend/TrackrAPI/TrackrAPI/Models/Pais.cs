using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Pais
    {
        public Pais()
        {
            Estado = new HashSet<Estado>();
        }

        public int IdPais { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Clave { get; set; }

        public virtual ICollection<Estado> Estado { get; set; }
    }
}
