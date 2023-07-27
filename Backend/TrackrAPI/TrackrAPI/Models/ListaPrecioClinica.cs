using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ListaPrecioClinica
    {
        public int IdListaPrecioClinica { get; set; }
        public int IdListaPrecio { get; set; }
        public int IdClinica { get; set; }

        public virtual Hospital IdClinicaNavigation { get; set; } = null!;
        public virtual ListaPrecio IdListaPrecioNavigation { get; set; } = null!;
    }
}
