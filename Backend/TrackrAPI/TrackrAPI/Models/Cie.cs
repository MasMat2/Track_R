using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Cie
    {
        public Cie()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdCie { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
