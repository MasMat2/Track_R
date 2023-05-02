using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class PedidoBitacora
    {
        public int IdPedidoBitacora { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public string? Descripcion { get; set; }
        public int? IdPedidoPresentacion { get; set; }

        public virtual PedidoPresentacion? IdPedidoPresentacionNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
