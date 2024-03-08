using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoPoliza
    {
        public TipoPoliza()
        {
            Poliza = new HashSet<Poliza>();
            PolizaAplicada = new HashSet<PolizaAplicada>();
        }

        public int IdTipoPoliza { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool AgregadoPorSistema { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual ICollection<Poliza> Poliza { get; set; }
        public virtual ICollection<PolizaAplicada> PolizaAplicada { get; set; }
    }
}
