using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoNotaGasto
    {
        public TipoNotaGasto()
        {
            NotaGasto = new HashSet<NotaGasto>();
        }

        public int IdTipoNotaGasto { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
    }
}
