using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Urgencia
    {
        public Urgencia()
        {
            UrgenciaTratamiento = new HashSet<UrgenciaTratamiento>();
        }

        public int IdUrgencia { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaEgreso { get; set; }
        public int IdPaciente { get; set; }
        public int IdRecibo { get; set; }
        public int IdHospital { get; set; }

        public virtual Hospital IdHospitalNavigation { get; set; } = null!;
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual Recibo IdReciboNavigation { get; set; } = null!;
        public virtual ICollection<UrgenciaTratamiento> UrgenciaTratamiento { get; set; }
    }
}
