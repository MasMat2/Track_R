using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            InventarioFisicoDetalle = new HashSet<InventarioFisicoDetalle>();
            Kardex = new HashSet<Kardex>();
            MovimientoMaterialDetalle = new HashSet<MovimientoMaterialDetalle>();
            OrdenCompraDetalle = new HashSet<OrdenCompraDetalle>();
            PresentacionArticulo = new HashSet<PresentacionArticulo>();
            TraspasoMovimientoMaterialDetalleIdArticuloDestinoNavigation = new HashSet<TraspasoMovimientoMaterialDetalle>();
            TraspasoMovimientoMaterialDetalleIdArticuloNavigation = new HashSet<TraspasoMovimientoMaterialDetalle>();
        }

        public int IdArticulo { get; set; }
        public string? Clave { get; set; }
        public string Nombre { get; set; } = null!;
        public string Formula { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        public string? Sku { get; set; }
        public string SustanciaActiva { get; set; } = null!;
        public bool DeInventario { get; set; }
        public bool DeVenta { get; set; }
        public bool EsServicio { get; set; }
        public bool ManejaPresentacion { get; set; }
        public bool EsGenerico { get; set; }
        public bool RequiereRecetaMedica { get; set; }
        public bool TieneLote { get; set; }
        public bool TieneUbicacion { get; set; }
        public bool? ManejaCaducidad { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdArticuloClase { get; set; }
        public int? IdSubSubCategoria { get; set; }
        public int IdArticuloPresentacion { get; set; }
        public int IdViaAdministracion { get; set; }
        public int IdFabricante { get; set; }
        public int IdCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
        public string? Estatus { get; set; }
        public string? Longitud { get; set; }
        public string? Ancho { get; set; }
        public string? Altura { get; set; }
        public bool? EsPsicotropico { get; set; }
        public bool? EsAntibiotico { get; set; }
        public string? Upc { get; set; }
        public int? IdCompania { get; set; }
        public decimal? CostoEstandar { get; set; }

        public virtual ArticuloClase IdArticuloClaseNavigation { get; set; } = null!;
        public virtual ArticuloPresentacion IdArticuloPresentacionNavigation { get; set; } = null!;
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Fabricante IdFabricanteNavigation { get; set; } = null!;
        public virtual Categoria? IdSubCategoriaNavigation { get; set; }
        public virtual Categoria? IdSubSubCategoriaNavigation { get; set; }
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; } = null!;
        public virtual ViaAdministracion IdViaAdministracionNavigation { get; set; } = null!;
        public virtual ICollection<InventarioFisicoDetalle> InventarioFisicoDetalle { get; set; }
        public virtual ICollection<Kardex> Kardex { get; set; }
        public virtual ICollection<MovimientoMaterialDetalle> MovimientoMaterialDetalle { get; set; }
        public virtual ICollection<OrdenCompraDetalle> OrdenCompraDetalle { get; set; }
        public virtual ICollection<PresentacionArticulo> PresentacionArticulo { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterialDetalle> TraspasoMovimientoMaterialDetalleIdArticuloDestinoNavigation { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterialDetalle> TraspasoMovimientoMaterialDetalleIdArticuloNavigation { get; set; }
    }
}
