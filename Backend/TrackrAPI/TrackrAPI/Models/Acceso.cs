using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Acceso
    {
        public Acceso()
        {
            AccesoPerfil = new HashSet<AccesoPerfil>();
            InverseIdAccesoPadreNavigation = new HashSet<Acceso>();
            JerarquiaAccesoEstructura = new HashSet<JerarquiaAccesoEstructura>();
        }

        public int IdAcceso { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Url { get; set; }
        public int? OrdenMenu { get; set; }
        public int IdTipoAcceso { get; set; }
        public int? IdAccesoPadre { get; set; }
        public int? IdIcono { get; set; }
        public string? Descripcion { get; set; }

        public virtual Acceso? IdAccesoPadreNavigation { get; set; }
        public virtual Icono? IdIconoNavigation { get; set; }
        public virtual TipoAcceso IdTipoAccesoNavigation { get; set; } = null!;
        public virtual ICollection<AccesoPerfil> AccesoPerfil { get; set; }
        public virtual ICollection<Acceso> InverseIdAccesoPadreNavigation { get; set; }
        public virtual ICollection<JerarquiaAccesoEstructura> JerarquiaAccesoEstructura { get; set; }
    }
}
