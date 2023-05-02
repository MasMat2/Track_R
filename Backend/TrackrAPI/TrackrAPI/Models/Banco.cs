using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Banco
    {
        public Banco()
        {
            Hospital = new HashSet<Hospital>();
        }

        public int IdBanco { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Hospital> Hospital { get; set; }
    }
}
