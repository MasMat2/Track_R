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
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<UsuarioWidget> UsuarioWidget { get; set; }
    }
}
