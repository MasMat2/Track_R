using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoActividadParticipante
    {
        public int IdProyectoActividadParticipante { get; set; }
        public int IdProyectoActividad { get; set; }
        public int IdUsuarioParticipante { get; set; }
        public bool? Estatus { get; set; }

        public virtual ProyectoActividad IdProyectoActividadNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioParticipanteNavigation { get; set; } = null!;
    }
}
