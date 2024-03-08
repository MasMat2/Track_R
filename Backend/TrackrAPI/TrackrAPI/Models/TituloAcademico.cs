using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TituloAcademico
    {
        public TituloAcademico()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdTituloAcademico { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
