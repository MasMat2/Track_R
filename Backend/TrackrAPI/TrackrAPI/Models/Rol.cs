using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Flujo = new HashSet<Flujo>();
            FlujoDetalle = new HashSet<FlujoDetalle>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
            TipoComisionDetalle = new HashSet<TipoComisionDetalle>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int IdRol { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }
        public bool? Filtrado { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ICollection<Flujo> Flujo { get; set; }
        public virtual ICollection<FlujoDetalle> FlujoDetalle { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
        public virtual ICollection<TipoComisionDetalle> TipoComisionDetalle { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
