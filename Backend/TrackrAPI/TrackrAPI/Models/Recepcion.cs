using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Recepcion
    {
        public Recepcion()
        {
            RecepcionPresentacion = new HashSet<RecepcionPresentacion>();
        }

        public int IdRecepcion { get; set; }
        public string Folio { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdLiquidacion { get; set; }
        public int IdEstadoProducto { get; set; }
        public bool Habilitado { get; set; }
        public DateTime FechaRecepcion { get; set; }

        public virtual EstadoProducto IdEstadoProductoNavigation { get; set; } = null!;
        public virtual Liquidacion IdLiquidacionNavigation { get; set; } = null!;
        public virtual ICollection<RecepcionPresentacion> RecepcionPresentacion { get; set; }
    }
}
