using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaFlujoDetalle
    {
        public NotaFlujoDetalle()
        {
            ComplementoPagoDetalle = new HashSet<ComplementoPagoDetalle>();
        }

        public int IdNotaFlujoDetalle { get; set; }
        public int IdNotaFlujo { get; set; }
        public int? IdUsuario { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public int IdConcepto { get; set; }
        public int? IdExpedienteAdministrativo { get; set; }
        public bool? Conciliado { get; set; }
        public string? Origen { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAuxiliar { get; set; }
        public int? IdImpuesto { get; set; }
        public string? TipoMovimiento { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual ExpedienteAdministrativo? IdExpedienteAdministrativoNavigation { get; set; }
        public virtual Impuesto? IdImpuestoNavigation { get; set; }
        public virtual NotaFlujo IdNotaFlujoNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<ComplementoPagoDetalle> ComplementoPagoDetalle { get; set; }
    }
}
