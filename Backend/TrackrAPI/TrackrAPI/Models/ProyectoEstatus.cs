using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoEstatus
    {
        public ProyectoEstatus()
        {
            ProyectoActividad = new HashSet<ProyectoActividad>();
            ProyectoElementoTecnica = new HashSet<ProyectoElementoTecnica>();
        }

        public int IdProyectoEstatus { get; set; }
        public string? Nombre { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<ProyectoActividad> ProyectoActividad { get; set; }
        public virtual ICollection<ProyectoElementoTecnica> ProyectoElementoTecnica { get; set; }
    }
}
