using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAdministrativoViaje
    {
        public int IdExpedienteAdministrativoViaje { get; set; }
        public int IdExpedienteAdministrativo { get; set; }
        public int IdVehiculo { get; set; }
        public int IdUsuarioChofer { get; set; }
        public int IdDomicilioEntrega { get; set; }
        public int AnioViaje { get; set; }
        public int MesViaje { get; set; }
        public int SemanaViaje { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public string? RfcDestinatario { get; set; }
        public string? NombreDestinatario { get; set; }
        public int? KilometrajeSalida { get; set; }
        public int? KilometrajeLlegada { get; set; }
        public int? IdUsuarioDestinatario { get; set; }
        public int? KilometrosRecorrer { get; set; }

        public virtual Domicilio IdDomicilioEntregaNavigation { get; set; } = null!;
        public virtual ExpedienteAdministrativo IdExpedienteAdministrativoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioChoferNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioDestinatarioNavigation { get; set; }
        public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
    }
}
