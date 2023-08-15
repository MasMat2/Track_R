using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteTrackr
    {
        public ExpedienteTrackr()
        {
            ExpedienteDoctor = new HashSet<ExpedienteDoctor>();
            ExpedienteEstudio = new HashSet<ExpedienteEstudio>();
            ExpedientePadecimiento = new HashSet<ExpedientePadecimiento>();
            ExpedienteRecomendaciones = new HashSet<ExpedienteRecomendaciones>();
            ExpedienteTratamiento = new HashSet<ExpedienteTratamiento>();
        }

        public int IdExpediente { get; set; }
        public string Numero { get; set; } = null!;
        public int IdUsuario { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public decimal Peso { get; set; }
        public decimal Cintura { get; set; }
        public decimal Estatura { get; set; }
        public int IdGenero { get; set; }
        public DateTime FechaAlta { get; set; }

        public virtual Genero IdGeneroNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteDoctor> ExpedienteDoctor { get; set; }
        public virtual ICollection<ExpedienteEstudio> ExpedienteEstudio { get; set; }
        public virtual ICollection<ExpedientePadecimiento> ExpedientePadecimiento { get; set; }
        public virtual ICollection<ExpedienteRecomendaciones> ExpedienteRecomendaciones { get; set; }
        public virtual ICollection<ExpedienteTratamiento> ExpedienteTratamiento { get; set; }
    }
}
