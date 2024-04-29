using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GuiaActividad
    {
        public GuiaActividad()
        {
            GuiaActividadEvidencia = new HashSet<GuiaActividadEvidencia>();
        }

        public int IdGuiaActividad { get; set; }
        public int IdGuiaElementoTecnica { get; set; }
        public int? Numero { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
        public int? HorasEstimadas { get; set; }
        public bool? Estatus { get; set; }
        public int? IdFlujo { get; set; }

        public virtual GuiaElementoTecnica IdGuiaElementoTecnicaNavigation { get; set; } = null!;
        public virtual ICollection<GuiaActividadEvidencia> GuiaActividadEvidencia { get; set; }
    }
}
