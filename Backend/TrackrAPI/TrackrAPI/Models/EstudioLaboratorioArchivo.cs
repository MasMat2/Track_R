using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstudioLaboratorioArchivo
    {
        public int IdEstudioLaboratorioArchivo { get; set; }
        public string TipoMime { get; set; } = null!;
        public byte[] Archivo { get; set; } = null!;
        public int IdEstudioLaboratorio { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual EstudioLaboratorio IdEstudioLaboratorioNavigation { get; set; } = null!;
    }
}
