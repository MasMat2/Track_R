using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class GuiaElementoTecnica
    {
        public GuiaElementoTecnica()
        {
            GuiaActividad = new HashSet<GuiaActividad>();
        }

        public int IdGuiaElementoTecnica { get; set; }
        public int IdGuia { get; set; }
        public string? Elemento { get; set; }
        public string? Tecnica { get; set; }
        public string? Clave { get; set; }
        public DateTime? FechaAlta { get; set; }
        public short? OrdenFuncional { get; set; }
        public string? Url { get; set; }
        public bool? Estatus { get; set; }

        public virtual Guia IdGuiaNavigation { get; set; } = null!;
        public virtual ICollection<GuiaActividad> GuiaActividad { get; set; }
    }
}
