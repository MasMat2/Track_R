using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class PuntoVentaGridDto
    {
        public int IdPuntoVenta { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public int IdAlmacen { get; set; }
        public int? IdUbicacionVenta { get; set; }
        public string NombreUbicacionVenta { get; set; }
        public int? IdConcepto { get; set; }
        public string NombreConcepto { get; set; }
    }
}
