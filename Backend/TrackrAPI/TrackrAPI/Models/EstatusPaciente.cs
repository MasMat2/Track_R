using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstatusPaciente
    {
        public EstatusPaciente()
        {
            Paciente = new HashSet<Paciente>();
        }

        public int IdEstatusPaciente { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}
