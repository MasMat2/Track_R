using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Expediente
    {
        public Expediente()
        {
            Cita = new HashSet<Cita>();
            ExpedienteAntecedenteFamiliar = new HashSet<ExpedienteAntecedenteFamiliar>();
            ExpedienteAntecedenteNoPatologico = new HashSet<ExpedienteAntecedenteNoPatologico>();
            ExpedienteAntecedentePatologico = new HashSet<ExpedienteAntecedentePatologico>();
            ExpedienteAntecedenteTratamiento = new HashSet<ExpedienteAntecedenteTratamiento>();
            ExpedienteBitacora = new HashSet<ExpedienteBitacora>();
            Paciente = new HashSet<Paciente>();
        }

        public int IdExpediente { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime? FechaApertura { get; set; }
        public int? IdExpedientePacienteInformacion { get; set; }
        public int? IdExpedienteDatoSocial { get; set; }
        public int? IdUsuarioPaciente { get; set; }
        public int? IdCompania { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual ExpedienteDatoSocial? IdExpedienteDatoSocialNavigation { get; set; }
        public virtual ExpedientePacienteInformacion? IdExpedientePacienteInformacionNavigation { get; set; }
        public virtual Usuario? IdUsuarioPacienteNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<ExpedienteAntecedenteFamiliar> ExpedienteAntecedenteFamiliar { get; set; }
        public virtual ICollection<ExpedienteAntecedenteNoPatologico> ExpedienteAntecedenteNoPatologico { get; set; }
        public virtual ICollection<ExpedienteAntecedentePatologico> ExpedienteAntecedentePatologico { get; set; }
        public virtual ICollection<ExpedienteAntecedenteTratamiento> ExpedienteAntecedenteTratamiento { get; set; }
        public virtual ICollection<ExpedienteBitacora> ExpedienteBitacora { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}
