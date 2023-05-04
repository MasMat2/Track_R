using System;

namespace TrackrAPI.Dtos.Catalogo
{
    public class ArticuloDto
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string Sku { get; set; }
        public string SustanciaActiva { get; set; }
        public bool? DeInventario { get; set; }
        public bool? DeVenta { get; set; }
        public bool? EsServicio { get; set; }
        public bool? ManejaPresentacion { get; set; }
        public bool? EsGenerico { get; set; }
        public bool? RequiereRecetaMedica { get; set; }
        public bool? TieneLote { get; set; }
        public bool? TieneUbicacion { get; set; }
        public bool? ManejaCaducidad { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdArticuloClase { get; set; }
        public int? IdSubSubCategoria { get; set; }
        public int? IdArticuloPresentacion { get; set; }
        public int? IdViaAdministracion { get; set; }
        public int? IdFabricante { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
        public string Formula { get; set; }
        public string Estatus { get; set; }
        public string nombreUnidadMedida { get; set; }
        public string Longitud { get; set; }
        public string Ancho { get; set; }
        public string Altura { get; set; }
        public bool? EsPsicotropico { get; set; }
        public bool? EsAntibiotico { get; set; }
        public string Upc { get; set; }
        public decimal? CostoEstandar { get; set; }
        public int? IdCompania { get; set; }
        // Extras Punto Venta
        public string Lote { get; set; }
        public int? IdUbicacion { get; set; }
    }
}
