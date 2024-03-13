using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class UsuarioAlmacen
    {
        public int IdUsuarioAlmacen { get; set; }
        public int IdUsuario { get; set; }
        public int IdAlmacen { get; set; }

        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
