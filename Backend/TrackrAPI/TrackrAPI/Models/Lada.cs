using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Lada
    {
        public Lada()
        {
            Compania = new HashSet<Compania>();
            Locacion = new HashSet<Locacion>();
        }

        public int IdLada { get; set; }
        public string Clave { get; set; } = null!;
        public string Numero { get; set; } = null!;

        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<Locacion> Locacion { get; set; }
    }
}
