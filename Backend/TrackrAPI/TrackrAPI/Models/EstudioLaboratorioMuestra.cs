using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstudioLaboratorioMuestra
    {
        public int IdEstudioLaboratorioMuestra { get; set; }
        public decimal Cantidad { get; set; }
        public int IdTipoMuestra { get; set; }
        public int IdEstudioLaboratorio { get; set; }
        public string? Numero { get; set; }
        public DateTime FechaAlta { get; set; }

        public virtual EstudioLaboratorio IdEstudioLaboratorioNavigation { get; set; } = null!;
        public virtual TipoMuestra IdTipoMuestraNavigation { get; set; } = null!;
    }
}
