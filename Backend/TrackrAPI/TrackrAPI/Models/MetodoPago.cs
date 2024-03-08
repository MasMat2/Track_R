using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class MetodoPago
    {
        public MetodoPago()
        {
            Domicilio = new HashSet<Domicilio>();
            Remision = new HashSet<Remision>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdMetodoPago { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Remision> Remision { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
