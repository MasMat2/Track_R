using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Chat
    {
        public Chat()
        {
            ChatMensaje = new HashSet<ChatMensaje>();
        }

        public int IdChat { get; set; }
        public DateTime Fecha { get; set; }
        public bool Habilitado { get; set; }
        public string? Titulo { get; set; }

        public virtual ICollection<ChatMensaje> ChatMensaje { get; set; }
    }
}
