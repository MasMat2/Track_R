using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ClasificacionPregunta
    {
        public ClasificacionPregunta()
        {
            RespuestasClasificacionPregunta = new HashSet<RespuestasClasificacionPregunta>();
        }

        public string? Nombre { get; set; }
        public bool? Estatus { get; set; }
        public string? Clave { get; set; }
        public int IdClasificacionPregunta { get; set; }

        public virtual ICollection<RespuestasClasificacionPregunta> RespuestasClasificacionPregunta { get; set; }
    }
}
