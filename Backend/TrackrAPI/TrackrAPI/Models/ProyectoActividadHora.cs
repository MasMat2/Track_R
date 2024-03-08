using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoActividadHora
    {
        public int IdProyectoActividadHora { get; set; }
        public int IdProyectoActividad { get; set; }
        public int Horas { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdUsuarioAlta { get; set; }
        public DateTime FechaAlta { get; set; }

        public virtual ProyectoActividad IdProyectoActividadNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
    }
}
