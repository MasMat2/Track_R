using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ChatMensajeVisto
    {
        public int IdMensajeVisto { get; set; }
        public int? IdPersona { get; set; }
        public int? IdMensaje { get; set; }

        public virtual ChatMensaje? IdMensajeNavigation { get; set; }
        public virtual Usuario? IdPersonaNavigation { get; set; }
    }
}
