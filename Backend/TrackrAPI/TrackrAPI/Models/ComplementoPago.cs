using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ComplementoPago
    {
        public ComplementoPago()
        {
            ComplementoPagoDetalle = new HashSet<ComplementoPagoDetalle>();
        }

        public int IdComplementoPago { get; set; }
        public string Version { get; set; } = null!;
        public string Folio { get; set; } = null!;
        public DateTime FechaComplemento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int IdMoneda { get; set; }
        public string LugarExpedicion { get; set; } = null!;
        public string RfcEmisor { get; set; } = null!;
        public string NombreEmisor { get; set; } = null!;
        public int IdRegimenFiscalEmisor { get; set; }
        public string RfcReceptor { get; set; } = null!;
        public string NombreReceptor { get; set; } = null!;
        public int Cantidad { get; set; }
        public int ValorUnitario { get; set; }
        public int Importe { get; set; }
        public string VersionPago { get; set; } = null!;
        public int IdMonedaDr { get; set; }
        public decimal MontoPago { get; set; }
        public string NumeroOperacion { get; set; } = null!;
        public int IdUsuarioCliente { get; set; }
        public int IdNotaFlujo { get; set; }
        public string? Uuid { get; set; }
        public string? NumeroCertificado { get; set; }
        public string? NumeroCertificadoSat { get; set; }
        public DateTime? FechaSellado { get; set; }
        public string? SelloSat { get; set; }
        public string? SelloCfd { get; set; }
        public string? SelloStr { get; set; }
        public string UsoCfdi { get; set; } = null!;
        public int? IdEstatusComplemento { get; set; }
        public string? DescripcionError { get; set; }
        public int IdLocacion { get; set; }
        public string? SelloCancelacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }

        public virtual EstatusFactura? IdEstatusComplementoNavigation { get; set; }
        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual Moneda IdMonedaDrNavigation { get; set; } = null!;
        public virtual Moneda IdMonedaNavigation { get; set; } = null!;
        public virtual NotaFlujo IdNotaFlujoNavigation { get; set; } = null!;
        public virtual RegimenFiscal IdRegimenFiscalEmisorNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioClienteNavigation { get; set; } = null!;
        public virtual ICollection<ComplementoPagoDetalle> ComplementoPagoDetalle { get; set; }
    }
}
