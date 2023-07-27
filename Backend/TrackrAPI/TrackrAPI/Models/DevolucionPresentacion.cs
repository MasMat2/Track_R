using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class DevolucionPresentacion
    {
        public int IdDevolucionPresentacion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int IdDevolucion { get; set; }
        public int IdPresentacion { get; set; }

        public virtual Devolucion IdDevolucionNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
    }
}
