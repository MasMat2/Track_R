using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteTrackr
    {
        public ExpedienteTrackr()
        {
            ExpedienteEstudio = new HashSet<ExpedienteEstudio>();
            ExpedientePadecimiento = new HashSet<ExpedientePadecimiento>();
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

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteEstudio> ExpedienteEstudio { get; set; }
        public virtual ICollection<ExpedientePadecimiento> ExpedientePadecimiento { get; set; }
    }
}
