using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FacturaConcepto = new HashSet<FacturaConcepto>();
            NotaVenta = new HashSet<NotaVenta>();
            RemisionNavigation = new HashSet<Remision>();
        }

        public int IdFactura { get; set; }
        public string Folio { get; set; } = null!;
        public string? Uuid { get; set; }
        public string? Descripcion { get; set; }
        public string LugarExpedicion { get; set; } = null!;
        public DateTime FechaFactura { get; set; }
        public DateTime? FechaPagoFactura { get; set; }
        public DateTime? FechaSellado { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string? SelloCancelacion { get; set; }
        public string? SelloSat { get; set; }
        public string? SelloCfd { get; set; }
        public string? SelloStr { get; set; }
        public string NombreReceptor { get; set; } = null!;
        public string RfcReceptor { get; set; } = null!;
        public string CalleReceptor { get; set; } = null!;
        public string NumeroExteriorReceptor { get; set; } = null!;
        public string ColoniaReceptor { get; set; } = null!;
        public string CodigoPostalReceptor { get; set; } = null!;
        public string RfcEmisor { get; set; } = null!;
        public string NombreEmisor { get; set; } = null!;
        public string CalleEmisor { get; set; } = null!;
        public string NumeroExteriorEmisor { get; set; } = null!;
        public string ColoniaEmisor { get; set; } = null!;
        public string CodigoPostalEmisor { get; set; } = null!;
        public int IdEstatusFactura { get; set; }
        public string CiudadReceptor { get; set; } = null!;
        public string CiudadEmisor { get; set; } = null!;
        public int IdSatMetodoPago { get; set; }
        public int IdSatFormaPago { get; set; }
        public int IdRegimenFiscal { get; set; }
        public int IdEstadoEmisor { get; set; }
        public string? NumeroInteriorReceptor { get; set; }
        public int IdEstadoReceptor { get; set; }
        public string UsoCfdi { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public bool Refacturada { get; set; }
        public DateTime FechaContable { get; set; }
        public string? DescripcionError { get; set; }
        public int? IdUsuarioCliente { get; set; }
        public int? IdRegimenFiscalReceptor { get; set; }
        public int? IdLocacion { get; set; }
        public int? IdExpedienteAdministrativo { get; set; }
        public string? Remision { get; set; }
        public bool? ConComplementoCartaPorte { get; set; }
        public string? XmlTimbrado { get; set; }
        public string? XmlCancelacion { get; set; }
        public bool? Pagada { get; set; }
        public int? IdSatTipoComprobante { get; set; }

        public virtual Estado IdEstadoEmisorNavigation { get; set; } = null!;
        public virtual Estado IdEstadoReceptorNavigation { get; set; } = null!;
        public virtual EstatusFactura IdEstatusFacturaNavigation { get; set; } = null!;
        public virtual ExpedienteAdministrativo? IdExpedienteAdministrativoNavigation { get; set; }
        public virtual Hospital? IdLocacionNavigation { get; set; }
        public virtual RegimenFiscal IdRegimenFiscalNavigation { get; set; } = null!;
        public virtual RegimenFiscal? IdRegimenFiscalReceptorNavigation { get; set; }
        public virtual SatFormaPago IdSatFormaPagoNavigation { get; set; } = null!;
        public virtual SatMetodoPago IdSatMetodoPagoNavigation { get; set; } = null!;
        public virtual SatTipoComprobante? IdSatTipoComprobanteNavigation { get; set; }
        public virtual Usuario? IdUsuarioClienteNavigation { get; set; }
        public virtual ICollection<FacturaConcepto> FacturaConcepto { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<Remision> RemisionNavigation { get; set; }
    }
}
