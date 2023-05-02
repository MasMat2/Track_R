using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Lada
    {
        public Lada()
        {
            Compania = new HashSet<Compania>();
            CompaniaContacto = new HashSet<CompaniaContacto>();
            Hospital = new HashSet<Hospital>();
        }

        public int IdLada { get; set; }
        public string Clave { get; set; } = null!;
        public string Numero { get; set; } = null!;

        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<CompaniaContacto> CompaniaContacto { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
    }
}
