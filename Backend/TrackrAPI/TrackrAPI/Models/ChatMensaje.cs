using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ChatMensaje
    {
        public int IdChatMensaje { get; set; }
        public int IdChat { get; set; }
        public int IdPersona { get; set; }
        public string Mensaje { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int IdPersonaVisto { get; set; }

        public virtual Chat IdChatNavigation { get; set; } = null!;
        public virtual Usuario IdPersonaNavigation { get; set; } = null!;
        public virtual ChatPersona IdPersonaVistoNavigation { get; set; } = null!;
    }
}
