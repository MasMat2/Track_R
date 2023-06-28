using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProgramacionExamen
    {
        public ProgramacionExamen()
        {
            Examen = new HashSet<Examen>();
        }

        public int IdProgramacionExamen { get; set; }
        public int IdTipoExamen { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int? IdProyectoElementoTecnica { get; set; }
        public string? Clave { get; set; }
        public double? Duracion { get; set; }
        public int? CantidadParticipantes { get; set; }
        public DateTime? FechaExamen { get; set; }
        public TimeSpan? HoraExamen { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual ProyectoElementoTecnica? IdProyectoElementoTecnicaNavigation { get; set; }
        public virtual TipoExamen IdTipoExamenNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioResponsableNavigation { get; set; } = null!;
        public virtual ICollection<Examen> Examen { get; set; }
    }
}
