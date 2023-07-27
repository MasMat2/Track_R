using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CitaArchivo
    {
        public int IdCitaArchivo { get; set; }
        public int IdCita { get; set; }
        public byte[] Archivo { get; set; } = null!;
        public string TipoMime { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual Cita IdCitaNavigation { get; set; } = null!;
    }
}
