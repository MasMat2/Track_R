using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusEstudioLaboratorio
    {
        public EstatusEstudioLaboratorio()
        {
            EstudioLaboratorio = new HashSet<EstudioLaboratorio>();
        }

        public int IdEstatusEstudioLaboratorio { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<EstudioLaboratorio> EstudioLaboratorio { get; set; }
    }
}
