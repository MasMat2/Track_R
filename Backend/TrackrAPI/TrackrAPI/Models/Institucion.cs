using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Institucion
    {
        public Institucion()
        {
            OrdenImagenologia = new HashSet<OrdenImagenologia>();
            OrdenLaboratorio = new HashSet<OrdenLaboratorio>();
        }

        public int IdInstitucion { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<OrdenImagenologia> OrdenImagenologia { get; set; }
        public virtual ICollection<OrdenLaboratorio> OrdenLaboratorio { get; set; }
    }
}
