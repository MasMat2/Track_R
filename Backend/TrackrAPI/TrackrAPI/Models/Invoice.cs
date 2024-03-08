using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public int? Folio { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoicePaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReName { get; set; } = null!;
        public string ReRfc { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string PaymentMode { get; set; } = null!;
        public int ClientId { get; set; }
        public int PaymentMethodId { get; set; }
        public int PaymentModeId { get; set; }
        public int CaseFileId { get; set; }
        public int TradeCompanyId { get; set; }
        public string ReStreet { get; set; } = null!;
        public string ReOutdoorNumber { get; set; } = null!;
        public string ReNeighborhood { get; set; } = null!;
        public string ReZipCode { get; set; } = null!;
        public string ReCity { get; set; } = null!;
        public string ReState { get; set; } = null!;
        public string ReCountry { get; set; } = null!;
        public string ExpeditionPlace { get; set; } = null!;
        public string ExRfc { get; set; } = null!;
        public string ExName { get; set; } = null!;
        public string ExStreet { get; set; } = null!;
        public string ExOutdoorNumber { get; set; } = null!;
        public string ExNeighborhood { get; set; } = null!;
        public string ExLocation { get; set; } = null!;
        public string ExCity { get; set; } = null!;
        public string ExState { get; set; } = null!;
        public string ExCountry { get; set; } = null!;
        public string ExZipCode { get; set; } = null!;
        public string ExFiscalRegime { get; set; } = null!;
        public string VoucherType { get; set; } = null!;
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public string? ReferenceDescription { get; set; }
        public string? VehicleType { get; set; }
        public string DestinationPlace { get; set; } = null!;
        public string? DestinationAddress { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal HandlingAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TransferredTax { get; set; }
        public string Tax { get; set; } = null!;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxWithheld { get; set; }
        public string? ReInteriorNumber { get; set; }
        public string? PaymentAccountNumber { get; set; }
        public string? Uuid { get; set; }
        public string? FileName { get; set; }
        public DateTime? InvoiceStampedDate { get; set; }
        public string? SatSeal { get; set; }
        public string? CfdSeal { get; set; }
        public string? StrSeal { get; set; }
        public string? AmountText { get; set; }
        public int VoucherTypeId { get; set; }
        public int CreditDays { get; set; }
        public string PaymentModeCode { get; set; } = null!;
        public string ReOrigin { get; set; } = null!;
        public string? PaymentMethodCode { get; set; }
        public string? CaseFileDescription { get; set; }
        public DateTime? CaseFileDate { get; set; }
        public string? VehicleName { get; set; }
        public string? DriverName { get; set; }
        public string? BankAccount { get; set; }
        public string? FiscalRegimeCode { get; set; }
        public bool? Canceled { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string? Type { get; set; }
        public string? SatSealCanceled { get; set; }
        public string? FileNameCanceled { get; set; }
        public int? PolicyId { get; set; }
        public string? PolicyNumber { get; set; }
        public decimal? Ieps { get; set; }
        public decimal? Iepswithheld { get; set; }
        public decimal? Isr { get; set; }
        public string? CertificateNumber { get; set; }
        public string? CertificateNumberSat { get; set; }
        public bool? ConComplementoCartaPorte { get; set; }
    }
}
