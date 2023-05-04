using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Farmacia
{
    public class NotaVentaDetalleFiltroDto
    {
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string Presentacion { get; set; }
        public int IdCategoria { get; set; }
        public int IdLocacion { get; set; }
    }
}
