using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GrupoPersonaActividad
    {
        public GrupoPersonaActividad()
        {
            GrupoActividadCita = new HashSet<GrupoActividadCita>();
        }

        public int IdGrupoPersonaActividad { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdGrupoPersona { get; set; }

        public virtual GrupoPersona IdGrupoPersonaNavigation { get; set; } = null!;
        public virtual ICollection<GrupoActividadCita> GrupoActividadCita { get; set; }
    }
}
