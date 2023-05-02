using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NecesidadPresentacion
    {
        public int IdNecesidadPresentacion { get; set; }
        public decimal Cantidad { get; set; }
        public int IdNecesidad { get; set; }
        public int IdPresentacion { get; set; }

        public virtual Necesidad IdNecesidadNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
    }
}
