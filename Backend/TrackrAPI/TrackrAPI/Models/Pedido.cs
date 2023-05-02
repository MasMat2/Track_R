using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            NotaVenta = new HashSet<NotaVenta>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
        }

        public int IdPedido { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdEstatusPedido { get; set; }
        public int? IdDireccion { get; set; }
        public int IdUsuarioComprador { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdTarjeta { get; set; }
        public string? OpenpayIdTransferencia { get; set; }
        public int? IdArea { get; set; }
        public int? IdCompania { get; set; }
        public string? PaypalIdTransferencia { get; set; }
        public int? IdDomicilio { get; set; }
        public int? IdPago { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Domicilio? IdDomicilioNavigation { get; set; }
        public virtual EstatusPedido? IdEstatusPedidoNavigation { get; set; }
        public virtual FormaPago? IdFormaPagoNavigation { get; set; }
        public virtual Pago? IdPagoNavigation { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
    }
}
