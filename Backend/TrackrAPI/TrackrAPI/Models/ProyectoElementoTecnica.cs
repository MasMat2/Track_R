using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoElementoTecnica
    {
        public ProyectoElementoTecnica()
        {
            ProgramacionExamen = new HashSet<ProgramacionExamen>();
            ProyectoActividad = new HashSet<ProyectoActividad>();
        }

        public int IdProyectoElementoTecnica { get; set; }
        public int IdProyecto { get; set; }
        public string? Clave { get; set; }
        public string? Elemento { get; set; }
        public string? Tecnica { get; set; }
        public string? Url { get; set; }
        public int? OrdenFuncional { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdProyectoEstatus { get; set; }
        public string? Comentario { get; set; }
        public int? EstatusCalculo { get; set; }
        public bool Liberado { get; set; }
        public bool? Estatus { get; set; }

        public virtual ProyectoEstatus IdProyectoEstatusNavigation { get; set; } = null!;
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        public virtual ICollection<ProgramacionExamen> ProgramacionExamen { get; set; }
        public virtual ICollection<ProyectoActividad> ProyectoActividad { get; set; }
    }
}
