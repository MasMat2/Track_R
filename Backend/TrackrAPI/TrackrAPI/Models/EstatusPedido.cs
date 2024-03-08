using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusPedido
    {
        public EstatusPedido()
        {
            FlujoDetalle = new HashSet<FlujoDetalle>();
            Pedido = new HashSet<Pedido>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
        }

        public int IdEstatusPedido { get; set; }
        public string Nombre { get; set; } = null!;
        public string Clave { get; set; } = null!;

        public virtual ICollection<FlujoDetalle> FlujoDetalle { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
    }
}
