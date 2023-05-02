using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Gasto
    {
        public Gasto()
        {
            GastoConcepto = new HashSet<GastoConcepto>();
        }

        public int IdGasto { get; set; }
        public string Folio { get; set; } = null!;
        public int IdLiquidacion { get; set; }
        public bool Habilitado { get; set; }
        public DateTime? FechaGasto { get; set; }
        public DateTime? FechaAlta { get; set; }

        public virtual Liquidacion IdLiquidacionNavigation { get; set; } = null!;
        public virtual ICollection<GastoConcepto> GastoConcepto { get; set; }
    }
}
