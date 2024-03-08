using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Enfermedad
    {
        public Enfermedad()
        {
            ExpedienteAntecedenteFamiliar = new HashSet<ExpedienteAntecedenteFamiliar>();
            ExpedienteAntecedenteNoPatologico = new HashSet<ExpedienteAntecedenteNoPatologico>();
            ExpedienteAntecedentePatologico = new HashSet<ExpedienteAntecedentePatologico>();
        }

        public int IdEnfermedad { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool? EsPatologico { get; set; }
        public bool? EsHeredofamiliar { get; set; }

        public virtual ICollection<ExpedienteAntecedenteFamiliar> ExpedienteAntecedenteFamiliar { get; set; }
        public virtual ICollection<ExpedienteAntecedenteNoPatologico> ExpedienteAntecedenteNoPatologico { get; set; }
        public virtual ICollection<ExpedienteAntecedentePatologico> ExpedienteAntecedentePatologico { get; set; }
    }
}
