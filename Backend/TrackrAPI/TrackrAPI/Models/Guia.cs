using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Guia
    {
        public Guia()
        {
            GuiaElementoTecnica = new HashSet<GuiaElementoTecnica>();
            Proyecto = new HashSet<Proyecto>();
        }

        public int IdGuia { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public int IdTipoGuia { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public bool? Estatus { get; set; }

        public virtual TipoGuia IdTipoGuiaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
        public virtual ICollection<GuiaElementoTecnica> GuiaElementoTecnica { get; set; }
        public virtual ICollection<Proyecto> Proyecto { get; set; }
    }
}
