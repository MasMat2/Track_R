using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Artefacto
    {
        public Artefacto()
        {
            GuiaActividadEvidencia = new HashSet<GuiaActividadEvidencia>();
        }

        public int IdArtefacto { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public byte[]? Archivo { get; set; }
        public string? ArchivoTipoMime { get; set; }
        public string? ArchivoNombre { get; set; }
        public bool Estatus { get; set; }
        public int IdLocacion { get; set; }
        public bool? AlmacenamientoExterno { get; set; }

        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual ICollection<GuiaActividadEvidencia> GuiaActividadEvidencia { get; set; }
    }
}
