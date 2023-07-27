using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class EstadoCivil
    {
        public EstadoCivil()
        {
            ExpedienteDatoSocial = new HashSet<ExpedienteDatoSocial>();
        }

        public int IdEstadoCivil { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedienteDatoSocial> ExpedienteDatoSocial { get; set; }
    }
}
