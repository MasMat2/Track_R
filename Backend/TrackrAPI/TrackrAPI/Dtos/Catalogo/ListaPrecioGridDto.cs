using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ListaPrecioGridDto
    {
        public int IdListaPrecio { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public string Clinica { get; set; }
    }
}
