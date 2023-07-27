using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Plantilla
    {
        public int IdPlantilla { get; set; }
        public string Formato { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
