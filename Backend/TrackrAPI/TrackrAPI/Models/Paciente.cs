using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            NotaVenta = new HashSet<NotaVenta>();
            Recibo = new HashSet<Recibo>();
            Urgencia = new HashSet<Urgencia>();
        }

        public int IdPaciente { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public int? IdExpediente { get; set; }
        public int? IdEstatusPaciente { get; set; }

        public virtual EstatusPaciente? IdEstatusPacienteNavigation { get; set; }
        public virtual Expediente? IdExpedienteNavigation { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<Recibo> Recibo { get; set; }
        public virtual ICollection<Urgencia> Urgencia { get; set; }
    }
}
