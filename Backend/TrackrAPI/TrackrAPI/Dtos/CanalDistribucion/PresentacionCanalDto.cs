using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.CanalDistribucion
{
    public class PresentacionCanalDto
    {
        public int idPresentacion { get; set; }
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string codigoBarras { get; set; }
        public string nombreTipoProducto { get; set; }
        public string nombreSubTipoProducto { get; set; }
        public string medida { get; set; }
        public string nombreProducto { get; set; }
        public string nombreConFormato { get; set; }

    }
}
