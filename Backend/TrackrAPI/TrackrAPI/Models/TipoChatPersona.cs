using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoChatPersona
    {
        public TipoChatPersona()
        {
            ChatPersona = new HashSet<ChatPersona>();
        }

        public int IdTipoChatPersona { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<ChatPersona> ChatPersona { get; set; }
    }
}
