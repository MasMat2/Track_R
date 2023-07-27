using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CitaGrupoPersona
    {
        public int IdCitaGrupoPersona { get; set; }
        public int IdCita { get; set; }
        public int IdGrupoPersona { get; set; }

        public virtual Cita IdCitaNavigation { get; set; } = null!;
        public virtual GrupoPersona IdGrupoPersonaNavigation { get; set; } = null!;
    }
}
