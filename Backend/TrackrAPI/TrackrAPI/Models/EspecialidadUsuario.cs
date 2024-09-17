using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EspecialidadUsuario
    {
        public int IdEspecialidadUsuario { get; set; }
        public int? IdEspecialidad { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Especialidad? IdEspecialidadNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
