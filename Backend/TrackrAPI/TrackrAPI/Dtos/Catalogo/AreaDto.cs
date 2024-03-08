using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Catalogo
{
    public class AreaDto
    {
        public int IdArea { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int? IdCompania { get; set; }

        public AreaDto(int IdArea, string Clave, string Nombre, int? IdCompania)
        {
            this.IdArea = IdArea;
            this.Clave = Clave;
            this.Nombre = Nombre;
            this.IdCompania = IdCompania;
        }
    }
}
