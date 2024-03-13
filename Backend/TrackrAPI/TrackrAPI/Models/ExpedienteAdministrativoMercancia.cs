using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAdministrativoMercancia
    {
        public int IdExpedienteAdministrativoMercancia { get; set; }
        public int IdExpedienteAdministrativo { get; set; }
        public int IdSatProductoServicioCartaPorte { get; set; }
        public int IdSatUnidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Valor { get; set; }
        public decimal Peso { get; set; }
        public int? IdDomicilioDestino { get; set; }
        public string? RfcDestinitario { get; set; }
        public int? IdUsuarioDestinatario { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public int? KilometrosRecorrer { get; set; }

        public virtual Domicilio? IdDomicilioDestinoNavigation { get; set; }
        public virtual ExpedienteAdministrativo IdExpedienteAdministrativoNavigation { get; set; } = null!;
        public virtual SatProductoServicioCartaPorte IdSatProductoServicioCartaPorteNavigation { get; set; } = null!;
        public virtual SatUnidad IdSatUnidadNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioDestinatarioNavigation { get; set; }
    }
}
