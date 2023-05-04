using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class AgrupadorCuentaContableDto
    {
        public int IdAgrupadorCuentaContable { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int? IdCuentaContableCapital { get; set; }
        public int? IdCuentaContableResultado { get; set; }
    }
}
