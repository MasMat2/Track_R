using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstudioImagenologiaArchivo
    {
        public int IdEstudioImagenologiaArchivo { get; set; }
        public int IdEstudioImagenologia { get; set; }
        public string TipoMime { get; set; } = null!;
        public byte[] Archivo { get; set; } = null!;
        public string? Nombre { get; set; }
    }
}
