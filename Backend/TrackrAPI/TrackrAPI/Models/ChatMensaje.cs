using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ChatMensaje
    {
        public ChatMensaje()
        {
            ChatMensajeVisto = new HashSet<ChatMensajeVisto>();
        }

        public int IdChatMensaje { get; set; }
        public int IdChat { get; set; }
        public int IdPersona { get; set; }
        public string Mensaje { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int? IdArchivo { get; set; }

        public virtual Archivo? IdArchivoNavigation { get; set; }
        public virtual Chat IdChatNavigation { get; set; } = null!;
        public virtual Usuario IdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<ChatMensajeVisto> ChatMensajeVisto { get; set; }
    }
}
