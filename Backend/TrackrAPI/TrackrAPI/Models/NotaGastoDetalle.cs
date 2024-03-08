using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaGastoDetalle
    {
        public int IdNotaGastoDetalle { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Monto { get; set; }
        public int IdNotaGasto { get; set; }
        public int IdConcepto { get; set; }
        public int? IdExpedienteAdministrativo { get; set; }
        public int? IdAuxiliar { get; set; }
        public int? IdImpuesto { get; set; }
        public int? IdComision { get; set; }
        public string? Anexo { get; set; }

        public virtual Auxiliar? IdAuxiliarNavigation { get; set; }
        public virtual Comision? IdComisionNavigation { get; set; }
        public virtual Concepto IdConceptoNavigation { get; set; } = null!;
        public virtual ExpedienteAdministrativo? IdExpedienteAdministrativoNavigation { get; set; }
        public virtual Impuesto? IdImpuestoNavigation { get; set; }
        public virtual NotaGasto IdNotaGastoNavigation { get; set; } = null!;
    }
}
