using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class UsuarioLocacion
    {
        public int IdUsuarioLocacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdLocacion { get; set; }
        public int IdPerfil { get; set; }

        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual Perfil IdPerfilNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
