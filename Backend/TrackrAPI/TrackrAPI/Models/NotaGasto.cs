using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaGasto
    {
        public NotaGasto()
        {
            NotaGastoDetalle = new HashSet<NotaGastoDetalle>();
            Pago = new HashSet<Pago>();
        }

        public int IdNotaGasto { get; set; }
        public int IdUsuario { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaMovimiento { get; set; }
        public DateTime FechaContable { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public int IdEstatusNotaGasto { get; set; }
        public int IdConcepto { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public decimal Saldo { get; set; }
        public int? IdTipoNotaGasto { get; set; }
        public int IdLocacion { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdMovimientoMaterial { get; set; }
        public int? IdSatMovimiento { get; set; }

        public virtual Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual EstatusNotaGasto IdEstatusNotaGastoNavigation { get; set; } = null!;
        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual Moneda? IdMonedaNavigation { get; set; }
        public virtual MovimientoMaterial? IdMovimientoMaterialNavigation { get; set; }
        public virtual SatMovimiento? IdSatMovimientoNavigation { get; set; }
        public virtual TipoNotaGasto? IdTipoNotaGastoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<NotaGastoDetalle> NotaGastoDetalle { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
    }
}
