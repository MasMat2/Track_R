using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CertificadoLocacion
    {
        public int IdCertificadoLocacion { get; set; }
        public int IdLocacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string? TipoMime { get; set; }

        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
    }
}
