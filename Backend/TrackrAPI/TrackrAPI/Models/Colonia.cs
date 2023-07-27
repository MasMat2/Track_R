using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Colonia
    {
        public Colonia()
        {
            Domicilio = new HashSet<Domicilio>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdColonia { get; set; }
        public string Clave { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
