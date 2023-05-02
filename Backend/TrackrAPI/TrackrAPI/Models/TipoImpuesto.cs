using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoImpuesto
    {
        public TipoImpuesto()
        {
            ImpuestoDetalle = new HashSet<ImpuestoDetalle>();
        }

        public int IdTipoImpuesto { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EsLocal { get; set; }

        public virtual ICollection<ImpuestoDetalle> ImpuestoDetalle { get; set; }
    }
}
