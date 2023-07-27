using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusNotaGasto
    {
        public EstatusNotaGasto()
        {
            NotaGasto = new HashSet<NotaGasto>();
        }

        public int IdEstatusNotaGasto { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
    }
}
