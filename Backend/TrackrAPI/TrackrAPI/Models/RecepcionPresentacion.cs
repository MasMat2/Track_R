using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class RecepcionPresentacion
    {
        public int IdRecepcionPresentacion { get; set; }
        public decimal Cantidad { get; set; }
        public int IdRecepcion { get; set; }
        public int IdPresentacion { get; set; }

        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Recepcion IdRecepcionNavigation { get; set; } = null!;
    }
}
