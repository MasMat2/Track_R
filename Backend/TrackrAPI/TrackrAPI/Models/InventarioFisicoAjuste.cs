using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class InventarioFisicoAjuste
    {
        public InventarioFisicoAjuste()
        {
            InventarioFisicoAjusteDetalle = new HashSet<InventarioFisicoAjusteDetalle>();
        }

        public int IdInventarioFisicoAjuste { get; set; }
        public string Observaciones { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdInventarioFisico { get; set; }

        public virtual InventarioFisico IdInventarioFisicoNavigation { get; set; } = null!;
        public virtual ICollection<InventarioFisicoAjusteDetalle> InventarioFisicoAjusteDetalle { get; set; }
    }
}
