using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class ExpedienteAdministrativo
    {
        public ExpedienteAdministrativo()
        {
            ExpedienteAdministrativoMercancia = new HashSet<ExpedienteAdministrativoMercancia>();
            ExpedienteAdministrativoViaje = new HashSet<ExpedienteAdministrativoViaje>();
            ExpedienteAuxiliar = new HashSet<ExpedienteAuxiliar>();
            Factura = new HashSet<Factura>();
            MovimientoMaterial = new HashSet<MovimientoMaterial>();
            NotaFlujoDetalle = new HashSet<NotaFlujoDetalle>();
            NotaGastoDetalle = new HashSet<NotaGastoDetalle>();
            NotaVenta = new HashSet<NotaVenta>();
        }

        public int IdExpedienteAdministrativo { get; set; }
        public string Numero { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int IdCompania { get; set; }
        public int? IdTipoExpedienteAdministrativo { get; set; }
        public int? IdAuxiliar { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual TipoExpedienteAdministrativo? IdTipoExpedienteAdministrativoNavigation { get; set; }
        public virtual ICollection<ExpedienteAdministrativoMercancia> ExpedienteAdministrativoMercancia { get; set; }
        public virtual ICollection<ExpedienteAdministrativoViaje> ExpedienteAdministrativoViaje { get; set; }
        public virtual ICollection<ExpedienteAuxiliar> ExpedienteAuxiliar { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<MovimientoMaterial> MovimientoMaterial { get; set; }
        public virtual ICollection<NotaFlujoDetalle> NotaFlujoDetalle { get; set; }
        public virtual ICollection<NotaGastoDetalle> NotaGastoDetalle { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
    }
}
