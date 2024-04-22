using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EntidadEstructuraTablaValor
    {
        public int IdEntidadEstructuraTablaValor { get; set; }
        public int Numero { get; set; }
        public int IdEntidadEstructura { get; set; }
        public int IdSeccion { get; set; }
        public string? Valor { get; set; }
        public int? IdTabla { get; set; }
        public bool? FueraDeRango { get; set; }
        public DateTime? FechaMuestra { get; set; }

        public virtual EntidadEstructura IdEntidadEstructuraNavigation { get; set; } = null!;
        public virtual SeccionCampo IdSeccionNavigation { get; set; } = null!;
    }
}
