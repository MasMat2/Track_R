using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FormaPago
    {
        public FormaPago()
        {
            CajaTurnoDetalle = new HashSet<CajaTurnoDetalle>();
            Deposito = new HashSet<Deposito>();
            InverseIdFormaPagoPadreNavigation = new HashSet<FormaPago>();
            Pago = new HashSet<Pago>();
            Pedido = new HashSet<Pedido>();
        }

        public int IdFormaPago { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdFormaPagoPadre { get; set; }
        public int? IdSatFormaPago { get; set; }

        public virtual FormaPago? IdFormaPagoPadreNavigation { get; set; }
        public virtual SatFormaPago? IdSatFormaPagoNavigation { get; set; }
        public virtual ICollection<CajaTurnoDetalle> CajaTurnoDetalle { get; set; }
        public virtual ICollection<Deposito> Deposito { get; set; }
        public virtual ICollection<FormaPago> InverseIdFormaPagoPadreNavigation { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
