using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusEstudioImagenologia
    {
        public EstatusEstudioImagenologia()
        {
            EstudioImagenologia = new HashSet<EstudioImagenologia>();
        }

        public int IdEstatusEstudioImagenologia { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<EstudioImagenologia> EstudioImagenologia { get; set; }
    }
}
