using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EntradaPersonal
    {
        public int IdEntradaPersonal { get; set; }
        public DateTime FechaAlta { get; set; }
        public int IdUsuario { get; set; }
        public int IdHospital { get; set; }
        public bool Habilitado { get; set; }
        public int? IdUsuarioBaja { get; set; }
        public DateTime? FechaBaja { get; set; }

        public virtual Hospital IdHospitalNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioBajaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
