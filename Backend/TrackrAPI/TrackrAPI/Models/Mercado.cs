using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Mercado
    {
        public Mercado()
        {
            MercadoCompania = new HashSet<MercadoCompania>();
        }

        public int IdMercado { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdGiroComercial { get; set; }

        public virtual GiroComercial IdGiroComercialNavigation { get; set; } = null!;
        public virtual ICollection<MercadoCompania> MercadoCompania { get; set; }
    }
}
