using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class InventarioFisico
    {
        public InventarioFisico()
        {
            InventarioFisicoAjuste = new HashSet<InventarioFisicoAjuste>();
            InventarioFisicoDetalle = new HashSet<InventarioFisicoDetalle>();
        }

        public int IdInventarioFisico { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public int IdEstatusInventarioFisico { get; set; }
        public int IdUsuarioAlmacenista { get; set; }
        public int IdAlmacen { get; set; }
        public decimal ValorTotalDiferencia { get; set; }

        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
        public virtual EstatusInventarioFisico IdEstatusInventarioFisicoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioAlmacenistaNavigation { get; set; } = null!;
        public virtual ICollection<InventarioFisicoAjuste> InventarioFisicoAjuste { get; set; }
        public virtual ICollection<InventarioFisicoDetalle> InventarioFisicoDetalle { get; set; }
    }
}
