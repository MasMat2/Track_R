using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Compania
    {
        public Compania()
        {
            JerarquiaAcceso = new HashSet<JerarquiaAcceso>();
            Locacion = new HashSet<Locacion>();
            Perfil = new HashSet<Perfil>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdCompania { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Correo { get; set; }
        public string? PortalWeb { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExterior { get; set; }
        public string? NumeroInterior { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono { get; set; }
        public string? Ciudad { get; set; }
        public string? Rfc { get; set; }
        public int? IdEstado { get; set; }
        public int? IdLada { get; set; }
        public int IdTipoCompania { get; set; }

        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual Lada? IdLadaNavigation { get; set; }
        public virtual TipoCompania IdTipoCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<JerarquiaAcceso> JerarquiaAcceso { get; set; }
        public virtual ICollection<Locacion> Locacion { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
