using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PolizaAplicada
    {
        public PolizaAplicada()
        {
            PolizaAplicadaDetalle = new HashSet<PolizaAplicadaDetalle>();
        }

        public int IdPolizaAplicada { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaMovimiento { get; set; }
        public DateTime FechaContable { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdTipoPoliza { get; set; }
        public int IdPoliza { get; set; }

        public virtual Poliza IdPolizaNavigation { get; set; } = null!;
        public virtual TipoPoliza IdTipoPolizaNavigation { get; set; } = null!;
        public virtual ICollection<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; }
    }
}
