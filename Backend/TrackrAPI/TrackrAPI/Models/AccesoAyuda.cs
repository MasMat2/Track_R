using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class AccesoAyuda
    {
        public int IdAccesoAyuda { get; set; }
        public int IdAcceso { get; set; }
        public string? EtiquetaCampo { get; set; }
        public string DescripcionAyuda { get; set; } = null!;
        public byte[]? Imagen { get; set; }
        public string? TipoMime { get; set; }
        public int? Orden { get; set; }
        public string? NombreArchivo { get; set; }
        public int? IdAyudaSeccion { get; set; }

        public virtual Acceso IdAccesoNavigation { get; set; } = null!;
        public virtual AyudaSeccion? IdAyudaSeccionNavigation { get; set; }
    }
}
