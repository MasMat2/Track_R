using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Archivo
    {
        public int IdArchivo { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public byte[] Archivo1 { get; set; } = null!;
        public string ArchivoTipoMime { get; set; } = null!;
        public string? ArchivoNombre { get; set; }
    }
}
