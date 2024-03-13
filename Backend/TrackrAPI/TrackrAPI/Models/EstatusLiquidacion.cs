using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusLiquidacion
    {
        public EstatusLiquidacion()
        {
            Liquidacion = new HashSet<Liquidacion>();
        }

        public int IdEstatusLiquidacion { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<Liquidacion> Liquidacion { get; set; }
    }
}
