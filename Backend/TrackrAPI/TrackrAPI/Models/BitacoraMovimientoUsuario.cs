using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class BitacoraMovimientoUsuario
    {
        public int IdBitacoraMovimientoUsuario { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdLocacion { get; set; }

        public virtual Hospital IdLocacionNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;
    }
}
