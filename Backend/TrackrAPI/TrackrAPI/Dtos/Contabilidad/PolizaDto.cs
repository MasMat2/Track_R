using TrackrAPI.Dtos.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Contabilidad
{
    public class PolizaDto
    {
        public int IdPoliza { get; set; }
        public string Numero { get; set; }
        public int IdTipoPoliza { get; set; }
        public string DescripcionTipo { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public string ClaveMoneda { get; set; }
        public decimal CargoTotal { get; set; }
        public decimal AbonoTotal { get; set; }
        public DateTime FechaContable { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public IEnumerable<PolizaDetalleGridDto> PolizaDetalle {get; set;}
        public string Origen { get; set; }
        public int? IdOrigen { get; set; }
    }
}
