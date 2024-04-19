using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ProyectoActividad
    {
        public ProyectoActividad()
        {
            ProyectoActividadEvidencia = new HashSet<ProyectoActividadEvidencia>();
            ProyectoActividadHora = new HashSet<ProyectoActividadHora>();
            ProyectoActividadParticipante = new HashSet<ProyectoActividadParticipante>();
        }

        public int IdProyectoActividad { get; set; }
        public int IdProyectoElementoTecnica { get; set; }
        public int? Numero { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdProyectoEstatus { get; set; }
        public int? EstatusCalculo { get; set; }
        public int? IdUsuarioResponsable { get; set; }
        public bool? Estatus { get; set; }
        public int? IdFlujo { get; set; }
        public int? IdFlujoDetalleAplicado { get; set; }

        public virtual ProyectoElementoTecnica IdProyectoElementoTecnicaNavigation { get; set; } = null!;
        public virtual ProyectoEstatus IdProyectoEstatusNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<ProyectoActividadEvidencia> ProyectoActividadEvidencia { get; set; }
        public virtual ICollection<ProyectoActividadHora> ProyectoActividadHora { get; set; }
        public virtual ICollection<ProyectoActividadParticipante> ProyectoActividadParticipante { get; set; }
    }
}
