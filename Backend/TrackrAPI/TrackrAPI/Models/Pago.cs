using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Pago
    {
        public Pago()
        {
            Deposito = new HashSet<Deposito>();
            NotaFlujoPago = new HashSet<NotaFlujoPago>();
            Pedido = new HashSet<Pedido>();
            ReciboPago = new HashSet<ReciboPago>();
        }

        public int IdPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public int IdFormaPago { get; set; }
        public int? IdCajaTurno { get; set; }
        public string Folio { get; set; } = null!;
        public DateTime FechaContable { get; set; }
        public int? IdUsuarioAlta { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdCaja { get; set; }
        public int? IdNotaGasto { get; set; }
        public string? Descripcion { get; set; }
        public string? NumeroMovimientoBancario { get; set; }
        public bool? Aprobado { get; set; }
        public bool? Declinado { get; set; }

        public virtual Caja? IdCajaNavigation { get; set; }
        public virtual CajaTurno? IdCajaTurnoNavigation { get; set; }
        public virtual FormaPago IdFormaPagoNavigation { get; set; } = null!;
        public virtual NotaGasto? IdNotaGastoNavigation { get; set; }
        public virtual TipoPago? IdTipoPagoNavigation { get; set; }
        public virtual Usuario? IdUsuarioAltaNavigation { get; set; }
        public virtual ICollection<Deposito> Deposito { get; set; }
        public virtual ICollection<NotaFlujoPago> NotaFlujoPago { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<ReciboPago> ReciboPago { get; set; }
    }
}
