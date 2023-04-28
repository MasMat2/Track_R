using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Colonia
    {
        public int IdColonia { get; set; }
        public string Clave { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
