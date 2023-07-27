using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ListaPrecioDetalleDto
    {
        public int IdListaPrecioDetalle { get; set; }
        public string Clave { get; set; }
        public DateTime FechaAlta { get; set; }
        public decimal PrecioBase { get; set; }
        public int IdImpuesto { get; set; }
        public int IdComision { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdListaPrecio { get; set; }
        public int IdPresentacion { get; set; }
        public int IdDescuento { get; set; }
        public string NombreUsuarioAlta { get; set; }

         public ListaPrecioDetalleDto() { }
    }
}
