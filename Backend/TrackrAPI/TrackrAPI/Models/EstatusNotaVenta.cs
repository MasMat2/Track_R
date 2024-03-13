using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusNotaVenta
    {
        public EstatusNotaVenta()
        {
            NotaVenta = new HashSet<NotaVenta>();
        }

        public int IdEstatusNotaVenta { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
    }
}
