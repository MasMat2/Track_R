using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Turno
    {
        public Turno()
        {
            CajaTurno = new HashSet<CajaTurno>();
        }

        public int IdTurno { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<CajaTurno> CajaTurno { get; set; }
    }
}
