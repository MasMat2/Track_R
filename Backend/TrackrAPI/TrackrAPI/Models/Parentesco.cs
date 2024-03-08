using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Parentesco
    {
        public Parentesco()
        {
            ExpedienteAntecedenteFamiliar = new HashSet<ExpedienteAntecedenteFamiliar>();
            ExpedienteDatoSocial = new HashSet<ExpedienteDatoSocial>();
        }

        public int IdParentesco { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool? EsFamiliar { get; set; }

        public virtual ICollection<ExpedienteAntecedenteFamiliar> ExpedienteAntecedenteFamiliar { get; set; }
        public virtual ICollection<ExpedienteDatoSocial> ExpedienteDatoSocial { get; set; }
    }
}
