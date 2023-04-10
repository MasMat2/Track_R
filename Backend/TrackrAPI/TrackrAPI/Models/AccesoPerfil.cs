using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class AccesoPerfil
    {
        public int IdAccesoPerfil { get; set; }
        public int IdAcceso { get; set; }
        public int IdPerfil { get; set; }

        public virtual Acceso IdAccesoNavigation { get; set; } = null!;
        public virtual Perfil IdPerfilNavigation { get; set; } = null!;
    }
}
