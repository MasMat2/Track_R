using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoMovimientoMaterialCuentaContable
    {
        public int IdTipoMovimientoMaterialCuentaContable { get; set; }
        public int IdTipoMovimientoMaterial { get; set; }
        public int IdCuentaContableCargo { get; set; }
        public int IdCuentaContableAbono { get; set; }

        public virtual CuentaContable IdCuentaContableAbonoNavigation { get; set; } = null!;
        public virtual CuentaContable IdCuentaContableCargoNavigation { get; set; } = null!;
        public virtual TipoMovimientoMaterial IdTipoMovimientoMaterialNavigation { get; set; } = null!;
    }
}
