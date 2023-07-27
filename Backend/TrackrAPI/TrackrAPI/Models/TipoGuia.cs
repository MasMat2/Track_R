using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoGuia
    {
        public TipoGuia()
        {
            Guia = new HashSet<Guia>();
        }

        public int IdTipoGuia { get; set; }
        public string? Nombre { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Guia> Guia { get; set; }
    }
}
