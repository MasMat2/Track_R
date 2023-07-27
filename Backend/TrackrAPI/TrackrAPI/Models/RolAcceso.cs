using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class RolAcceso
    {
        public RolAcceso()
        {
            Acceso = new HashSet<Acceso>();
        }

        public int IdRolAcceso { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Acceso> Acceso { get; set; }
    }
}
