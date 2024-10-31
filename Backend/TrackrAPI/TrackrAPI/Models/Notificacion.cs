using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Notificacion
    {
        public Notificacion()
        {
            DetalleExpedienteRecomendacionesGenerales = new HashSet<DetalleExpedienteRecomendacionesGenerales>();
            ExpedienteRecomendaciones = new HashSet<ExpedienteRecomendaciones>();
            NotificacionDoctor = new HashSet<NotificacionDoctor>();
            NotificacionUsuario = new HashSet<NotificacionUsuario>();
            TratamientoToma = new HashSet<TratamientoToma>();
        }

        public int IdNotificacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Mensaje { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public int IdTipoNotificacion { get; set; }
        public int? IdPersona { get; set; }
        public int? IdChat { get; set; }
        public string? ComplementoMensaje { get; set; }
        public int? IdPadecimiento { get; set; }

        public virtual Chat? IdChatNavigation { get; set; }
        public virtual EntidadEstructura? IdPadecimientoNavigation { get; set; }
        public virtual Usuario? IdPersonaNavigation { get; set; }
        public virtual TipoNotificacion IdTipoNotificacionNavigation { get; set; } = null!;
        public virtual ICollection<DetalleExpedienteRecomendacionesGenerales> DetalleExpedienteRecomendacionesGenerales { get; set; }
        public virtual ICollection<ExpedienteRecomendaciones> ExpedienteRecomendaciones { get; set; }
        public virtual ICollection<NotificacionDoctor> NotificacionDoctor { get; set; }
        public virtual ICollection<NotificacionUsuario> NotificacionUsuario { get; set; }
        public virtual ICollection<TratamientoToma> TratamientoToma { get; set; }
    }
}
