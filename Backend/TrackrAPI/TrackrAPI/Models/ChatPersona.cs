﻿using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ChatPersona
    {
        public int IdChatPersona { get; set; }
        public int IdChat { get; set; }
        public int IdPersona { get; set; }
        public int IdTipo { get; set; }

        public virtual Usuario IdPersonaNavigation { get; set; } = null!;
        public virtual TipoChatPersona IdTipoNavigation { get; set; } = null!;
    }
}
