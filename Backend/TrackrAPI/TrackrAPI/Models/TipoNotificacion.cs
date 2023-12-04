using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoNotificacion
    {
        public TipoNotificacion()
        {
            Notificacion = new HashSet<Notificacion>();
        }

        public int IdTipoNotificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Clave { get; set; }

        public virtual ICollection<Notificacion> Notificacion { get; set; }
    }
}
