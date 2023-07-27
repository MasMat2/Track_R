using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoConcepto
    {
        public TipoConcepto()
        {
            ConfiguracionConcepto = new HashSet<ConfiguracionConcepto>();
        }

        public int IdTipoConcepto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<ConfiguracionConcepto> ConfiguracionConcepto { get; set; }
    }
}
