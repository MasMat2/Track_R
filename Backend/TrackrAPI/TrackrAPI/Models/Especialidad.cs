using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            EspecialidadUsuario = new HashSet<EspecialidadUsuario>();
        }

        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<EspecialidadUsuario> EspecialidadUsuario { get; set; }
    }
}
