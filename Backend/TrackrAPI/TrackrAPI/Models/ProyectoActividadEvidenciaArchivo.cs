using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoActividadEvidenciaArchivo
    {
        public int IdProyectoActividadEvidenciaArchivo { get; set; }
        public int IdProyectoActividadEvidencia { get; set; }
        public byte[]? Archivo { get; set; }
        public string ArchivoTipoMime { get; set; } = null!;
        public string ArchivoNombre { get; set; } = null!;
        public int Version { get; set; }
        public string? Descripcion { get; set; }
        public bool Estatus { get; set; }
        public bool? AlmacenamientoExterno { get; set; }

        public virtual ProyectoActividadEvidencia IdProyectoActividadEvidenciaNavigation { get; set; } = null!;
    }
}
