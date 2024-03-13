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
            UsuarioLocacion = new HashSet<UsuarioLocacion>();
        }

        public int IdPerfil { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdCompania { get; set; }
        public string? Clave { get; set; }
        public int? IdTipoCompania { get; set; }
        public int? IdJerarquiaAcceso { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual JerarquiaAcceso? IdJerarquiaAccesoNavigation { get; set; }
        public virtual TipoCompania? IdTipoCompaniaNavigation { get; set; }
        public virtual ICollection<AccesoPerfil> AccesoPerfil { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<UsuarioLocacion> UsuarioLocacion { get; set; }
    }
}
