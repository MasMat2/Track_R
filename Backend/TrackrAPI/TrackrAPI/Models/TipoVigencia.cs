using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoVigencia
    {
        public TipoVigencia()
        {
            ConfiguracionVigencia = new HashSet<ConfiguracionVigencia>();
        }

        public int IdTipoVigencia { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ConfiguracionVigencia> ConfiguracionVigencia { get; set; }
    }
}
