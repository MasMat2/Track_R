using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoNotaVenta
    {
        public TipoNotaVenta()
        {
            NotaVenta = new HashSet<NotaVenta>();
        }

        public int IdTipoNotaVenta { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
    }
}
