using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatUnidad
    {
        public SatUnidad()
        {
            Concepto = new HashSet<Concepto>();
            ExpedienteAdministrativoMercancia = new HashSet<ExpedienteAdministrativoMercancia>();
        }

        public int IdSatUnidad { get; set; }
        public string Clave { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Concepto> Concepto { get; set; }
        public virtual ICollection<ExpedienteAdministrativoMercancia> ExpedienteAdministrativoMercancia { get; set; }
    }
}
