using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Cierre
    {
        public Cierre()
        {
            CajaTurno = new HashSet<CajaTurno>();
        }

        public int IdCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaContable { get; set; }
        public int IdHospital { get; set; }

        public virtual ICollection<CajaTurno> CajaTurno { get; set; }
    }
}
