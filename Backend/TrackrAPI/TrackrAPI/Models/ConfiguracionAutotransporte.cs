using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ConfiguracionAutotransporte
    {
        public ConfiguracionAutotransporte()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdConfiguracionAutotransporte { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string NumeroEjes { get; set; } = null!;
        public string NumeroLlantas { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
