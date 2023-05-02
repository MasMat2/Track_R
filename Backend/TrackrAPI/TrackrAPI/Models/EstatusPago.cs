using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusPago
    {
        public EstatusPago()
        {
            Cita = new HashSet<Cita>();
            Recibo = new HashSet<Recibo>();
        }

        public int IdEstatusPago { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<Recibo> Recibo { get; set; }
    }
}
