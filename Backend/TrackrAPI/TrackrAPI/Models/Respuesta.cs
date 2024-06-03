using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Respuesta
    {
        public int IdRespuesta { get; set; }
        public int IdReactivo { get; set; }
        public string? Clave { get; set; }
        public string? Respuesta1 { get; set; }
        public bool? RespuestaCorrecta { get; set; }
        public int? Valor { get; set; }

        public virtual Reactivo IdReactivoNavigation { get; set; } = null!;
    }
}
