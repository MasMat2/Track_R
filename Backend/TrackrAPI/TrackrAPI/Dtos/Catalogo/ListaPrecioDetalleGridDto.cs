using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ListaPrecioDetalleGridDto
    {
        public int IdListaPrecioDetalle { get; set; }
        public int IdDescuento { get; set; }
        public string Clave { get; set; }
        public string Sku { get; set; }
        public string NombrePresentacion { get; set; }
        public decimal PrecioBase { get; set; }
        public bool Kit { get; set; }
        public string TipoIVA { get; set; }
        public string TipoComision { get; set; }
        public string TipoDescuento { get; set; }

    }
}
