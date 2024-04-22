using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Compania = new HashSet<Compania>();
            Domicilio = new HashSet<Domicilio>();
            Hospital = new HashSet<Hospital>();
            Localidad = new HashSet<Localidad>();
            Municipio = new HashSet<Municipio>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public string? Clave { get; set; }

        public virtual Pais IdPaisNavigation { get; set; } = null!;
        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Localidad> Localidad { get; set; }
        public virtual ICollection<Municipio> Municipio { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
