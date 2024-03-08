using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            ExpedienteDatoSocial = new HashSet<ExpedienteDatoSocial>();
        }

        public int IdServicio { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedienteDatoSocial> ExpedienteDatoSocial { get; set; }
    }
}
