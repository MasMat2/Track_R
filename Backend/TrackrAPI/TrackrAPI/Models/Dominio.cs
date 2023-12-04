using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Dominio
    {
        public Dominio()
        {
            DominioDetalle = new HashSet<DominioDetalle>();
            ExpedienteCampo = new HashSet<ExpedienteCampo>();
            SeccionCampo = new HashSet<SeccionCampo>();
        }

        public int IdDominio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoDato { get; set; } = null!;
        public string TipoCampo { get; set; } = null!;
        public int? LongitudMinima { get; set; }
        public int? LongitudMaxima { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public DateTime? FechaMinima { get; set; }
        public DateTime? FechaMaxima { get; set; }
        public bool? PermiteFueraDeRango { get; set; }
        public string? UnidadMedida { get; set; }

        public virtual ICollection<DominioDetalle> DominioDetalle { get; set; }
        public virtual ICollection<ExpedienteCampo> ExpedienteCampo { get; set; }
        public virtual ICollection<SeccionCampo> SeccionCampo { get; set; }
    }
}
