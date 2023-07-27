using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoRecibo
    {
        public TipoRecibo()
        {
            Recibo = new HashSet<Recibo>();
        }

        public int IdTipoRecibo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Recibo> Recibo { get; set; }
    }
}
