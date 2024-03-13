using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class CuentaContable
    {
        public CuentaContable()
        {
            AgrupadorCuentaContableIdCuentaContableCapitalNavigation = new HashSet<AgrupadorCuentaContable>();
            AgrupadorCuentaContableIdCuentaContableResultadoNavigation = new HashSet<AgrupadorCuentaContable>();
            Almacen = new HashSet<Almacen>();
            BalanceCuentaContable = new HashSet<BalanceCuentaContable>();
            CajaIdCuentaContableAutomaticaNavigation = new HashSet<Caja>();
            CajaIdCuentaContableNavigation = new HashSet<Caja>();
            Concepto = new HashSet<Concepto>();
            Impuesto = new HashSet<Impuesto>();
            ImpuestoDetalleIdCuentaContableAbonoNavigation = new HashSet<ImpuestoDetalle>();
            ImpuestoDetalleIdCuentaContableCargoNavigation = new HashSet<ImpuestoDetalle>();
            InverseIdCuentaContablePadreNavigation = new HashSet<CuentaContable>();
            JerarquiaEstructura = new HashSet<JerarquiaEstructura>();
            PolizaAplicadaDetalle = new HashSet<PolizaAplicadaDetalle>();
            PolizaDetalle = new HashSet<PolizaDetalle>();
            TipoActivo = new HashSet<TipoActivo>();
            TipoMovimientoMaterialCuentaContableIdCuentaContableAbonoNavigation = new HashSet<TipoMovimientoMaterialCuentaContable>();
            TipoMovimientoMaterialCuentaContableIdCuentaContableCargoNavigation = new HashSet<TipoMovimientoMaterialCuentaContable>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public int IdCuentaContable { get; set; }
        public string Numero { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int? IdSubtipoCuentaContable { get; set; }
        public int? IdTipoCuentaContable { get; set; }
        public bool Reconciliatoria { get; set; }
        public bool RecibeMovimientos { get; set; }
        public bool Auxiliar { get; set; }
        public bool PartidaAbierta { get; set; }
        public bool Automatica { get; set; }
        public int? IdCompania { get; set; }
        public int? IdTipoAuxiliar { get; set; }
        public int? IdAgrupadorCuentaContable { get; set; }
        public int? IdCuentaContablePadre { get; set; }
        public bool? EsConcepto { get; set; }

        public virtual AgrupadorCuentaContable? IdAgrupadorCuentaContableNavigation { get; set; }
        public virtual Compania? IdCompaniaNavigation { get; set; }
        public virtual CuentaContable? IdCuentaContablePadreNavigation { get; set; }
        public virtual SubtipoCuentaContable? IdSubtipoCuentaContableNavigation { get; set; }
        public virtual TipoAuxiliar? IdTipoAuxiliarNavigation { get; set; }
        public virtual TipoCuentaContable? IdTipoCuentaContableNavigation { get; set; }
        public virtual ICollection<AgrupadorCuentaContable> AgrupadorCuentaContableIdCuentaContableCapitalNavigation { get; set; }
        public virtual ICollection<AgrupadorCuentaContable> AgrupadorCuentaContableIdCuentaContableResultadoNavigation { get; set; }
        public virtual ICollection<Almacen> Almacen { get; set; }
        public virtual ICollection<BalanceCuentaContable> BalanceCuentaContable { get; set; }
        public virtual ICollection<Caja> CajaIdCuentaContableAutomaticaNavigation { get; set; }
        public virtual ICollection<Caja> CajaIdCuentaContableNavigation { get; set; }
        public virtual ICollection<Concepto> Concepto { get; set; }
        public virtual ICollection<Impuesto> Impuesto { get; set; }
        public virtual ICollection<ImpuestoDetalle> ImpuestoDetalleIdCuentaContableAbonoNavigation { get; set; }
        public virtual ICollection<ImpuestoDetalle> ImpuestoDetalleIdCuentaContableCargoNavigation { get; set; }
        public virtual ICollection<CuentaContable> InverseIdCuentaContablePadreNavigation { get; set; }
        public virtual ICollection<JerarquiaEstructura> JerarquiaEstructura { get; set; }
        public virtual ICollection<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; }
        public virtual ICollection<PolizaDetalle> PolizaDetalle { get; set; }
        public virtual ICollection<TipoActivo> TipoActivo { get; set; }
        public virtual ICollection<TipoMovimientoMaterialCuentaContable> TipoMovimientoMaterialCuentaContableIdCuentaContableAbonoNavigation { get; set; }
        public virtual ICollection<TipoMovimientoMaterialCuentaContable> TipoMovimientoMaterialCuentaContableIdCuentaContableCargoNavigation { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
