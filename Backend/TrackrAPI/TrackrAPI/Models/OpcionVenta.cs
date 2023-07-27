using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class OpcionVenta
    {
        public OpcionVenta()
        {
            ConfiguracionOpcionVenta = new HashSet<ConfiguracionOpcionVenta>();
        }

        public int IdOpcionVenta { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ConfiguracionOpcionVenta> ConfiguracionOpcionVenta { get; set; }
    }
}
