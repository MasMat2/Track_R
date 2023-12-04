using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ConfirmacionCorreo
    {
        public int IdConfirmacionCorreo { get; set; }
        public string Clave { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
