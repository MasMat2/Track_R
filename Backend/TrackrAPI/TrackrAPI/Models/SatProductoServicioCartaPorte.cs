using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class SatProductoServicioCartaPorte
    {
        public SatProductoServicioCartaPorte()
        {
            ExpedienteAdministrativoMercancia = new HashSet<ExpedienteAdministrativoMercancia>();
        }

        public int IdSatProductoServicioCartaPorte { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public virtual ICollection<ExpedienteAdministrativoMercancia> ExpedienteAdministrativoMercancia { get; set; }
    }
}
