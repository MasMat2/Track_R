using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Genero
    {
        public Genero()
        {
            ExpedienteTrackr = new HashSet<ExpedienteTrackr>();
        }

        public int IdGenero { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<ExpedienteTrackr> ExpedienteTrackr { get; set; }
    }
}
