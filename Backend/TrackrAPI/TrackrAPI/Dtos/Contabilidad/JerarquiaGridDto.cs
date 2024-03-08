using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class JerarquiaGridDto
    {
        public int IdJerarquia { get; set; }
        public int IdCompania { get; set; }
        public string Nombre { get; set; }
        public bool InvertirSigno { get; set; }
        public bool Estandar { get; set; }
    }
}
