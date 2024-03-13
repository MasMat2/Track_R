using System;

namespace TrackrAPI.Dtos.Farmacia
{
    public class NotaVentaDetalleReporteEstadoResultadoDto
    {
        public DateTime FechaAlta { get; set; }
        public decimal Cantidad { get; set; }
        public string NombrePresentacion { get; set; }
        public string Categoria { get; set; }
        public decimal Importe { get; set; }
    }
}