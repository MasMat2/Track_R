using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoAcceso
    {
        public TipoAcceso()
        {
            Acceso = new HashSet<Acceso>();
        }

        public int IdTipoAcceso { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Acceso> Acceso { get; set; }
    }
}
