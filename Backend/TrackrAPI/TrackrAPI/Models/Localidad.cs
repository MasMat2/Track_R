using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Localidad
    {
        public Localidad()
        {
            Domicilio = new HashSet<Domicilio>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdLocalidad { get; set; }
        public string Clave { get; set; } = null!;
        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
