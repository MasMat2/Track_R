using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ContenidoExamen
    {
        public int IdContenidoExamen { get; set; }
        public int IdTipoExamen { get; set; }
        public int IdAsignatura { get; set; }
        public int IdNivelExamen { get; set; }
        public string? Clave { get; set; }
        public int? TotalPreguntas { get; set; }
        public double? Duracion { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
        public virtual NivelExamen IdNivelExamenNavigation { get; set; } = null!;
        public virtual TipoExamen IdTipoExamenNavigation { get; set; } = null!;
    }
}
