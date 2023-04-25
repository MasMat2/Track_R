using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioRol = new HashSet<UsuarioRol>();
            UsuarioWidget = new HashSet<UsuarioWidget>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string? TelefonoMovil { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public bool Habilitado { get; set; }
        public string? ImagenTipoMime { get; set; }
        public int IdTipoUsuario { get; set; }
        public int IdPerfil { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Perfil IdPerfilNavigation { get; set; } = null!;
        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
        public virtual ICollection<UsuarioWidget> UsuarioWidget { get; set; }
    }
}
