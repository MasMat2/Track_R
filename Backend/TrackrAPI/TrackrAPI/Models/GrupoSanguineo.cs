using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GrupoSanguineo
    {
        public GrupoSanguineo()
        {
            ExpedientePacienteInformacion = new HashSet<ExpedientePacienteInformacion>();
        }

        public int IdGrupoSanguineo { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedientePacienteInformacion> ExpedientePacienteInformacion { get; set; }
    }
}
