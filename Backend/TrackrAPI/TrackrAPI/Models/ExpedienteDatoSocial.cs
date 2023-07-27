using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteDatoSocial
    {
        public ExpedienteDatoSocial()
        {
            Expediente = new HashSet<Expediente>();
        }

        public int IdExpedienteDatoSocial { get; set; }
        public string Nacionalidad { get; set; } = null!;
        public int CantidadHijos { get; set; }
        public int CantidadHijas { get; set; }
        public int CantidadHermanos { get; set; }
        public int CantidadHermanas { get; set; }
        public bool VivePadre { get; set; }
        public string NombrePadre { get; set; } = null!;
        public string ApellidoPaternoPadre { get; set; } = null!;
        public string? ApellidoMaternoPadre { get; set; }
        public bool ViveMadre { get; set; }
        public string NombreMadre { get; set; } = null!;
        public string ApellidoPaternoMadre { get; set; } = null!;
        public string ApellidoMaternoMadre { get; set; } = null!;
        public string? PersonaCercanaNombre { get; set; }
        public string? PersonaCercanaApellidoPaterno { get; set; }
        public string? PersonaCercanaApellidoMaterno { get; set; }
        public string OtrosServiciosMedicos { get; set; } = null!;
        public int? IdServicio { get; set; }
        public int IdEstadoCivil { get; set; }
        public int? IdParentesco { get; set; }
        public int IdCiudadNacimiento { get; set; }
        public string? ServicioMedico { get; set; }

        public virtual Municipio IdCiudadNacimientoNavigation { get; set; } = null!;
        public virtual EstadoCivil IdEstadoCivilNavigation { get; set; } = null!;
        public virtual Parentesco? IdParentescoNavigation { get; set; }
        public virtual Servicio? IdServicioNavigation { get; set; }
        public virtual ICollection<Expediente> Expediente { get; set; }
    }
}
