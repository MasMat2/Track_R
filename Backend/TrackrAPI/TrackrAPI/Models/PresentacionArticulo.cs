using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PresentacionArticulo
    {
        public int IdPresentacionArticulo { get; set; }
        public int IdPresentacion { get; set; }
        public int IdArticulo { get; set; }
        public decimal Cantidad { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
    }
}
