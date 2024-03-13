using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoActividadEvidencia
    {
        public ProyectoActividadEvidencia()
        {
            ProyectoActividadEvidenciaArchivo = new HashSet<ProyectoActividadEvidenciaArchivo>();
        }

        public int IdProyectoActividadEvidencia { get; set; }
        public int IdProyectoActividad { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public bool? Estatus { get; set; }
        public bool? IntegraPlantilla { get; set; }

        public virtual ProyectoActividad IdProyectoActividadNavigation { get; set; } = null!;
        public virtual ICollection<ProyectoActividadEvidenciaArchivo> ProyectoActividadEvidenciaArchivo { get; set; }
    }
}
