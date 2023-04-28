using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Municipio
    {
        public Municipio()
        {
            CodigoPostal = new HashSet<CodigoPostal>();
            Locacion = new HashSet<Locacion>();
        }

        public int IdMunicipio { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdEstado { get; set; }
        public string? Clave { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual ICollection<CodigoPostal> CodigoPostal { get; set; }
        public virtual ICollection<Locacion> Locacion { get; set; }
    }
}
