using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CompaniaContacto
    {
        public int IdCompaniaContacto { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string TelefonoMovil { get; set; } = null!;
        public int IdCompania { get; set; }
        public int IdLada { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Lada IdLadaNavigation { get; set; } = null!;
    }
}
