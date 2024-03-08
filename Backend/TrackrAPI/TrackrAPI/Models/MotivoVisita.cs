using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class MotivoVisita
    {
        public MotivoVisita()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdMotivoVisita { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
