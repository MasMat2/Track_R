using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusInventarioFisico
    {
        public EstatusInventarioFisico()
        {
            InventarioFisico = new HashSet<InventarioFisico>();
        }

        public int IdEstatusInventarioFisico { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<InventarioFisico> InventarioFisico { get; set; }
    }
}
