using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Area
    {
        public Area()
        {
            Departamento = new HashSet<Departamento>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdArea { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Departamento> Departamento { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
