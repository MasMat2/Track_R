using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Widget
    {
        public Widget()
        {
            UsuarioWidget = new HashSet<UsuarioWidget>();
        }

        public int IdWidget { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Clave { get; set; }

        public virtual ICollection<UsuarioWidget> UsuarioWidget { get; set; }
    }
}
