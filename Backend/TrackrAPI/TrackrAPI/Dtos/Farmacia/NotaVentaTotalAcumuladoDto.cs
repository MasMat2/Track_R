using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Dtos.Farmacia
{
    public class NotaVentaTotalAcumuladoDto
    {
        public string Categoria { get; set; }
        public bool EsConcepto { get; set; }
        public decimal Total { get; set; }
        public int Cantidad { get; set; }
    }
}
