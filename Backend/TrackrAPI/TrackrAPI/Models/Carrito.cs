using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdPresentacion { get; set; }
        public int? IdUsuarioComprador { get; set; }
        public decimal Cantidad { get; set; }
        public string? Comentarios { get; set; }
        public string? Token { get; set; }
        public decimal? Precio { get; set; }
        public int IdCompania { get; set; }

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioCompradorNavigation { get; set; }
    }
}
