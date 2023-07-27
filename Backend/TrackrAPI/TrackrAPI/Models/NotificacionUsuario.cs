using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotificacionUsuario
    {
        public int IdNotificacionUsuario { get; set; }
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public bool Visto { get; set; }

        public virtual Notificacion IdNotificacionNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
