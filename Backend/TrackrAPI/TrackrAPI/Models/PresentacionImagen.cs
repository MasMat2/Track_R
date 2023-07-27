using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PresentacionImagen
    {
        public int IdPresentacionImagen { get; set; }
        public int IdPresentacion { get; set; }
        public byte[] Imagen { get; set; } = null!;
        public string TipoMime { get; set; } = null!;

        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
    }
}
