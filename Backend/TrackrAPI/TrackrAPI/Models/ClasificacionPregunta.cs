using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ClasificacionPregunta
    {
        public int IdClasificacionPregunta { get; set; }
        public string? Nombre { get; set; }
        public bool? Estatus { get; set; }
        public string? Clave { get; set; }
    }
}
