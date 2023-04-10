using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            AccesoPerfil = new HashSet<AccesoPerfil>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdPerfil { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<AccesoPerfil> AccesoPerfil { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
