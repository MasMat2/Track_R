using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Auxiliar
    {
        public Auxiliar()
        {
            ExpedienteAdministrativo = new HashSet<ExpedienteAdministrativo>();
            JerarquiaEstructura = new HashSet<JerarquiaEstructura>();
            NotaFlujoDetalle = new HashSet<NotaFlujoDetalle>();
            NotaGastoDetalle = new HashSet<NotaGastoDetalle>();
            PolizaAplicadaDetalle = new HashSet<PolizaAplicadaDetalle>();
            PolizaDetalle = new HashSet<PolizaDetalle>();
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdAuxiliar { get; set; }
        public string Numero { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdTipoAuxiliar { get; set; }
        public int IdCompania { get; set; }
        public bool? RecibeMovimientos { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual TipoAuxiliar IdTipoAuxiliarNavigation { get; set; } = null!;
        public virtual ICollection<ExpedienteAdministrativo> ExpedienteAdministrativo { get; set; }
        public virtual ICollection<JerarquiaEstructura> JerarquiaEstructura { get; set; }
        public virtual ICollection<NotaFlujoDetalle> NotaFlujoDetalle { get; set; }
        public virtual ICollection<NotaGastoDetalle> NotaGastoDetalle { get; set; }
        public virtual ICollection<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; }
        public virtual ICollection<PolizaDetalle> PolizaDetalle { get; set; }
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
