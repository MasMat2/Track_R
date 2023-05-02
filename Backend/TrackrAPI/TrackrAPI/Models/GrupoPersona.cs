using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GrupoPersona
    {
        public GrupoPersona()
        {
            CitaGrupoPersona = new HashSet<CitaGrupoPersona>();
            GrupoPersonaActividad = new HashSet<GrupoPersonaActividad>();
        }

        public int IdGrupoPersona { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? Sexo { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }

        public virtual ICollection<CitaGrupoPersona> CitaGrupoPersona { get; set; }
        public virtual ICollection<GrupoPersonaActividad> GrupoPersonaActividad { get; set; }
    }
}
