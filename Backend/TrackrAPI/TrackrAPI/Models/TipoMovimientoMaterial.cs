using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TipoMovimientoMaterial
    {
        public TipoMovimientoMaterial()
        {
            MovimientoMaterial = new HashSet<MovimientoMaterial>();
            TipoMovimientoMaterialCuentaContable = new HashSet<TipoMovimientoMaterialCuentaContable>();
        }

        public int IdTipoMovimientoMaterial { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EsPositivo { get; set; }
        public bool? Automatico { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<MovimientoMaterial> MovimientoMaterial { get; set; }
        public virtual ICollection<TipoMovimientoMaterialCuentaContable> TipoMovimientoMaterialCuentaContable { get; set; }
    }
}
