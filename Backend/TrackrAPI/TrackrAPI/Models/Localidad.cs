using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Localidad
    {
        public int IdLocalidad { get; set; }
        public string Clave { get; set; } = null!;
        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual Estado IdEstadoNavigation { get; set; } = null!;
    }
}
