using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Seguridad
{
    public class RolGridDto
    {
        public int IdRol { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Filtrado { get; set; }
        public int? IdCompania { get; set; }

        public RolGridDto(int idRol, string clave, string nombre, bool? filtrado, int? idCompania)
        {
            this.IdRol = idRol;
            this.Clave = clave;
            this.Nombre = nombre;
            this.Filtrado = filtrado != null && (bool)filtrado;
            this.IdCompania = idCompania;
        }
    }
}
