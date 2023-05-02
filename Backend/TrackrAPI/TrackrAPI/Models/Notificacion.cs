using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Notificacion
    {
        public Notificacion()
        {
            NotificacionUsuario = new HashSet<NotificacionUsuario>();
        }

        public int IdNotificacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Descripcion { get; set; } = null!;
        public string? Origen { get; set; }

        public virtual ICollection<NotificacionUsuario> NotificacionUsuario { get; set; }
    }
}
