using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusOrdenCompra
    {
        public EstatusOrdenCompra()
        {
            OrdenCompra = new HashSet<OrdenCompra>();
        }

        public int IdEstatusOrdenCompra { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
    }
}
