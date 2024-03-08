using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ComplementoPagoDetalle
    {
        public int IdComplementoPagoDetalle { get; set; }
        public int? IdMoneda { get; set; }
        public int? NumeroParcialidad { get; set; }
        public decimal? SaldoAnterior { get; set; }
        public decimal? SaldoInsoluto { get; set; }
        public decimal? MontoPagado { get; set; }
        public int IdComplementoPago { get; set; }
        public int IdNotaFlujoDetalle { get; set; }

        public virtual ComplementoPago IdComplementoPagoNavigation { get; set; } = null!;
        public virtual Moneda? IdMonedaNavigation { get; set; }
        public virtual NotaFlujoDetalle IdNotaFlujoDetalleNavigation { get; set; } = null!;
    }
}
