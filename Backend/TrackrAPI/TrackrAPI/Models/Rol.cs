using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Rol
    {
        public Rol()
        {
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int IdRol { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }
        public bool? Filtrado { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
