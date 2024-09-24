using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExamenReactivo
    {
        public int IdExamenReactivo { get; set; }
        public int IdExamen { get; set; }
        public int IdReactivo { get; set; }
        public bool? Resultado { get; set; }
        public string? RespuestaAlumno { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }
        public int? RespuestaValor { get; set; }

        public virtual Examen IdExamenNavigation { get; set; } = null!;
        public virtual Reactivo IdReactivoNavigation { get; set; } = null!;
    }
}
