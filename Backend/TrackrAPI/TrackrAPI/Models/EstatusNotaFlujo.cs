using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusNotaFlujo
    {
        public EstatusNotaFlujo()
        {
            NotaFlujo = new HashSet<NotaFlujo>();
        }

        public int IdEstatusNotaFlujo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<NotaFlujo> NotaFlujo { get; set; }
    }
}
