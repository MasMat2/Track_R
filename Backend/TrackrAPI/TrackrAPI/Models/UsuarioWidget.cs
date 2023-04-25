using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class UsuarioWidget
    {
        public int IdUsuarioWidget { get; set; }
        public int IdWidget { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual Widget IdWidgetNavigation { get; set; } = null!;
    }
}
