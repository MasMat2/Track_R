using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class PolizaFiltroDto
    {
        public DateTime? FechaContable { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string Numero { get; set; }
        public int IdTipoPoliza { get; set; }
        public int IdCompania { get; set; }
    }
}
