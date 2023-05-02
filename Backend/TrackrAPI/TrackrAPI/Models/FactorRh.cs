using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FactorRh
    {
        public FactorRh()
        {
            ExpedientePacienteInformacion = new HashSet<ExpedientePacienteInformacion>();
        }

        public int IdFactorRh { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedientePacienteInformacion> ExpedientePacienteInformacion { get; set; }
    }
}
