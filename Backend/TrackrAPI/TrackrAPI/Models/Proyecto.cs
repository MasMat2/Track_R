using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            HistorialMovimiento = new HashSet<HistorialMovimiento>();
            ProyectoElementoTecnica = new HashSet<ProyectoElementoTecnica>();
        }

        public int IdProyecto { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public int IdLocacion { get; set; }
        public int IdGuia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public bool? Estatus { get; set; }
        public int? IdUsuarioAdministrador { get; set; }

        public virtual Guia IdGuiaNavigation { get; set; } = null!;
        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioAdministradorNavigation { get; set; }
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioResponsableNavigation { get; set; } = null!;
        public virtual ICollection<HistorialMovimiento> HistorialMovimiento { get; set; }
        public virtual ICollection<ProyectoElementoTecnica> ProyectoElementoTecnica { get; set; }
    }
}
