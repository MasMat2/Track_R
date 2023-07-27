using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GiroComercial
    {
        public GiroComercial()
        {
            Compania = new HashSet<Compania>();
            Mercado = new HashSet<Mercado>();
        }

        public int IdGiroComercial { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Compania> Compania { get; set; }
        public virtual ICollection<Mercado> Mercado { get; set; }
    }
}
