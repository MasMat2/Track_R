using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Notificacion
    {
        public Notificacion()
        {
            NotificacionDoctor = new HashSet<NotificacionDoctor>();
            NotificacionUsuario = new HashSet<NotificacionUsuario>();
        }

        public int IdNotificacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Mensaje { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public int IdTipoNotificacion { get; set; }

        public virtual TipoNotificacion IdTipoNotificacionNavigation { get; set; } = null!;
        public virtual ICollection<NotificacionDoctor> NotificacionDoctor { get; set; }
        public virtual ICollection<NotificacionUsuario> NotificacionUsuario { get; set; }
    }
}
