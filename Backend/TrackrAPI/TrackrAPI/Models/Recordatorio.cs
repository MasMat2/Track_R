using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Recordatorio
    {
        public int IdRecordatorio { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdUsuario { get; set; }
        public bool Visto { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
