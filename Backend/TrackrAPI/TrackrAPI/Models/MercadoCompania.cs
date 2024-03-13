using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class MercadoCompania
    {
        public int IdMercadoCompania { get; set; }
        public int IdMercado { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Mercado IdMercadoNavigation { get; set; } = null!;
    }
}
