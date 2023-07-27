using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoComision
    {
        public TipoComision()
        {
            ListaPrecioDetalle = new HashSet<ListaPrecioDetalle>();
            TipoComisionDetalle = new HashSet<TipoComisionDetalle>();
        }

        public int IdTipoComision { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public string Estatus { get; set; } = null!;
        public decimal? CifraControl { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalle { get; set; }
        public virtual ICollection<TipoComisionDetalle> TipoComisionDetalle { get; set; }
    }
}
