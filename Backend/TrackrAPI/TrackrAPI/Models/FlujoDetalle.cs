using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class FlujoDetalle
    {
        public FlujoDetalle()
        {
            FlujoDetalleAplicado = new HashSet<FlujoDetalleAplicado>();
            FlujoDetalleResponsable = new HashSet<FlujoDetalleResponsable>();
        }

        public int IdFlujoDetalle { get; set; }
        public int IdFlujo { get; set; }
        public int Orden { get; set; }
        public int? IdArea { get; set; }
        public int IdEstatusPedido { get; set; }
        public int? IdRol { get; set; }
        public bool? SolicitarResponsable { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual EstatusPedido IdEstatusPedidoNavigation { get; set; } = null!;
        public virtual Flujo IdFlujoNavigation { get; set; } = null!;
        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<FlujoDetalleAplicado> FlujoDetalleAplicado { get; set; }
        public virtual ICollection<FlujoDetalleResponsable> FlujoDetalleResponsable { get; set; }
    }
}
