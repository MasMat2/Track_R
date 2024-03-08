using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedientePacienteInformacion
    {
        public ExpedientePacienteInformacion()
        {
            Expediente = new HashSet<Expediente>();
        }

        public int IdExpedientePacienteInformacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Curp { get; set; } = null!;
        public string TelefonoCasa { get; set; } = null!;
        public string TelefonoMovil { get; set; } = null!;
        public string? Correo { get; set; }
        public bool EsPacienteExtranjero { get; set; }
        public bool EsPacienteEspecial { get; set; }
        public int? IdMunicipio { get; set; }
        public string? Colonia { get; set; }
        public string? Calle { get; set; }
        public string? EntreCalles { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }
        public string? CodigoPostal { get; set; }
        public int? IdGrupoSanguineo { get; set; }
        public int? IdFactorRh { get; set; }
        public int? IdTipoIdentificacion { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public string? MotivoPacienteEspecial { get; set; }

        public virtual FactorRh? IdFactorRhNavigation { get; set; }
        public virtual GrupoSanguineo? IdGrupoSanguineoNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual TipoIdentificacion? IdTipoIdentificacionNavigation { get; set; }
        public virtual ICollection<Expediente> Expediente { get; set; }
    }
}
