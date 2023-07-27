using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Horario
    {
        public int IdHorario { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
