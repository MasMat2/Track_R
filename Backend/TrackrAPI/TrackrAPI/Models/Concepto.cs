using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Concepto
    {
        public Concepto()
        {
            ConfiguracionConcepto = new HashSet<ConfiguracionConcepto>();
            FacturaConcepto = new HashSet<FacturaConcepto>();
            GastoConcepto = new HashSet<GastoConcepto>();
            MovimientoMaterialDetalle = new HashSet<MovimientoMaterialDetalle>();
            NotaFlujo = new HashSet<NotaFlujo>();
            NotaFlujoDetalle = new HashSet<NotaFlujoDetalle>();
            NotaGasto = new HashSet<NotaGasto>();
            NotaGastoDetalle = new HashSet<NotaGastoDetalle>();
            NotaVenta = new HashSet<NotaVenta>();
            NotaVentaDetalle = new HashSet<NotaVentaDetalle>();
            Presentacion = new HashSet<Presentacion>();
            PuntoVenta = new HashSet<PuntoVenta>();
            TipoPago = new HashSet<TipoPago>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int IdConcepto { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdCompania { get; set; }
        public string? Clave { get; set; }
        public string? TipoMovimiento { get; set; }
        public int? IdCuentaContable { get; set; }
        public bool? Operativo { get; set; }
        public int? IdSatProductoServicio { get; set; }
        public int? IdSatUnidad { get; set; }
        public int? IdTipoAuxiliar { get; set; }

        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContableNavigation { get; set; }
        public virtual SatProductoServicio? IdSatProductoServicioNavigation { get; set; }
        public virtual SatUnidad? IdSatUnidadNavigation { get; set; }
        public virtual TipoAuxiliar? IdTipoAuxiliarNavigation { get; set; }
        public virtual ICollection<ConfiguracionConcepto> ConfiguracionConcepto { get; set; }
        public virtual ICollection<FacturaConcepto> FacturaConcepto { get; set; }
        public virtual ICollection<GastoConcepto> GastoConcepto { get; set; }
        public virtual ICollection<MovimientoMaterialDetalle> MovimientoMaterialDetalle { get; set; }
        public virtual ICollection<NotaFlujo> NotaFlujo { get; set; }
        public virtual ICollection<NotaFlujoDetalle> NotaFlujoDetalle { get; set; }
        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
        public virtual ICollection<NotaGastoDetalle> NotaGastoDetalle { get; set; }
        public virtual ICollection<NotaVenta> NotaVenta { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalle { get; set; }
        public virtual ICollection<Presentacion> Presentacion { get; set; }
        public virtual ICollection<PuntoVenta> PuntoVenta { get; set; }
        public virtual ICollection<TipoPago> TipoPago { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
