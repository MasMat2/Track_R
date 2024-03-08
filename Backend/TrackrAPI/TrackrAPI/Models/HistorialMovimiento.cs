using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class HistorialMovimiento
    {
        public int IdHistorialMovimiento { get; set; }
        public string? Descripcion { get; set; }
        public string? Tipo { get; set; }
        public string? Folio { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdProyecto { get; set; }
        public bool? Estatus { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
    }
}
