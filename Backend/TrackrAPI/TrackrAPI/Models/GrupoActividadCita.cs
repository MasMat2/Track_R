using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GrupoActividadCita
    {
        public int IdGrupoActividadCita { get; set; }
        public int IdCita { get; set; }
        public int IdGrupoPersonaActividad { get; set; }
        public bool Valor { get; set; }
        public string? Observaciones { get; set; }

        public virtual Cita IdCitaNavigation { get; set; } = null!;
        public virtual GrupoPersonaActividad IdGrupoPersonaActividadNavigation { get; set; } = null!;
    }
}
