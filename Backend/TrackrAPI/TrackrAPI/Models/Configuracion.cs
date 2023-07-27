using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Configuracion
    {
        public int IdConfiguracion { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Valor { get; set; } = null!;
        public int IdHospital { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
    }
}
