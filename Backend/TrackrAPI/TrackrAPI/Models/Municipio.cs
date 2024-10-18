using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Municipio
    {
        public Municipio()
        {
            CodigoPostal = new HashSet<CodigoPostal>();
            Compania = new HashSet<Compania>();
            Direccion = new HashSet<Direccion>();
            Domicilio = new HashSet<Domicilio>();
            Hospital = new HashSet<Hospital>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdMunicipio { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdEstado { get; set; }
        public string? Clave { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual ICollection<CodigoPostal> CodigoPostal { get; set; }
        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<Direccion> Direccion { get; set; }
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
