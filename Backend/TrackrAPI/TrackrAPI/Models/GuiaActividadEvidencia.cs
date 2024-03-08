using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GuiaActividadEvidencia
    {
        public int IdGuiaActividadEvidencia { get; set; }
        public int IdGuiaActividad { get; set; }
        public int IdArtefacto { get; set; }

        public virtual Artefacto IdArtefactoNavigation { get; set; } = null!;
        public virtual GuiaActividad IdGuiaActividadNavigation { get; set; } = null!;
    }
}
