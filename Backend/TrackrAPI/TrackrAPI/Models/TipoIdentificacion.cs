using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoIdentificacion
    {
        public TipoIdentificacion()
        {
            ExpedientePacienteInformacion = new HashSet<ExpedientePacienteInformacion>();
        }

        public int IdTipoIdentificacion { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedientePacienteInformacion> ExpedientePacienteInformacion { get; set; }
    }
}
