using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PedidoPresentacion
    {
        public PedidoPresentacion()
        {
            PedidoBitacora = new HashSet<PedidoBitacora>();
        }

        public int IdPedidoPresentacion { get; set; }
        public int IdPedido { get; set; }
        public int IdSucursal { get; set; }
        public int IdPresentacion { get; set; }
        public decimal Cantidad { get; set; }
        public string? Comentarios { get; set; }
        public decimal Precio { get; set; }
        public int? Calificacion { get; set; }
        public string? ComentarioCalificacion { get; set; }
        public int? IdFlujoDetalleAplicado { get; set; }
        public int? IdArea { get; set; }
        public int? IdEstatusPedido { get; set; }
        public int? IdRol { get; set; }
        public int? IdUsuarioResponsable { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual EstatusPedido? IdEstatusPedidoNavigation { get; set; }
        public virtual FlujoDetalleAplicado? IdFlujoDetalleAplicadoNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Rol? IdRolNavigation { get; set; }
        public virtual Hospital IdSucursalNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<PedidoBitacora> PedidoBitacora { get; set; }
    }
}
