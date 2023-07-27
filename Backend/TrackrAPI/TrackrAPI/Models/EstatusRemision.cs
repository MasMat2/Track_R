using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusRemision
    {
        public EstatusRemision()
        {
            Remision = new HashSet<Remision>();
        }

        public int IdEstatusRemision { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<Remision> Remision { get; set; }
    }
}
