using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusAlmacen
    {
        public EstatusAlmacen()
        {
            Almacen = new HashSet<Almacen>();
        }

        public int IdEstatusAlmacen { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Almacen> Almacen { get; set; }
    }
}
