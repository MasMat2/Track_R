using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Examen
    {
        public Examen()
        {
            ExamenReactivo = new HashSet<ExamenReactivo>();
        }

        public int IdExamen { get; set; }
        public int IdProgramacionExamen { get; set; }
        public int IdUsuarioParticipante { get; set; }
        public int IdEstatusExamen { get; set; }
        public double? Resultado { get; set; }
        public int? PreguntasCorrectas { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual EstatusExamen IdEstatusExamenNavigation { get; set; } = null!;
        public virtual ProgramacionExamen IdProgramacionExamenNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioParticipanteNavigation { get; set; } = null!;
        public virtual ICollection<ExamenReactivo> ExamenReactivo { get; set; }
    }
}
