using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoArticulo
    {
        public int IdTipoArticulo { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
