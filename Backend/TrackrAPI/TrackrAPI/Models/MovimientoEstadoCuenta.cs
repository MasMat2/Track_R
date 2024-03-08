using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class MovimientoEstadoCuenta
    {
        public MovimientoEstadoCuenta()
        {
            NotaFlujo = new HashSet<NotaFlujo>();
        }

        public int IdMovimientoEstadoCuenta { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaMovimiento { get; set; }
        public decimal? Cargo { get; set; }
        public decimal? Abono { get; set; }
        public string? NumeroCheque { get; set; }
        public int? IdChequera { get; set; }
        public bool Aplicado { get; set; }
        public string? Descripcion { get; set; }
        public string? CodigoTransaccion { get; set; }
        public int IdCompania { get; set; }

        public virtual Caja? IdChequeraNavigation { get; set; }
        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<NotaFlujo> NotaFlujo { get; set; }
    }
}
