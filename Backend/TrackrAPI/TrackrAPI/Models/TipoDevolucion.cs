using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoDevolucion
    {
        public TipoDevolucion()
        {
            Devolucion = new HashSet<Devolucion>();
        }

        public int IdTipoDevolucion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<Devolucion> Devolucion { get; set; }
    }
}
