using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CompaniaLogotipo
    {
        public int IdCompaniaLogotipo { get; set; }
        public int IdCompania { get; set; }
        public string Imagen { get; set; } = null!;
        public string TipoMime { get; set; } = null!;
        public byte[] Archivo { get; set; } = null!;

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
    }
}
