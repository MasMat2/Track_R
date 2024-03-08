using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Compania
    {
        public Compania()
        {
            Almacen = new HashSet<Almacen>();
            Area = new HashSet<Area>();
            Articulo = new HashSet<Articulo>();
            ArticuloClase = new HashSet<ArticuloClase>();
            ArticuloPresentacion = new HashSet<ArticuloPresentacion>();
            Auxiliar = new HashSet<Auxiliar>();
            BalanceCuentaContable = new HashSet<BalanceCuentaContable>();
            Carrito = new HashSet<Carrito>();
            Categoria = new HashSet<Categoria>();
            CompaniaContacto = new HashSet<CompaniaContacto>();
            CompaniaLogotipo = new HashSet<CompaniaLogotipo>();
            Concepto = new HashSet<Concepto>();
            Configuracion = new HashSet<Configuracion>();
            ConfiguracionVigencia = new HashSet<ConfiguracionVigencia>();
            CuentaContable = new HashSet<CuentaContable>();
            Departamento = new HashSet<Departamento>();
            Domicilio = new HashSet<Domicilio>();
            DominioDetalle = new HashSet<DominioDetalle>();
            ExcelPolizaCargaMasiva = new HashSet<ExcelPolizaCargaMasiva>();
            Expediente = new HashSet<Expediente>();
            ExpedienteAdministrativo = new HashSet<ExpedienteAdministrativo>();
            Fabricante = new HashSet<Fabricante>();
            Flujo = new HashSet<Flujo>();
            Hospital = new HashSet<Hospital>();
            Impuesto = new HashSet<Impuesto>();
            Jerarquia = new HashSet<Jerarquia>();
            JerarquiaAcceso = new HashSet<JerarquiaAcceso>();
            Liquidacion = new HashSet<Liquidacion>();
            MercadoCompania = new HashSet<MercadoCompania>();
            MovimientoEstadoCuenta = new HashSet<MovimientoEstadoCuenta>();
            Pedido = new HashSet<Pedido>();
            Perfil = new HashSet<Perfil>();
            Poliza = new HashSet<Poliza>();
            Presentacion = new HashSet<Presentacion>();
            Proveedor = new HashSet<Proveedor>();
            Rol = new HashSet<Rol>();
            TipoActivo = new HashSet<TipoActivo>();
            TipoCambio = new HashSet<TipoCambio>();
            TipoCliente = new HashSet<TipoCliente>();
            TipoComision = new HashSet<TipoComision>();
            TipoDescuento = new HashSet<TipoDescuento>();
            TipoEmpleado = new HashSet<TipoEmpleado>();
            TipoExpedienteAdministrativo = new HashSet<TipoExpedienteAdministrativo>();
            TipoPoliza = new HashSet<TipoPoliza>();
            TipoProveedor = new HashSet<TipoProveedor>();
            UnidadMedida = new HashSet<UnidadMedida>();
            Usuario = new HashSet<Usuario>();
            Vehiculo = new HashSet<Vehiculo>();
            VersionPoliza = new HashSet<VersionPoliza>();
        }

        public int IdCompania { get; set; }
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Correo { get; set; }
        public string? PortalWeb { get; set; }
        public string? Calle { get; set; }
        public string? NumeroExterior { get; set; }
        public string? NumeroInterior { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono { get; set; }
        public string? Ciudad { get; set; }
        public int? IdEstado { get; set; }
        public string? Rfc { get; set; }
        public int? IdLada { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdAgrupadorCuentaContable { get; set; }
        public int? IdTipoCompania { get; set; }
        public bool? AfectacionContable { get; set; }
        public int? IdGiroComercial { get; set; }
        public bool? Timbrado { get; set; }
        public int? IdMunicipio { get; set; }
        public bool? UsoAlmacen { get; set; }

        public virtual AgrupadorCuentaContable? IdAgrupadorCuentaContableNavigation { get; set; }
        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual GiroComercial? IdGiroComercialNavigation { get; set; }
        public virtual Lada? IdLadaNavigation { get; set; }
        public virtual Moneda? IdMonedaNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual RegimenFiscal? IdRegimenFiscalNavigation { get; set; }
        public virtual TipoCompania? IdTipoCompaniaNavigation { get; set; }
        public virtual ICollection<Almacen> Almacen { get; set; }
        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
        public virtual ICollection<ArticuloClase> ArticuloClase { get; set; }
        public virtual ICollection<ArticuloPresentacion> ArticuloPresentacion { get; set; }
        public virtual ICollection<Auxiliar> Auxiliar { get; set; }
        public virtual ICollection<BalanceCuentaContable> BalanceCuentaContable { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Categoria> Categoria { get; set; }
        public virtual ICollection<CompaniaContacto> CompaniaContacto { get; set; }
        public virtual ICollection<CompaniaLogotipo> CompaniaLogotipo { get; set; }
        public virtual ICollection<Concepto> Concepto { get; set; }
        public virtual ICollection<Configuracion> Configuracion { get; set; }
        public virtual ICollection<ConfiguracionVigencia> ConfiguracionVigencia { get; set; }
        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
        public virtual ICollection<Departamento> Departamento { get; set; }
        public virtual ICollection<Domicilio> Domicilio { get; set; }
        public virtual ICollection<DominioDetalle> DominioDetalle { get; set; }
        public virtual ICollection<ExcelPolizaCargaMasiva> ExcelPolizaCargaMasiva { get; set; }
        public virtual ICollection<Expediente> Expediente { get; set; }
        public virtual ICollection<ExpedienteAdministrativo> ExpedienteAdministrativo { get; set; }
        public virtual ICollection<Fabricante> Fabricante { get; set; }
        public virtual ICollection<Flujo> Flujo { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Impuesto> Impuesto { get; set; }
        public virtual ICollection<Jerarquia> Jerarquia { get; set; }
        public virtual ICollection<JerarquiaAcceso> JerarquiaAcceso { get; set; }
        public virtual ICollection<Liquidacion> Liquidacion { get; set; }
        public virtual ICollection<MercadoCompania> MercadoCompania { get; set; }
        public virtual ICollection<MovimientoEstadoCuenta> MovimientoEstadoCuenta { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
        public virtual ICollection<Poliza> Poliza { get; set; }
        public virtual ICollection<Presentacion> Presentacion { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
        public virtual ICollection<Rol> Rol { get; set; }
        public virtual ICollection<TipoActivo> TipoActivo { get; set; }
        public virtual ICollection<TipoCambio> TipoCambio { get; set; }
        public virtual ICollection<TipoCliente> TipoCliente { get; set; }
        public virtual ICollection<TipoComision> TipoComision { get; set; }
        public virtual ICollection<TipoDescuento> TipoDescuento { get; set; }
        public virtual ICollection<TipoEmpleado> TipoEmpleado { get; set; }
        public virtual ICollection<TipoExpedienteAdministrativo> TipoExpedienteAdministrativo { get; set; }
        public virtual ICollection<TipoPoliza> TipoPoliza { get; set; }
        public virtual ICollection<TipoProveedor> TipoProveedor { get; set; }
        public virtual ICollection<UnidadMedida> UnidadMedida { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
        public virtual ICollection<VersionPoliza> VersionPoliza { get; set; }
    }
}
