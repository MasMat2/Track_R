using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;

namespace TrackrAPI.Dtos.Farmacia
{
    public class NotaVentaDetalleDto
    {
        public int IdNotaVentaDetalle { get; set; }
        public int IdPresentacion { get; set; }
        public int IdNotaVenta { get; set; }
        public int IdImpuesto { get; set; }
        public int IdTipoDescuento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Impuesto { get; set; }
        public string Descripcion { get; set; }
        public double Importe { get; set; }
        public int? IdUbicacion { get; set; }
        public string Lote { get; set; }
        public string NombrePresentacion { get; set; }
        public decimal? Descuento { get; set; }
        public int IdUsuarioComision { get; set; }
        public int IdUsuarioMedico { get; set; }
        public List<ArticuloDto> Articulos { get; set; }
    }
}
