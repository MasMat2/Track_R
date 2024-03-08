using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class NotaVentaDetalle
    {
        public NotaVentaDetalle()
        {
            Comision = new HashSet<Comision>();
        }

        public int IdNotaVentaDetalle { get; set; }
        public int IdPresentacion { get; set; }
        public int IdNotaVenta { get; set; }
        public int IdImpuesto { get; set; }
        public int IdTipoDescuento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int? IdUbicacion { get; set; }
        public string? Lote { get; set; }
        public decimal? PrecioBase { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Impuesto { get; set; }
        public int? IdNotaVentaPadreDevolucion { get; set; }
        public int? IdUsuarioMedico { get; set; }
        public int? IdConcepto { get; set; }

        public virtual Concepto? IdConceptoNavigation { get; set; }
        public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;
        public virtual NotaVenta IdNotaVentaNavigation { get; set; } = null!;
        public virtual NotaVenta? IdNotaVentaPadreDevolucionNavigation { get; set; }
        public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
        public virtual TipoDescuento IdTipoDescuentoNavigation { get; set; } = null!;
        public virtual Ubicacion? IdUbicacionNavigation { get; set; }
        public virtual Usuario? IdUsuarioMedicoNavigation { get; set; }
        public virtual ICollection<Comision> Comision { get; set; }
    }
}
