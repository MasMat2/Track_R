using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Presentacion
    {
        public Presentacion()
        {
            Carrito = new HashSet<Carrito>();
            Cita = new HashSet<Cita>();
            ConfiguracionOpcionVenta = new HashSet<ConfiguracionOpcionVenta>();
            DevolucionPresentacion = new HashSet<DevolucionPresentacion>();
            FacturaConcepto = new HashSet<FacturaConcepto>();
            ListaPrecioDetalle = new HashSet<ListaPrecioDetalle>();
            NecesidadPresentacion = new HashSet<NecesidadPresentacion>();
            NotaVentaDetalle = new HashSet<NotaVentaDetalle>();
            OrdenImagenologia = new HashSet<OrdenImagenologia>();
            OrdenLaboratorio = new HashSet<OrdenLaboratorio>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
            PresentacionArticulo = new HashSet<PresentacionArticulo>();
            PresentacionImagen = new HashSet<PresentacionImagen>();
            RecepcionPresentacion = new HashSet<RecepcionPresentacion>();
            RecetaDetalle = new HashSet<RecetaDetalle>();
            ReciboDetalle = new HashSet<ReciboDetalle>();
            RemisionPresentacion = new HashSet<RemisionPresentacion>();
            UrgenciaTratamiento = new HashSet<UrgenciaTratamiento>();
        }

        public int IdPresentacion { get; set; }
        public string? Clave { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Nombre { get; set; } = null!;
        public bool EsKit { get; set; }
        public string? Sku { get; set; }
        public int? IdTipoPresentacion { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? IdArea { get; set; }
        public bool EsServicio { get; set; }
        public string? Indicaciones { get; set; }
        public string? Upc { get; set; }
        public int? IdFlujo { get; set; }
        public int? IdCompania { get; set; }
        public int? Calificacion { get; set; }
        public int? IdConcepto { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual Flujo? IdFlujoNavigation { get; set; }
        public virtual TipoPresentacion? IdTipoPresentacionNavigation { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
        public virtual ICollection<ConfiguracionOpcionVenta> ConfiguracionOpcionVenta { get; set; }
        public virtual ICollection<DevolucionPresentacion> DevolucionPresentacion { get; set; }
        public virtual ICollection<FacturaConcepto> FacturaConcepto { get; set; }
        public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalle { get; set; }
        public virtual ICollection<NecesidadPresentacion> NecesidadPresentacion { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalle { get; set; }
        public virtual ICollection<OrdenImagenologia> OrdenImagenologia { get; set; }
        public virtual ICollection<OrdenLaboratorio> OrdenLaboratorio { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
        public virtual ICollection<PresentacionArticulo> PresentacionArticulo { get; set; }
        public virtual ICollection<PresentacionImagen> PresentacionImagen { get; set; }
        public virtual ICollection<RecepcionPresentacion> RecepcionPresentacion { get; set; }
        public virtual ICollection<RecetaDetalle> RecetaDetalle { get; set; }
        public virtual ICollection<ReciboDetalle> ReciboDetalle { get; set; }
        public virtual ICollection<RemisionPresentacion> RemisionPresentacion { get; set; }
        public virtual ICollection<UrgenciaTratamiento> UrgenciaTratamiento { get; set; }
    }
}
