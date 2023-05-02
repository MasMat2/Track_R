using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatMovimiento
    {
        public SatMovimiento()
        {
            NotaGasto = new HashSet<NotaGasto>();
            SatMovimientoConcepto = new HashSet<SatMovimientoConcepto>();
        }

        public int IdSatMovimiento { get; set; }
        public string Uuid { get; set; } = null!;
        public string RfcEmisor { get; set; } = null!;
        public string RfcReceptor { get; set; } = null!;
        public DateTime FechaMovimiento { get; set; }
        public decimal Total { get; set; }
        public int IdLocacion { get; set; }
        public bool? Aplicado { get; set; }
        public string? Comprobante { get; set; }
        public string? Folio { get; set; }
        public string? TipoComprobante { get; set; }
        public string? NombreEmisor { get; set; }
        public string? LugarExpedicion { get; set; }
        public int? IdRegimenFiscalEmisor { get; set; }

        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual RegimenFiscal? IdRegimenFiscalEmisorNavigation { get; set; }
        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
        public virtual ICollection<SatMovimientoConcepto> SatMovimientoConcepto { get; set; }
    }
}
