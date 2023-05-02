using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaFlujo
    {
        public NotaFlujo()
        {
            ComplementoPago = new HashSet<ComplementoPago>();
            NotaFlujoDetalle = new HashSet<NotaFlujoDetalle>();
            NotaFlujoPago = new HashSet<NotaFlujoPago>();
            ReciboPago = new HashSet<ReciboPago>();
        }

        public int IdNotaFlujo { get; set; }
        public string Numero { get; set; } = null!;
        public int? IdObjetoFlujo { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public int? IdConcepto { get; set; }
        public int IdLocacion { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public DateTime FechaContable { get; set; }
        public int? IdMovimientoEstadoCuenta { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdEstatusNotaFlujo { get; set; }
        public string? Origen { get; set; }
        public int? IdOrigen { get; set; }

        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual EstatusNotaFlujo? IdEstatusNotaFlujoNavigation { get; set; }
        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual Moneda? IdMonedaNavigation { get; set; }
        public virtual MovimientoEstadoCuenta? IdMovimientoEstadoCuentaNavigation { get; set; }
        public virtual Caja? IdObjetoFlujoNavigation { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPago { get; set; }
        public virtual ICollection<NotaFlujoDetalle> NotaFlujoDetalle { get; set; }
        public virtual ICollection<NotaFlujoPago> NotaFlujoPago { get; set; }
        public virtual ICollection<ReciboPago> ReciboPago { get; set; }
    }
}
