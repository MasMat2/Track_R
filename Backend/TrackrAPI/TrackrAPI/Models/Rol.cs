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

        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
