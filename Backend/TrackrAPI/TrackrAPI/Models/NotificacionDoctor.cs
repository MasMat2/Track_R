using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotificacionDoctor
    {
        public int IdNotificacionDoctor { get; set; }
        public int IdNotificacion { get; set; }
        public int IdPaciente { get; set; }

        public virtual Notificacion IdNotificacionNavigation { get; set; } = null!;
        public virtual Usuario IdPacienteNavigation { get; set; } = null!;
    }
}
