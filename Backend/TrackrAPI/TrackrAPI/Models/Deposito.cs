using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Deposito
    {
        public int IdDeposito { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int? NumeroReferencia { get; set; }
        public byte[] Archivo { get; set; } = null!;
        public string? ArchivoTipoMime { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdPago { get; set; }
        public decimal? Monto { get; set; }
        public bool Confirmado { get; set; }

        public virtual FormaPago? IdFormaPagoNavigation { get; set; }
        public virtual Pago? IdPagoNavigation { get; set; }
    }
}
