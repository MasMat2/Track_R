using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrackrAPI.Models
{
    public partial class TrackrContext : DbContext
    {
        public TrackrContext()
        {
        }

        public TrackrContext(DbContextOptions<TrackrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acceso> Acceso { get; set; } = null!;
        public virtual DbSet<AccesoAyuda> AccesoAyuda { get; set; } = null!;
        public virtual DbSet<AccesoPerfil> AccesoPerfil { get; set; } = null!;
        public virtual DbSet<AgrupadorCuentaContable> AgrupadorCuentaContable { get; set; } = null!;
        public virtual DbSet<Almacen> Almacen { get; set; } = null!;
        public virtual DbSet<Area> Area { get; set; } = null!;
        public virtual DbSet<Artefacto> Artefacto { get; set; } = null!;
        public virtual DbSet<Articulo> Articulo { get; set; } = null!;
        public virtual DbSet<ArticuloClase> ArticuloClase { get; set; } = null!;
        public virtual DbSet<ArticuloPresentacion> ArticuloPresentacion { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignatura { get; set; } = null!;
        public virtual DbSet<Auxiliar> Auxiliar { get; set; } = null!;
        public virtual DbSet<AyudaSeccion> AyudaSeccion { get; set; } = null!;
        public virtual DbSet<BalanceCuentaContable> BalanceCuentaContable { get; set; } = null!;
        public virtual DbSet<Banco> Banco { get; set; } = null!;
        public virtual DbSet<BitacoraMovimientoUsuario> BitacoraMovimientoUsuario { get; set; } = null!;
        public virtual DbSet<Caja> Caja { get; set; } = null!;
        public virtual DbSet<CajaTurno> CajaTurno { get; set; } = null!;
        public virtual DbSet<CajaTurnoDetalle> CajaTurnoDetalle { get; set; } = null!;
        public virtual DbSet<Carrito> Carrito { get; set; } = null!;
        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<CategoriaImagen> CategoriaImagen { get; set; } = null!;
        public virtual DbSet<CertificadoLocacion> CertificadoLocacion { get; set; } = null!;
        public virtual DbSet<Cie> Cie { get; set; } = null!;
        public virtual DbSet<Cierre> Cierre { get; set; } = null!;
        public virtual DbSet<Cita> Cita { get; set; } = null!;
        public virtual DbSet<CitaArchivo> CitaArchivo { get; set; } = null!;
        public virtual DbSet<CitaGrupoPersona> CitaGrupoPersona { get; set; } = null!;
        public virtual DbSet<CobranzaPago> CobranzaPago { get; set; } = null!;
        public virtual DbSet<CodigoPostal> CodigoPostal { get; set; } = null!;
        public virtual DbSet<Colonia> Colonia { get; set; } = null!;
        public virtual DbSet<Comision> Comision { get; set; } = null!;
        public virtual DbSet<Compania> Compania { get; set; } = null!;
        public virtual DbSet<CompaniaContacto> CompaniaContacto { get; set; } = null!;
        public virtual DbSet<CompaniaLogotipo> CompaniaLogotipo { get; set; } = null!;
        public virtual DbSet<ComplementoPago> ComplementoPago { get; set; } = null!;
        public virtual DbSet<ComplementoPagoDetalle> ComplementoPagoDetalle { get; set; } = null!;
        public virtual DbSet<Concepto> Concepto { get; set; } = null!;
        public virtual DbSet<Configuracion> Configuracion { get; set; } = null!;
        public virtual DbSet<ConfiguracionAutotransporte> ConfiguracionAutotransporte { get; set; } = null!;
        public virtual DbSet<ConfiguracionConcepto> ConfiguracionConcepto { get; set; } = null!;
        public virtual DbSet<ConfiguracionOpcionVenta> ConfiguracionOpcionVenta { get; set; } = null!;
        public virtual DbSet<ConfiguracionVigencia> ConfiguracionVigencia { get; set; } = null!;
        public virtual DbSet<ContenidoExamen> ContenidoExamen { get; set; } = null!;
        public virtual DbSet<CuentaContable> CuentaContable { get; set; } = null!;
        public virtual DbSet<Departamento> Departamento { get; set; } = null!;
        public virtual DbSet<Deposito> Deposito { get; set; } = null!;
        public virtual DbSet<DetalleExpedienteRecomendacionesGenerales> DetalleExpedienteRecomendacionesGenerales { get; set; } = null!;
        public virtual DbSet<Devolucion> Devolucion { get; set; } = null!;
        public virtual DbSet<DevolucionPresentacion> DevolucionPresentacion { get; set; } = null!;
        public virtual DbSet<Direccion> Direccion { get; set; } = null!;
        public virtual DbSet<DistributedLocks> DistributedLocks { get; set; } = null!;
        public virtual DbSet<Domicilio> Domicilio { get; set; } = null!;
        public virtual DbSet<Dominio> Dominio { get; set; } = null!;
        public virtual DbSet<DominioDetalle> DominioDetalle { get; set; } = null!;
        public virtual DbSet<Enfermedad> Enfermedad { get; set; } = null!;
        public virtual DbSet<Entidad> Entidad { get; set; } = null!;
        public virtual DbSet<EntidadEstructura> EntidadEstructura { get; set; } = null!;
        public virtual DbSet<EntidadEstructuraTablaValor> EntidadEstructuraTablaValor { get; set; } = null!;
        public virtual DbSet<EntidadEstructuraValor> EntidadEstructuraValor { get; set; } = null!;
        public virtual DbSet<EntradaPersonal> EntradaPersonal { get; set; } = null!;
        public virtual DbSet<Especialidad> Especialidad { get; set; } = null!;
        public virtual DbSet<Estado> Estado { get; set; } = null!;
        public virtual DbSet<EstadoCivil> EstadoCivil { get; set; } = null!;
        public virtual DbSet<EstadoProducto> EstadoProducto { get; set; } = null!;
        public virtual DbSet<EstatusAlmacen> EstatusAlmacen { get; set; } = null!;
        public virtual DbSet<EstatusCita> EstatusCita { get; set; } = null!;
        public virtual DbSet<EstatusEstudioImagenologia> EstatusEstudioImagenologia { get; set; } = null!;
        public virtual DbSet<EstatusEstudioLaboratorio> EstatusEstudioLaboratorio { get; set; } = null!;
        public virtual DbSet<EstatusExamen> EstatusExamen { get; set; } = null!;
        public virtual DbSet<EstatusFactura> EstatusFactura { get; set; } = null!;
        public virtual DbSet<EstatusInventarioFisico> EstatusInventarioFisico { get; set; } = null!;
        public virtual DbSet<EstatusLiquidacion> EstatusLiquidacion { get; set; } = null!;
        public virtual DbSet<EstatusMovimientoMaterial> EstatusMovimientoMaterial { get; set; } = null!;
        public virtual DbSet<EstatusNotaFlujo> EstatusNotaFlujo { get; set; } = null!;
        public virtual DbSet<EstatusNotaGasto> EstatusNotaGasto { get; set; } = null!;
        public virtual DbSet<EstatusNotaVenta> EstatusNotaVenta { get; set; } = null!;
        public virtual DbSet<EstatusOrdenCompra> EstatusOrdenCompra { get; set; } = null!;
        public virtual DbSet<EstatusPaciente> EstatusPaciente { get; set; } = null!;
        public virtual DbSet<EstatusPago> EstatusPago { get; set; } = null!;
        public virtual DbSet<EstatusPedido> EstatusPedido { get; set; } = null!;
        public virtual DbSet<EstatusRemision> EstatusRemision { get; set; } = null!;
        public virtual DbSet<EstudioImagenologia> EstudioImagenologia { get; set; } = null!;
        public virtual DbSet<EstudioImagenologiaArchivo> EstudioImagenologiaArchivo { get; set; } = null!;
        public virtual DbSet<EstudioLaboratorio> EstudioLaboratorio { get; set; } = null!;
        public virtual DbSet<EstudioLaboratorioArchivo> EstudioLaboratorioArchivo { get; set; } = null!;
        public virtual DbSet<EstudioLaboratorioMuestra> EstudioLaboratorioMuestra { get; set; } = null!;
        public virtual DbSet<Examen> Examen { get; set; } = null!;
        public virtual DbSet<ExamenReactivo> ExamenReactivo { get; set; } = null!;
        public virtual DbSet<ExcelArchivo> ExcelArchivo { get; set; } = null!;
        public virtual DbSet<ExcelError> ExcelError { get; set; } = null!;
        public virtual DbSet<ExcelPolizaCargaMasiva> ExcelPolizaCargaMasiva { get; set; } = null!;
        public virtual DbSet<ExcelPolizaCargaMasivaError> ExcelPolizaCargaMasivaError { get; set; } = null!;
        public virtual DbSet<Expediente> Expediente { get; set; } = null!;
        public virtual DbSet<ExpedienteAdministrativo> ExpedienteAdministrativo { get; set; } = null!;
        public virtual DbSet<ExpedienteAdministrativoMercancia> ExpedienteAdministrativoMercancia { get; set; } = null!;
        public virtual DbSet<ExpedienteAdministrativoViaje> ExpedienteAdministrativoViaje { get; set; } = null!;
        public virtual DbSet<ExpedienteAntecedenteFamiliar> ExpedienteAntecedenteFamiliar { get; set; } = null!;
        public virtual DbSet<ExpedienteAntecedenteNoPatologico> ExpedienteAntecedenteNoPatologico { get; set; } = null!;
        public virtual DbSet<ExpedienteAntecedentePatologico> ExpedienteAntecedentePatologico { get; set; } = null!;
        public virtual DbSet<ExpedienteAntecedenteTratamiento> ExpedienteAntecedenteTratamiento { get; set; } = null!;
        public virtual DbSet<ExpedienteAuxiliar> ExpedienteAuxiliar { get; set; } = null!;
        public virtual DbSet<ExpedienteBitacora> ExpedienteBitacora { get; set; } = null!;
        public virtual DbSet<ExpedienteCampo> ExpedienteCampo { get; set; } = null!;
        public virtual DbSet<ExpedienteCampoValor> ExpedienteCampoValor { get; set; } = null!;
        public virtual DbSet<ExpedienteDatoSocial> ExpedienteDatoSocial { get; set; } = null!;
        public virtual DbSet<ExpedienteDoctor> ExpedienteDoctor { get; set; } = null!;
        public virtual DbSet<ExpedienteEstudio> ExpedienteEstudio { get; set; } = null!;
        public virtual DbSet<ExpedientePacienteInformacion> ExpedientePacienteInformacion { get; set; } = null!;
        public virtual DbSet<ExpedientePadecimiento> ExpedientePadecimiento { get; set; } = null!;
        public virtual DbSet<ExpedienteRecomendaciones> ExpedienteRecomendaciones { get; set; } = null!;
        public virtual DbSet<ExpedienteRecomendacionesGenerales> ExpedienteRecomendacionesGenerales { get; set; } = null!;
        public virtual DbSet<ExpedienteSeccion> ExpedienteSeccion { get; set; } = null!;
        public virtual DbSet<ExpedienteTrackr> ExpedienteTrackr { get; set; } = null!;
        public virtual DbSet<ExpedienteTratamiento> ExpedienteTratamiento { get; set; } = null!;
        public virtual DbSet<Fabricante> Fabricante { get; set; } = null!;
        public virtual DbSet<FactorRh> FactorRh { get; set; } = null!;
        public virtual DbSet<Factura> Factura { get; set; } = null!;
        public virtual DbSet<FacturaConcepto> FacturaConcepto { get; set; } = null!;
        public virtual DbSet<FacturaConceptoImpuesto> FacturaConceptoImpuesto { get; set; } = null!;
        public virtual DbSet<Flujo> Flujo { get; set; } = null!;
        public virtual DbSet<FlujoDetalle> FlujoDetalle { get; set; } = null!;
        public virtual DbSet<FlujoDetalleAplicado> FlujoDetalleAplicado { get; set; } = null!;
        public virtual DbSet<FlujoDetalleAplicadoResponsable> FlujoDetalleAplicadoResponsable { get; set; } = null!;
        public virtual DbSet<FlujoDetalleResponsable> FlujoDetalleResponsable { get; set; } = null!;
        public virtual DbSet<FormaPago> FormaPago { get; set; } = null!;
        public virtual DbSet<Gasto> Gasto { get; set; } = null!;
        public virtual DbSet<GastoConcepto> GastoConcepto { get; set; } = null!;
        public virtual DbSet<Genero> Genero { get; set; } = null!;
        public virtual DbSet<GiroComercial> GiroComercial { get; set; } = null!;
        public virtual DbSet<GrupoActividadCita> GrupoActividadCita { get; set; } = null!;
        public virtual DbSet<GrupoPersona> GrupoPersona { get; set; } = null!;
        public virtual DbSet<GrupoPersonaActividad> GrupoPersonaActividad { get; set; } = null!;
        public virtual DbSet<GrupoSanguineo> GrupoSanguineo { get; set; } = null!;
        public virtual DbSet<Guia> Guia { get; set; } = null!;
        public virtual DbSet<GuiaActividad> GuiaActividad { get; set; } = null!;
        public virtual DbSet<GuiaActividadEvidencia> GuiaActividadEvidencia { get; set; } = null!;
        public virtual DbSet<GuiaElementoTecnica> GuiaElementoTecnica { get; set; } = null!;
        public virtual DbSet<HistorialMovimiento> HistorialMovimiento { get; set; } = null!;
        public virtual DbSet<Horario> Horario { get; set; } = null!;
        public virtual DbSet<Hospital> Hospital { get; set; } = null!;
        public virtual DbSet<HospitalLogotipo> HospitalLogotipo { get; set; } = null!;
        public virtual DbSet<Icono> Icono { get; set; } = null!;
        public virtual DbSet<Impuesto> Impuesto { get; set; } = null!;
        public virtual DbSet<ImpuestoDetalle> ImpuestoDetalle { get; set; } = null!;
        public virtual DbSet<Institucion> Institucion { get; set; } = null!;
        public virtual DbSet<InventarioFisico> InventarioFisico { get; set; } = null!;
        public virtual DbSet<InventarioFisicoAjuste> InventarioFisicoAjuste { get; set; } = null!;
        public virtual DbSet<InventarioFisicoAjusteDetalle> InventarioFisicoAjusteDetalle { get; set; } = null!;
        public virtual DbSet<InventarioFisicoDetalle> InventarioFisicoDetalle { get; set; } = null!;
        public virtual DbSet<Invoice> Invoice { get; set; } = null!;
        public virtual DbSet<Jerarquia> Jerarquia { get; set; } = null!;
        public virtual DbSet<JerarquiaAcceso> JerarquiaAcceso { get; set; } = null!;
        public virtual DbSet<JerarquiaAccesoEstructura> JerarquiaAccesoEstructura { get; set; } = null!;
        public virtual DbSet<JerarquiaAccesoTipoCompania> JerarquiaAccesoTipoCompania { get; set; } = null!;
        public virtual DbSet<JerarquiaColumna> JerarquiaColumna { get; set; } = null!;
        public virtual DbSet<JerarquiaConfiguracion> JerarquiaConfiguracion { get; set; } = null!;
        public virtual DbSet<JerarquiaEstructura> JerarquiaEstructura { get; set; } = null!;
        public virtual DbSet<Kardex> Kardex { get; set; } = null!;
        public virtual DbSet<Lada> Lada { get; set; } = null!;
        public virtual DbSet<Liquidacion> Liquidacion { get; set; } = null!;
        public virtual DbSet<ListaPrecio> ListaPrecio { get; set; } = null!;
        public virtual DbSet<ListaPrecioClinica> ListaPrecioClinica { get; set; } = null!;
        public virtual DbSet<ListaPrecioDetalle> ListaPrecioDetalle { get; set; } = null!;
        public virtual DbSet<Localidad> Localidad { get; set; } = null!;
        public virtual DbSet<Mercado> Mercado { get; set; } = null!;
        public virtual DbSet<MercadoCompania> MercadoCompania { get; set; } = null!;
        public virtual DbSet<MetodoPago> MetodoPago { get; set; } = null!;
        public virtual DbSet<Moneda> Moneda { get; set; } = null!;
        public virtual DbSet<MotivoVisita> MotivoVisita { get; set; } = null!;
        public virtual DbSet<MovimientoEstadoCuenta> MovimientoEstadoCuenta { get; set; } = null!;
        public virtual DbSet<MovimientoMaterial> MovimientoMaterial { get; set; } = null!;
        public virtual DbSet<MovimientoMaterialDetalle> MovimientoMaterialDetalle { get; set; } = null!;
        public virtual DbSet<Municipio> Municipio { get; set; } = null!;
        public virtual DbSet<Necesidad> Necesidad { get; set; } = null!;
        public virtual DbSet<NecesidadPresentacion> NecesidadPresentacion { get; set; } = null!;
        public virtual DbSet<NivelExamen> NivelExamen { get; set; } = null!;
        public virtual DbSet<NotaFlujo> NotaFlujo { get; set; } = null!;
        public virtual DbSet<NotaFlujoDetalle> NotaFlujoDetalle { get; set; } = null!;
        public virtual DbSet<NotaFlujoPago> NotaFlujoPago { get; set; } = null!;
        public virtual DbSet<NotaGasto> NotaGasto { get; set; } = null!;
        public virtual DbSet<NotaGastoDetalle> NotaGastoDetalle { get; set; } = null!;
        public virtual DbSet<NotaVenta> NotaVenta { get; set; } = null!;
        public virtual DbSet<NotaVentaDetalle> NotaVentaDetalle { get; set; } = null!;
        public virtual DbSet<Notificacion> Notificacion { get; set; } = null!;
        public virtual DbSet<NotificacionDoctor> NotificacionDoctor { get; set; } = null!;
        public virtual DbSet<NotificacionUsuario> NotificacionUsuario { get; set; } = null!;
        public virtual DbSet<OpcionVenta> OpcionVenta { get; set; } = null!;
        public virtual DbSet<OrdenCompra> OrdenCompra { get; set; } = null!;
        public virtual DbSet<OrdenCompraDetalle> OrdenCompraDetalle { get; set; } = null!;
        public virtual DbSet<OrdenImagenologia> OrdenImagenologia { get; set; } = null!;
        public virtual DbSet<OrdenLaboratorio> OrdenLaboratorio { get; set; } = null!;
        public virtual DbSet<Paciente> Paciente { get; set; } = null!;
        public virtual DbSet<Pago> Pago { get; set; } = null!;
        public virtual DbSet<Pais> Pais { get; set; } = null!;
        public virtual DbSet<Parentesco> Parentesco { get; set; } = null!;
        public virtual DbSet<Pedido> Pedido { get; set; } = null!;
        public virtual DbSet<PedidoBitacora> PedidoBitacora { get; set; } = null!;
        public virtual DbSet<PedidoPresentacion> PedidoPresentacion { get; set; } = null!;
        public virtual DbSet<Perfil> Perfil { get; set; } = null!;
        public virtual DbSet<Plantilla> Plantilla { get; set; } = null!;
        public virtual DbSet<Poliza> Poliza { get; set; } = null!;
        public virtual DbSet<PolizaAplicada> PolizaAplicada { get; set; } = null!;
        public virtual DbSet<PolizaAplicadaDetalle> PolizaAplicadaDetalle { get; set; } = null!;
        public virtual DbSet<PolizaDetalle> PolizaDetalle { get; set; } = null!;
        public virtual DbSet<PolizaOrigen> PolizaOrigen { get; set; } = null!;
        public virtual DbSet<Presentacion> Presentacion { get; set; } = null!;
        public virtual DbSet<PresentacionArticulo> PresentacionArticulo { get; set; } = null!;
        public virtual DbSet<PresentacionImagen> PresentacionImagen { get; set; } = null!;
        public virtual DbSet<ProgramacionExamen> ProgramacionExamen { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedor { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyecto { get; set; } = null!;
        public virtual DbSet<ProyectoActividad> ProyectoActividad { get; set; } = null!;
        public virtual DbSet<ProyectoActividadEvidencia> ProyectoActividadEvidencia { get; set; } = null!;
        public virtual DbSet<ProyectoActividadEvidenciaArchivo> ProyectoActividadEvidenciaArchivo { get; set; } = null!;
        public virtual DbSet<ProyectoActividadHora> ProyectoActividadHora { get; set; } = null!;
        public virtual DbSet<ProyectoActividadParticipante> ProyectoActividadParticipante { get; set; } = null!;
        public virtual DbSet<ProyectoElementoTecnica> ProyectoElementoTecnica { get; set; } = null!;
        public virtual DbSet<ProyectoEstatus> ProyectoEstatus { get; set; } = null!;
        public virtual DbSet<PuntoVenta> PuntoVenta { get; set; } = null!;
        public virtual DbSet<Reactivo> Reactivo { get; set; } = null!;
        public virtual DbSet<Recepcion> Recepcion { get; set; } = null!;
        public virtual DbSet<RecepcionPresentacion> RecepcionPresentacion { get; set; } = null!;
        public virtual DbSet<Receta> Receta { get; set; } = null!;
        public virtual DbSet<RecetaDetalle> RecetaDetalle { get; set; } = null!;
        public virtual DbSet<Recibo> Recibo { get; set; } = null!;
        public virtual DbSet<ReciboDetalle> ReciboDetalle { get; set; } = null!;
        public virtual DbSet<ReciboEnglobado> ReciboEnglobado { get; set; } = null!;
        public virtual DbSet<ReciboPago> ReciboPago { get; set; } = null!;
        public virtual DbSet<Recordatorio> Recordatorio { get; set; } = null!;
        public virtual DbSet<RegimenFiscal> RegimenFiscal { get; set; } = null!;
        public virtual DbSet<Remision> Remision { get; set; } = null!;
        public virtual DbSet<RemisionPresentacion> RemisionPresentacion { get; set; } = null!;
        public virtual DbSet<RestablecerContrasena> RestablecerContrasena { get; set; } = null!;
        public virtual DbSet<Rol> Rol { get; set; } = null!;
        public virtual DbSet<RolAcceso> RolAcceso { get; set; } = null!;
        public virtual DbSet<SatFormaPago> SatFormaPago { get; set; } = null!;
        public virtual DbSet<SatMetodoPago> SatMetodoPago { get; set; } = null!;
        public virtual DbSet<SatMovimiento> SatMovimiento { get; set; } = null!;
        public virtual DbSet<SatMovimientoConcepto> SatMovimientoConcepto { get; set; } = null!;
        public virtual DbSet<SatProductoServicio> SatProductoServicio { get; set; } = null!;
        public virtual DbSet<SatProductoServicioCartaPorte> SatProductoServicioCartaPorte { get; set; } = null!;
        public virtual DbSet<SatSolicitud> SatSolicitud { get; set; } = null!;
        public virtual DbSet<SatTipoComprobante> SatTipoComprobante { get; set; } = null!;
        public virtual DbSet<SatTipoFactor> SatTipoFactor { get; set; } = null!;
        public virtual DbSet<SatUnidad> SatUnidad { get; set; } = null!;
        public virtual DbSet<ScriptCambio> ScriptCambio { get; set; } = null!;
        public virtual DbSet<Seccion> Seccion { get; set; } = null!;
        public virtual DbSet<SeccionCampo> SeccionCampo { get; set; } = null!;
        public virtual DbSet<Servicio> Servicio { get; set; } = null!;
        public virtual DbSet<SubtipoCuentaContable> SubtipoCuentaContable { get; set; } = null!;
        public virtual DbSet<Tarjeta> Tarjeta { get; set; } = null!;
        public virtual DbSet<TipoAcceso> TipoAcceso { get; set; } = null!;
        public virtual DbSet<TipoActivo> TipoActivo { get; set; } = null!;
        public virtual DbSet<TipoArticulo> TipoArticulo { get; set; } = null!;
        public virtual DbSet<TipoAuxiliar> TipoAuxiliar { get; set; } = null!;
        public virtual DbSet<TipoCambio> TipoCambio { get; set; } = null!;
        public virtual DbSet<TipoCliente> TipoCliente { get; set; } = null!;
        public virtual DbSet<TipoComision> TipoComision { get; set; } = null!;
        public virtual DbSet<TipoComisionDetalle> TipoComisionDetalle { get; set; } = null!;
        public virtual DbSet<TipoCompania> TipoCompania { get; set; } = null!;
        public virtual DbSet<TipoConcepto> TipoConcepto { get; set; } = null!;
        public virtual DbSet<TipoCuentaContable> TipoCuentaContable { get; set; } = null!;
        public virtual DbSet<TipoDescuento> TipoDescuento { get; set; } = null!;
        public virtual DbSet<TipoDescuentoDetalle> TipoDescuentoDetalle { get; set; } = null!;
        public virtual DbSet<TipoDevolucion> TipoDevolucion { get; set; } = null!;
        public virtual DbSet<TipoEmpleado> TipoEmpleado { get; set; } = null!;
        public virtual DbSet<TipoExamen> TipoExamen { get; set; } = null!;
        public virtual DbSet<TipoExpediente> TipoExpediente { get; set; } = null!;
        public virtual DbSet<TipoExpedienteAdministrativo> TipoExpedienteAdministrativo { get; set; } = null!;
        public virtual DbSet<TipoFlujo> TipoFlujo { get; set; } = null!;
        public virtual DbSet<TipoGuia> TipoGuia { get; set; } = null!;
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; } = null!;
        public virtual DbSet<TipoImpuesto> TipoImpuesto { get; set; } = null!;
        public virtual DbSet<TipoMovimientoMaterial> TipoMovimientoMaterial { get; set; } = null!;
        public virtual DbSet<TipoMovimientoMaterialCuentaContable> TipoMovimientoMaterialCuentaContable { get; set; } = null!;
        public virtual DbSet<TipoMuestra> TipoMuestra { get; set; } = null!;
        public virtual DbSet<TipoNotaGasto> TipoNotaGasto { get; set; } = null!;
        public virtual DbSet<TipoNotaVenta> TipoNotaVenta { get; set; } = null!;
        public virtual DbSet<TipoNotificacion> TipoNotificacion { get; set; } = null!;
        public virtual DbSet<TipoPago> TipoPago { get; set; } = null!;
        public virtual DbSet<TipoPermisoTransporte> TipoPermisoTransporte { get; set; } = null!;
        public virtual DbSet<TipoPoliza> TipoPoliza { get; set; } = null!;
        public virtual DbSet<TipoPresentacion> TipoPresentacion { get; set; } = null!;
        public virtual DbSet<TipoProveedor> TipoProveedor { get; set; } = null!;
        public virtual DbSet<TipoPuntoVenta> TipoPuntoVenta { get; set; } = null!;
        public virtual DbSet<TipoRecibo> TipoRecibo { get; set; } = null!;
        public virtual DbSet<TipoRemolque> TipoRemolque { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; } = null!;
        public virtual DbSet<TipoVehiculo> TipoVehiculo { get; set; } = null!;
        public virtual DbSet<TipoVigencia> TipoVigencia { get; set; } = null!;
        public virtual DbSet<TipoWidget> TipoWidget { get; set; } = null!;
        public virtual DbSet<TituloAcademico> TituloAcademico { get; set; } = null!;
        public virtual DbSet<TraspasoMovimientoMaterial> TraspasoMovimientoMaterial { get; set; } = null!;
        public virtual DbSet<TraspasoMovimientoMaterialDetalle> TraspasoMovimientoMaterialDetalle { get; set; } = null!;
        public virtual DbSet<TratamientoRecordatorio> TratamientoRecordatorio { get; set; } = null!;
        public virtual DbSet<TratamientoToma> TratamientoToma { get; set; } = null!;
        public virtual DbSet<Turno> Turno { get; set; } = null!;
        public virtual DbSet<Ubicacion> Ubicacion { get; set; } = null!;
        public virtual DbSet<UbicacionVenta> UbicacionVenta { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; } = null!;
        public virtual DbSet<Urgencia> Urgencia { get; set; } = null!;
        public virtual DbSet<UrgenciaTratamiento> UrgenciaTratamiento { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<UsuarioAlmacen> UsuarioAlmacen { get; set; } = null!;
        public virtual DbSet<UsuarioLocacion> UsuarioLocacion { get; set; } = null!;
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; } = null!;
        public virtual DbSet<UsuarioWidget> UsuarioWidget { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculo { get; set; } = null!;
        public virtual DbSet<VehiculoMantenimiento> VehiculoMantenimiento { get; set; } = null!;
        public virtual DbSet<VersionPoliza> VersionPoliza { get; set; } = null!;
        public virtual DbSet<ViaAdministracion> ViaAdministracion { get; set; } = null!;
        public virtual DbSet<VistaBalanzaComprobacion> VistaBalanzaComprobacion { get; set; } = null!;
        public virtual DbSet<Widget> Widget { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<Acceso>(entity =>
            {
                entity.HasKey(e => e.IdAcceso);

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.Property(e => e.UrlVideoAyuda)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAccesoPadreNavigation)
                    .WithMany(p => p.InverseIdAccesoPadreNavigation)
                    .HasForeignKey(d => d.IdAccesoPadre)
                    .HasConstraintName("FK_Acceso_AccesoPadre");

                entity.HasOne(d => d.IdIconoNavigation)
                    .WithMany(p => p.Acceso)
                    .HasForeignKey(d => d.IdIcono)
                    .HasConstraintName("FK_Acceso_Icono");

                entity.HasOne(d => d.IdRolAccesoNavigation)
                    .WithMany(p => p.Acceso)
                    .HasForeignKey(d => d.IdRolAcceso)
                    .HasConstraintName("FK__Acceso__IdRolAcc__12149A71");

                entity.HasOne(d => d.IdTipoAccesoNavigation)
                    .WithMany(p => p.Acceso)
                    .HasForeignKey(d => d.IdTipoAcceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Acceso_TipoAcceso");
            });

            modelBuilder.Entity<AccesoAyuda>(entity =>
            {
                entity.HasKey(e => e.IdAccesoAyuda)
                    .HasName("PK__AccesoAy__7D89CC876DA35C36");

                entity.Property(e => e.DescripcionAyuda)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EtiquetaCampo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.NombreArchivo).HasMaxLength(500);

                entity.Property(e => e.TipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAccesoNavigation)
                    .WithMany(p => p.AccesoAyuda)
                    .HasForeignKey(d => d.IdAcceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccesoAyu__IdAcc__379B24DB");

                entity.HasOne(d => d.IdAyudaSeccionNavigation)
                    .WithMany(p => p.AccesoAyuda)
                    .HasForeignKey(d => d.IdAyudaSeccion)
                    .HasConstraintName("FK__AccesoAyu__IdAyu__3A779186");
            });

            modelBuilder.Entity<AccesoPerfil>(entity =>
            {
                entity.HasKey(e => e.IdAccesoPerfil);

                entity.HasOne(d => d.IdAccesoNavigation)
                    .WithMany(p => p.AccesoPerfil)
                    .HasForeignKey(d => d.IdAcceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccesoPerfil_Acceso");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.AccesoPerfil)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccesoPerfil_Perfil");
            });

            modelBuilder.Entity<AgrupadorCuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdAgrupadorCuentaContable)
                    .HasName("PK__Agrupado__3356258940ECE24C");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.HasOne(d => d.IdCuentaContableCapitalNavigation)
                    .WithMany(p => p.AgrupadorCuentaContableIdCuentaContableCapitalNavigation)
                    .HasForeignKey(d => d.IdCuentaContableCapital)
                    .HasConstraintName("FK__Agrupador__Cuent__5C77A3CF");

                entity.HasOne(d => d.IdCuentaContableResultadoNavigation)
                    .WithMany(p => p.AgrupadorCuentaContableIdCuentaContableResultadoNavigation)
                    .HasForeignKey(d => d.IdCuentaContableResultado)
                    .HasConstraintName("FK__Agrupador__Cuent__5D6BC808");
            });

            modelBuilder.Entity<Almacen>(entity =>
            {
                entity.HasKey(e => e.IdAlmacen)
                    .HasName("PK_IdAlmacen");

                entity.Property(e => e.Calle).HasMaxLength(100);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.CodigoPostal).HasMaxLength(5);

                entity.Property(e => e.Colonia).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Localidad).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.NumeroExterior).HasMaxLength(6);

                entity.Property(e => e.NumeroInterior).HasMaxLength(6);

                entity.Property(e => e.TelefonoDos).HasMaxLength(15);

                entity.Property(e => e.TelefonoUno).HasMaxLength(15);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Almacen)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Almacen_Compania");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.Almacen)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .HasConstraintName("FK__Almacen__IdCuent__27CED166");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Almacen)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Almacen_Estado");

                entity.HasOne(d => d.IdEstatusAlmacenNavigation)
                    .WithMany(p => p.Almacen)
                    .HasForeignKey(d => d.IdEstatusAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Almacen_EstatusAlmacen");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.Almacen)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Almacen_UsuarioResponsable");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Area__IdCompania__68A8708A");
            });

            modelBuilder.Entity<Artefacto>(entity =>
            {
                entity.HasKey(e => e.IdArtefacto)
                    .HasName("PK__Artefact__A94502DC2123B37A");

                entity.ToTable("Artefacto", "Proyectos");

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.ArchivoNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoTipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.Artefacto)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Artefacto__IdLoc__66E023A9");
            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo);

                entity.Property(e => e.Altura)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ancho)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.CostoEstandar).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Estatus).HasMaxLength(50);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Formula).HasMaxLength(500);

                entity.Property(e => e.Longitud)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(256);

                entity.Property(e => e.Sku).HasMaxLength(50);

                entity.Property(e => e.SustanciaActiva).HasMaxLength(500);

                entity.Property(e => e.Upc).HasMaxLength(50);

                entity.HasOne(d => d.IdArticuloClaseNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdArticuloClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_ArticuloClase");

                entity.HasOne(d => d.IdArticuloPresentacionNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdArticuloPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_ArticuloPresentacion");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.ArticuloIdCategoriaNavigation)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_Categoria");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Articulo__IdComp__10615C29");

                entity.HasOne(d => d.IdFabricanteNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdFabricante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_Fabricante");

                entity.HasOne(d => d.IdSubCategoriaNavigation)
                    .WithMany(p => p.ArticuloIdSubCategoriaNavigation)
                    .HasForeignKey(d => d.IdSubCategoria)
                    .HasConstraintName("FK_Articulo_SubCategoria");

                entity.HasOne(d => d.IdSubSubCategoriaNavigation)
                    .WithMany(p => p.ArticuloIdSubSubCategoriaNavigation)
                    .HasForeignKey(d => d.IdSubSubCategoria)
                    .HasConstraintName("FK_Articulo_SubSubCategoria");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_UnidadMedida");

                entity.HasOne(d => d.IdViaAdministracionNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdViaAdministracion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_ViaAdministracion");
            });

            modelBuilder.Entity<ArticuloClase>(entity =>
            {
                entity.HasKey(e => e.IdArticuloClase);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.ArticuloClase)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__ArticuloC__IdCom__2EE5E349");
            });

            modelBuilder.Entity<ArticuloPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdArticuloPresentacion);

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.ArticuloPresentacion)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__ArticuloP__IdCom__68C86C1B");
            });

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.HasKey(e => e.IdAsignatura)
                    .HasName("PK__Asignatu__94F174B86A0F8150");

                entity.ToTable("Asignatura", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            });

            modelBuilder.Entity<Auxiliar>(entity =>
            {
                entity.HasKey(e => e.IdAuxiliar)
                    .HasName("PK__Auxiliar__F8532C0956C99303");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Numero).HasMaxLength(20);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Auxiliar)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auxiliar__IdComp__3651FAE7");

                entity.HasOne(d => d.IdTipoAuxiliarNavigation)
                    .WithMany(p => p.Auxiliar)
                    .HasForeignKey(d => d.IdTipoAuxiliar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Auxiliar__IdTipo__355DD6AE");
            });

            modelBuilder.Entity<AyudaSeccion>(entity =>
            {
                entity.HasKey(e => e.IdAyudaSeccion)
                    .HasName("PK__AyudaSec__56D54A221972AA0B");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<BalanceCuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdBalanceCuentaContable)
                    .HasName("PK__BalanceC__E8ACB1373AE73213");

                entity.Property(e => e.Abono).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Cargo).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.BalanceCuentaContable)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BalanceCu__IdCom__11DF9047");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.BalanceCuentaContable)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BalanceCu__IdCue__10EB6C0E");
            });

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.IdBanco)
                    .HasName("PK__Banco__2D3F553E346137B5");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BitacoraMovimientoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdBitacoraMovimientoUsuario)
                    .HasName("PK__Bitacora__93A3C124961D0229");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.BitacoraMovimientoUsuario)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BitacoraM__IdLoc__7FABD173");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.BitacoraMovimientoUsuario)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BitacoraM__IdUsu__7EB7AD3A");
            });

            modelBuilder.Entity<Caja>(entity =>
            {
                entity.HasKey(e => e.IdCaja);

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.CajaIdCuentaContableNavigation)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .HasConstraintName("FK__Caja__IdCuentaCo__5B4E756C");

                entity.HasOne(d => d.IdCuentaContableAutomaticaNavigation)
                    .WithMany(p => p.CajaIdCuentaContableAutomaticaNavigation)
                    .HasForeignKey(d => d.IdCuentaContableAutomatica)
                    .HasConstraintName("FK__Caja__IdCuentaCo__48C5B0DD");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Caja)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Caja_Hospital");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.Caja)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK__Caja__IdMoneda__34BEB830");

                entity.HasOne(d => d.IdTipoActivoNavigation)
                    .WithMany(p => p.Caja)
                    .HasForeignKey(d => d.IdTipoActivo)
                    .HasConstraintName("FK__Caja__IdTipoActi__418EA369");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.Caja)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .HasConstraintName("FK_Caja_UsuarioResponsable");
            });

            modelBuilder.Entity<CajaTurno>(entity =>
            {
                entity.HasKey(e => e.IdCajaTurno);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.FondoCaja).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MontoEsperado).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MontoIngresado).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.CajaTurno)
                    .HasForeignKey(d => d.IdCaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CajaTurno_Caja");

                entity.HasOne(d => d.IdCierreNavigation)
                    .WithMany(p => p.CajaTurno)
                    .HasForeignKey(d => d.IdCierre)
                    .HasConstraintName("FK_CajaTurno_Cierre");

                entity.HasOne(d => d.IdTurnoNavigation)
                    .WithMany(p => p.CajaTurno)
                    .HasForeignKey(d => d.IdTurno)
                    .HasConstraintName("FK_CajaTurno_Turno");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CajaTurno)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_CajaTurno_Usuario");
            });

            modelBuilder.Entity<CajaTurnoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdCajaTurnoDetalle);

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdCajaTurnoNavigation)
                    .WithMany(p => p.CajaTurnoDetalle)
                    .HasForeignKey(d => d.IdCajaTurno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CajaTurnoDetalle_CajaTurno");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.CajaTurnoDetalle)
                    .HasForeignKey(d => d.IdFormaPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CajaTurnoDetalle_FormaPago");
            });

            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.HasKey(e => e.IdCarrito);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Comentarios).HasMaxLength(500);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Token).HasMaxLength(2000);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Carrito__IdCompa__7B1C2680");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrito_Presentacion");

                entity.HasOne(d => d.IdUsuarioCompradorNavigation)
                    .WithMany(p => p.Carrito)
                    .HasForeignKey(d => d.IdUsuarioComprador)
                    .HasConstraintName("FK_Carrito_UsuarioComprador");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCategoriaPadreNavigation)
                    .WithMany(p => p.InverseIdCategoriaPadreNavigation)
                    .HasForeignKey(d => d.IdCategoriaPadre)
                    .HasConstraintName("FK_Categoria_CategoriaPadre");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Categoria)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK_Categoria_Compania");
            });

            modelBuilder.Entity<CategoriaImagen>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaImagen)
                    .HasName("PK__Categori__BD46940E000B2429");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.NombreArchivo).HasMaxLength(500);

                entity.Property(e => e.TipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.CategoriaImagen)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoriaImagen_Categoria");
            });

            modelBuilder.Entity<CertificadoLocacion>(entity =>
            {
                entity.HasKey(e => e.IdCertificadoLocacion)
                    .HasName("PK__Certific__AB281D138CF9FB25");

                entity.Property(e => e.Contrasena).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.Property(e => e.TipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.CertificadoLocacion)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Certifica__IdLoc__615C547D");
            });

            modelBuilder.Entity<Cie>(entity =>
            {
                entity.HasKey(e => e.IdCie);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<Cierre>(entity =>
            {
                entity.HasKey(e => e.IdCierre);

                entity.Property(e => e.FechaCierre).HasColumnType("datetime");

                entity.Property(e => e.FechaContable).HasColumnType("date");
            });

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.IdCita);

                entity.Property(e => e.Diagnostico).HasMaxLength(500);

                entity.Property(e => e.EvaluacionMedica).HasMaxLength(500);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaCita).HasColumnType("datetime");

                entity.Property(e => e.FechaTomaSomatometria).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.Property(e => e.OtroMotivoVisita).HasMaxLength(500);

                entity.Property(e => e.Referencia).HasMaxLength(500);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_Cita_Area");

                entity.HasOne(d => d.IdCieNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdCie)
                    .HasConstraintName("FK_Cita_Cie");

                entity.HasOne(d => d.IdEstatusCitaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdEstatusCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_EstatusCita");

                entity.HasOne(d => d.IdEstatusPagoNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdEstatusPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_EstatusPago");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_Expediente");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_Hospital");

                entity.HasOne(d => d.IdMotivoVisitaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdMotivoVisita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_MotivoVisita");

                entity.HasOne(d => d.IdNotaVentaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdNotaVenta)
                    .HasConstraintName("FK__Cita__IdNotaVent__22B5168E");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK_PresentacionCita");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdRecibo)
                    .HasConstraintName("FK_Recibo_Cita");

                entity.HasOne(d => d.IdUsuarioDoctorNavigation)
                    .WithMany(p => p.CitaIdUsuarioDoctorNavigation)
                    .HasForeignKey(d => d.IdUsuarioDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cita_UsuarioDoctor");

                entity.HasOne(d => d.IdUsuarioTomaSomatometriaNavigation)
                    .WithMany(p => p.CitaIdUsuarioTomaSomatometriaNavigation)
                    .HasForeignKey(d => d.IdUsuarioTomaSomatometria)
                    .HasConstraintName("FK__Cita__IdUsuarioT__2D32A501");
            });

            modelBuilder.Entity<CitaArchivo>(entity =>
            {
                entity.HasKey(e => e.IdCitaArchivo)
                    .HasName("PK__CitaArch__7F3593B8813B1A44");

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.TipoMime).HasMaxLength(200);

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.CitaArchivo)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CitaArchi__IdCit__0A096455");
            });

            modelBuilder.Entity<CitaGrupoPersona>(entity =>
            {
                entity.HasKey(e => e.IdCitaGrupoPersona);

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.CitaGrupoPersona)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CitaGrupoPersona_Cita");

                entity.HasOne(d => d.IdGrupoPersonaNavigation)
                    .WithMany(p => p.CitaGrupoPersona)
                    .HasForeignKey(d => d.IdGrupoPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CitaGrupoPersona_GrupoPersona");
            });

            modelBuilder.Entity<CobranzaPago>(entity =>
            {
                entity.HasKey(e => e.IdCobranza)
                    .HasName("PK__Cobranza__9E1409EA17B275B6");

                entity.Property(e => e.IdCobranza).HasColumnName("idCobranza");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdLiquidacionNavigation)
                    .WithMany(p => p.CobranzaPago)
                    .HasForeignKey(d => d.IdLiquidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CobranzaP__idLiq__0F382DC6");

                entity.HasOne(d => d.IdRemisionNavigation)
                    .WithMany(p => p.CobranzaPago)
                    .HasForeignKey(d => d.IdRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CobranzaP__idRem__0E44098D");
            });

            modelBuilder.Entity<CodigoPostal>(entity =>
            {
                entity.HasKey(e => e.IdCodigoPostal);

                entity.Property(e => e.CodigoPostal1)
                    .HasMaxLength(5)
                    .HasColumnName("CodigoPostal");

                entity.Property(e => e.Colonia).HasMaxLength(200);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.CodigoPostal)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodigoPostal_Municipio");
            });

            modelBuilder.Entity<Colonia>(entity =>
            {
                entity.HasKey(e => e.IdColonia)
                    .HasName("PK__Colonia__A1580F66D170D8D7");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.CodigoPostal).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<Comision>(entity =>
            {
                entity.HasKey(e => e.IdComision)
                    .HasName("PK_Comision_1");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.MontoComision).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Comision)
                    .HasForeignKey(d => d.IdHospital)
                    .HasConstraintName("FK__Comision__IdHosp__4DBF7024");

                entity.HasOne(d => d.IdNotaVentaDetalleNavigation)
                    .WithMany(p => p.Comision)
                    .HasForeignKey(d => d.IdNotaVentaDetalle)
                    .HasConstraintName("FK_Comision_NotaVentaDetalle");

                entity.HasOne(d => d.IdTipoComisionDetalleNavigation)
                    .WithMany(p => p.Comision)
                    .HasForeignKey(d => d.IdTipoComisionDetalle)
                    .HasConstraintName("FK_Comision_TipoComisionDetalle");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comision)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comision_Usuario");
            });

            modelBuilder.Entity<Compania>(entity =>
            {
                entity.HasKey(e => e.IdCompania);

                entity.Property(e => e.Calle).HasMaxLength(50);

                entity.Property(e => e.Ciudad).HasMaxLength(50);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.CodigoPostal).HasMaxLength(50);

                entity.Property(e => e.Colonia).HasMaxLength(50);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NumeroExterior).HasMaxLength(50);

                entity.Property(e => e.NumeroInterior).HasMaxLength(50);

                entity.Property(e => e.PortalWeb).HasMaxLength(50);

                entity.Property(e => e.Rfc).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.HasOne(d => d.IdAgrupadorCuentaContableNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdAgrupadorCuentaContable)
                    .HasConstraintName("FK__Compania__IdAgru__1C5D1EBA");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Compania_Estado");

                entity.HasOne(d => d.IdGiroComercialNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdGiroComercial)
                    .HasConstraintName("FK__Compania__IdGiro__3671F678");

                entity.HasOne(d => d.IdLadaNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdLada)
                    .HasConstraintName("FK_Compania_Lada");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK__Compania__IdMone__1980B20F");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Compania__IdMuni__7A280247");

                entity.HasOne(d => d.IdRegimenFiscalNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdRegimenFiscal)
                    .HasConstraintName("FK_Compania_RegimenFiscal");

                entity.HasOne(d => d.IdTipoCompaniaNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdTipoCompania)
                    .HasConstraintName("FK__Compania__IdTipo__77B5A9F0");
            });

            modelBuilder.Entity<CompaniaContacto>(entity =>
            {
                entity.HasKey(e => e.IdCompaniaContacto)
                    .HasName("PK__Compania__3302ACF05D901013");

                entity.Property(e => e.ApellidoMaterno).HasMaxLength(200);

                entity.Property(e => e.ApellidoPaterno).HasMaxLength(200);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.TelefonoMovil).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.CompaniaContacto)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompaniaC__IdCom__6BD9E2F0");

                entity.HasOne(d => d.IdLadaNavigation)
                    .WithMany(p => p.CompaniaContacto)
                    .HasForeignKey(d => d.IdLada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompaniaC__IdLad__6CCE0729");
            });

            modelBuilder.Entity<CompaniaLogotipo>(entity =>
            {
                entity.HasKey(e => e.IdCompaniaLogotipo)
                    .HasName("PK__Compania__490D2A580FA3CBC1");

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.CompaniaLogotipo)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompaniaL__IdCom__76577163");
            });

            modelBuilder.Entity<ComplementoPago>(entity =>
            {
                entity.HasKey(e => e.IdComplementoPago)
                    .HasName("PK__Compleme__EF27A5BC14BAAF32");

                entity.Property(e => e.DescripcionError).HasMaxLength(500);

                entity.Property(e => e.FechaCancelacion).HasColumnType("datetime");

                entity.Property(e => e.FechaComplemento).HasColumnType("datetime");

                entity.Property(e => e.FechaSellado).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(500);

                entity.Property(e => e.LugarExpedicion).HasMaxLength(100);

                entity.Property(e => e.MontoPago).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.NombreEmisor).HasMaxLength(60);

                entity.Property(e => e.NombreReceptor).HasMaxLength(100);

                entity.Property(e => e.NumeroOperacion).HasMaxLength(50);

                entity.Property(e => e.RfcEmisor).HasMaxLength(15);

                entity.Property(e => e.RfcReceptor).HasMaxLength(15);

                entity.Property(e => e.SelloCancelacion).HasMaxLength(500);

                entity.Property(e => e.Subtotal).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.UsoCfdi).HasMaxLength(500);

                entity.Property(e => e.Uuid).HasMaxLength(150);

                entity.Property(e => e.Version).HasMaxLength(5);

                entity.Property(e => e.VersionPago).HasMaxLength(5);

                entity.HasOne(d => d.IdEstatusComplementoNavigation)
                    .WithMany(p => p.ComplementoPago)
                    .HasForeignKey(d => d.IdEstatusComplemento)
                    .HasConstraintName("FK__Complemen__IdEst__7C4554E3");

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.ComplementoPago)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdLoc__7D39791C");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.ComplementoPagoIdMonedaNavigation)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdMon__70D3A237");

                entity.HasOne(d => d.IdMonedaDrNavigation)
                    .WithMany(p => p.ComplementoPagoIdMonedaDrNavigation)
                    .HasForeignKey(d => d.IdMonedaDr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdMon__73B00EE2");

                entity.HasOne(d => d.IdNotaFlujoNavigation)
                    .WithMany(p => p.ComplementoPago)
                    .HasForeignKey(d => d.IdNotaFlujo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdNot__75985754");

                entity.HasOne(d => d.IdRegimenFiscalEmisorNavigation)
                    .WithMany(p => p.ComplementoPago)
                    .HasForeignKey(d => d.IdRegimenFiscalEmisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdReg__72BBEAA9");

                entity.HasOne(d => d.IdUsuarioClienteNavigation)
                    .WithMany(p => p.ComplementoPago)
                    .HasForeignKey(d => d.IdUsuarioCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdUsu__74A4331B");
            });

            modelBuilder.Entity<ComplementoPagoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdComplementoPagoDetalle)
                    .HasName("PK__Compleme__BD43ED18FF048BDD");

                entity.Property(e => e.MontoPagado).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SaldoAnterior).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SaldoInsoluto).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.IdComplementoPagoNavigation)
                    .WithMany(p => p.ComplementoPagoDetalle)
                    .HasForeignKey(d => d.IdComplementoPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdCom__7968E838");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.ComplementoPagoDetalle)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK__Complemen__IdMon__7874C3FF");

                entity.HasOne(d => d.IdNotaFlujoDetalleNavigation)
                    .WithMany(p => p.ComplementoPagoDetalle)
                    .HasForeignKey(d => d.IdNotaFlujoDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Complemen__IdNot__7B5130AA");
            });

            modelBuilder.Entity<Concepto>(entity =>
            {
                entity.HasKey(e => e.IdConcepto);

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.TipoMovimiento).HasMaxLength(1);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Concepto)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Concepto__IdComp__292D09F3");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.Concepto)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .HasConstraintName("FK__Concepto__IdCuen__13FCE2E3");

                entity.HasOne(d => d.IdSatProductoServicioNavigation)
                    .WithMany(p => p.Concepto)
                    .HasForeignKey(d => d.IdSatProductoServicio)
                    .HasConstraintName("FK__Concepto__IdSatP__194BA7E5");

                entity.HasOne(d => d.IdSatUnidadNavigation)
                    .WithMany(p => p.Concepto)
                    .HasForeignKey(d => d.IdSatUnidad)
                    .HasConstraintName("FK__Concepto__IdSatU__1A3FCC1E");

                entity.HasOne(d => d.IdTipoAuxiliarNavigation)
                    .WithMany(p => p.Concepto)
                    .HasForeignKey(d => d.IdTipoAuxiliar)
                    .HasConstraintName("FK__Concepto__IdTipo__49B9D516");
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracion);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Valor).HasMaxLength(4000);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Configuracion)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Configura__IdCom__188C8DD6");
            });

            modelBuilder.Entity<ConfiguracionAutotransporte>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionAutotransporte)
                    .HasName("PK__Configur__A7AC5EC96810A884");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.NumeroEjes).HasMaxLength(10);

                entity.Property(e => e.NumeroLlantas).HasMaxLength(10);
            });

            modelBuilder.Entity<ConfiguracionConcepto>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionConcepto)
                    .HasName("PK__Configur__738C60796A7C084F");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.ConfiguracionConcepto)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdCon__2DDCB077");

                entity.HasOne(d => d.IdTipoConceptoNavigation)
                    .WithMany(p => p.ConfiguracionConcepto)
                    .HasForeignKey(d => d.IdTipoConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdTip__2FC4F8E9");
            });

            modelBuilder.Entity<ConfiguracionOpcionVenta>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionOpcionVenta)
                    .HasName("PK__Configur__9D4216BD1D8AA798");

                entity.HasOne(d => d.IdFlujoNavigation)
                    .WithMany(p => p.ConfiguracionOpcionVenta)
                    .HasForeignKey(d => d.IdFlujo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdFlu__737B04B8");

                entity.HasOne(d => d.IdOpcionVentaNavigation)
                    .WithMany(p => p.ConfiguracionOpcionVenta)
                    .HasForeignKey(d => d.IdOpcionVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdOpc__7286E07F");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ConfiguracionOpcionVenta)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdPre__7192BC46");
            });

            modelBuilder.Entity<ConfiguracionVigencia>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionVigencia)
                    .HasName("PK__Configur__4843BEDB7E81F34F");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.ConfiguracionVigencia)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdCom__68FD7645");

                entity.HasOne(d => d.IdTipoVigenciaNavigation)
                    .WithMany(p => p.ConfiguracionVigencia)
                    .HasForeignKey(d => d.IdTipoVigencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Configura__IdTip__6809520C");
            });

            modelBuilder.Entity<ContenidoExamen>(entity =>
            {
                entity.HasKey(e => e.IdContenidoExamen)
                    .HasName("PK__Contenid__8F9AD9BE1293DADD");

                entity.ToTable("ContenidoExamen", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.HasOne(d => d.IdAsignaturaNavigation)
                    .WithMany(p => p.ContenidoExamen)
                    .HasForeignKey(d => d.IdAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contenido__IdAsi__4FFCBE51");

                entity.HasOne(d => d.IdNivelExamenNavigation)
                    .WithMany(p => p.ContenidoExamen)
                    .HasForeignKey(d => d.IdNivelExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contenido__IdNiv__50F0E28A");

                entity.HasOne(d => d.IdTipoExamenNavigation)
                    .WithMany(p => p.ContenidoExamen)
                    .HasForeignKey(d => d.IdTipoExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contenido__IdTip__4F089A18");
            });

            modelBuilder.Entity<CuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdCuentaContable);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Numero).HasMaxLength(20);

                entity.HasOne(d => d.IdAgrupadorCuentaContableNavigation)
                    .WithMany(p => p.CuentaContable)
                    .HasForeignKey(d => d.IdAgrupadorCuentaContable)
                    .HasConstraintName("FK__CuentaCon__IdAgr__1D5142F3");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.CuentaContable)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__CuentaCon__IdCom__2A212E2C");

                entity.HasOne(d => d.IdCuentaContablePadreNavigation)
                    .WithMany(p => p.InverseIdCuentaContablePadreNavigation)
                    .HasForeignKey(d => d.IdCuentaContablePadre)
                    .HasConstraintName("FK__CuentaCon__IdCue__20B7BF83");

                entity.HasOne(d => d.IdSubtipoCuentaContableNavigation)
                    .WithMany(p => p.CuentaContable)
                    .HasForeignKey(d => d.IdSubtipoCuentaContable)
                    .HasConstraintName("FK_CuentaContable_SubtipoCuentaContable");

                entity.HasOne(d => d.IdTipoAuxiliarNavigation)
                    .WithMany(p => p.CuentaContable)
                    .HasForeignKey(d => d.IdTipoAuxiliar)
                    .HasConstraintName("FK__CuentaCon__IdTip__37461F20");

                entity.HasOne(d => d.IdTipoCuentaContableNavigation)
                    .WithMany(p => p.CuentaContable)
                    .HasForeignKey(d => d.IdTipoCuentaContable)
                    .HasConstraintName("FK_CuentaContable_TipoCuentaContable");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK_Deparamento");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Departamento)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_Departamento_Area");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Departamento)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Departame__IdCom__2B155265");
            });

            modelBuilder.Entity<Deposito>(entity =>
            {
                entity.HasKey(e => e.IdDeposito)
                    .HasName("PK__Deposito__011A5BF22F798345");

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.ArchivoTipoMime).HasMaxLength(200);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.Deposito)
                    .HasForeignKey(d => d.IdFormaPago)
                    .HasConstraintName("FK__Deposito__IdForm__3B16B004");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.Deposito)
                    .HasForeignKey(d => d.IdPago)
                    .HasConstraintName("FK__Deposito__IdPago__3C0AD43D");
            });

            modelBuilder.Entity<DetalleExpedienteRecomendacionesGenerales>(entity =>
            {
                entity.HasKey(e => e.IdDetalleExpedienteRecomendacionesGenerales)
                    .HasName("PK__DetalleE__126B70BEEF131F3C");

                entity.ToTable("DetalleExpedienteRecomendacionesGenerales", "Trackr");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.DetalleExpedienteRecomendacionesGenerales)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleEx__IdExp__75ED5D0F");

                entity.HasOne(d => d.IdExpedienteRecomendacionesGeneralesNavigation)
                    .WithMany(p => p.DetalleExpedienteRecomendacionesGenerales)
                    .HasForeignKey(d => d.IdExpedienteRecomendacionesGenerales)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleEx__IdExp__77D5A581");

                entity.HasOne(d => d.IdNotificacionNavigation)
                    .WithMany(p => p.DetalleExpedienteRecomendacionesGenerales)
                    .HasForeignKey(d => d.IdNotificacion)
                    .HasConstraintName("FK__DetalleEx__IdNot__76E18148");
            });

            modelBuilder.Entity<Devolucion>(entity =>
            {
                entity.HasKey(e => e.IdDevolucion)
                    .HasName("PK__Devoluci__BFAF069AFE98F1CA");

                entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.FechaDevolucion)
                    .HasColumnType("date")
                    .HasColumnName("fechaDevolucion");

                entity.Property(e => e.Folio)
                    .HasMaxLength(35)
                    .HasColumnName("folio");

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.IdDomicilioSucursal).HasColumnName("idDomicilioSucursal");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.Property(e => e.IdTipoDevolucion).HasColumnName("idTipoDevolucion");

                entity.HasOne(d => d.IdDomicilioSucursalNavigation)
                    .WithMany(p => p.Devolucion)
                    .HasForeignKey(d => d.IdDomicilioSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Devolucio__idDom__114071C9");

                entity.HasOne(d => d.IdLiquidacionNavigation)
                    .WithMany(p => p.Devolucion)
                    .HasForeignKey(d => d.IdLiquidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Devolucio__idLiq__7948ECA7");

                entity.HasOne(d => d.IdTipoDevolucionNavigation)
                    .WithMany(p => p.Devolucion)
                    .HasForeignKey(d => d.IdTipoDevolucion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Devolucio__idTip__7A3D10E0");
            });

            modelBuilder.Entity<DevolucionPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdDevolucionPresentacion)
                    .HasName("PK__Devoluci__1BAC25D0B308A645");

                entity.Property(e => e.IdDevolucionPresentacion).HasColumnName("idDevolucionPresentacion");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");

                entity.Property(e => e.IdPresentacion).HasColumnName("idPresentacion");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdDevolucionNavigation)
                    .WithMany(p => p.DevolucionPresentacion)
                    .HasForeignKey(d => d.IdDevolucion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Devolucio__idDev__7E0DA1C4");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.DevolucionPresentacion)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Devolucio__idPre__7F01C5FD");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__B49878C9BA481958");

                entity.Property(e => e.Calle).HasMaxLength(100);

                entity.Property(e => e.CodigoPostal).HasMaxLength(6);

                entity.Property(e => e.Colonia).HasMaxLength(100);

                entity.Property(e => e.EntreCalles).HasMaxLength(150);

                entity.Property(e => e.Latitud).HasMaxLength(50);

                entity.Property(e => e.Longitud).HasMaxLength(50);

                entity.Property(e => e.NumeroExterior).HasMaxLength(6);

                entity.Property(e => e.NumeroInterior).HasMaxLength(6);

                entity.Property(e => e.OtraReferencia).HasMaxLength(150);

                entity.Property(e => e.Recibe).HasMaxLength(200);

                entity.Property(e => e.Telefono).HasMaxLength(15);

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Direccion)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK_Direccion_Ciudad");
            });

            modelBuilder.Entity<DistributedLocks>(entity =>
            {
                entity.HasKey(e => e.IdDistributedLocks)
                    .HasName("PK__Distribu__D1580D1FFDB7EFE8");

                entity.ToTable("DistributedLocks", "Trackr");

                entity.HasIndex(e => e.Resource, "UC_DistributedLocks_Resource")
                    .IsUnique();

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Resource).HasMaxLength(255);
            });

            modelBuilder.Entity<Domicilio>(entity =>
            {
                entity.HasKey(e => e.IdDomicilio);

                entity.Property(e => e.Calle).HasMaxLength(100);

                entity.Property(e => e.CodigoPostal).HasMaxLength(5);

                entity.Property(e => e.Colonia).HasMaxLength(50);

                entity.Property(e => e.EntreCalles).HasMaxLength(150);

                entity.Property(e => e.Latitud).HasMaxLength(50);

                entity.Property(e => e.Localidad).HasMaxLength(50);

                entity.Property(e => e.Longitud).HasMaxLength(50);

                entity.Property(e => e.NombreSucursal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroExterior).HasMaxLength(6);

                entity.Property(e => e.NumeroInterior).HasMaxLength(6);

                entity.Property(e => e.OtraReferencia).HasMaxLength(150);

                entity.HasOne(d => d.IdColoniaNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdColonia)
                    .HasConstraintName("FK__Domicilio__IdCol__0603C947");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Domicilio__IdCom__2DF1BF10");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Domicilio_Estado");

                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdLocalidad)
                    .HasConstraintName("FK__Domicilio__IdLoc__06F7ED80");

                entity.HasOne(d => d.IdMetodoPagoNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdMetodoPago)
                    .HasConstraintName("FK__Domicilio__IdMet__1BBE003C");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Domicilio__IdMun__050FA50E");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Domicilio)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK__Domicilio__IdPai__1ECF7711");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.DomicilioIdUsuarioNavigation)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Domicilio__IdUsu__267098D9");

                entity.HasOne(d => d.IdUsuarioRepartidorNavigation)
                    .WithMany(p => p.DomicilioIdUsuarioRepartidorNavigation)
                    .HasForeignKey(d => d.IdUsuarioRepartidor)
                    .HasConstraintName("FK__Domicilio__IdUsu__151102AD");
            });

            modelBuilder.Entity<Dominio>(entity =>
            {
                entity.HasKey(e => e.IdDominio);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaMaxima).HasColumnType("date");

                entity.Property(e => e.FechaMinima).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.TipoCampo).HasMaxLength(50);

                entity.Property(e => e.TipoDato).HasMaxLength(50);

                entity.Property(e => e.ValorMaximo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ValorMinimo).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<DominioDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDominioDetalle);

                entity.Property(e => e.Valor).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.DominioDetalle)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__DominioDe__IdCom__44A01A3E");

                entity.HasOne(d => d.IdDominioNavigation)
                    .WithMany(p => p.DominioDetalle)
                    .HasForeignKey(d => d.IdDominio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DominioDetalle_Dominio");
            });

            modelBuilder.Entity<Enfermedad>(entity =>
            {
                entity.HasKey(e => e.IdEnfermedad);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Entidad>(entity =>
            {
                entity.HasKey(e => e.IdEntidad)
                    .HasName("PK__Entidad__7D6628682E7BC423");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EntidadEstructura>(entity =>
            {
                entity.HasKey(e => e.IdEntidadEstructura)
                    .HasName("PK__EntidadE__319BD1B0BD0852C9");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdEntidadNavigation)
                    .WithMany(p => p.EntidadEstructura)
                    .HasForeignKey(d => d.IdEntidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EntidadEs__IdEnt__76226739");

                entity.HasOne(d => d.IdEntidadEstructuraPadreNavigation)
                    .WithMany(p => p.InverseIdEntidadEstructuraPadreNavigation)
                    .HasForeignKey(d => d.IdEntidadEstructuraPadre)
                    .HasConstraintName("FK__EntidadEs__IdEnt__780AAFAB");

                entity.HasOne(d => d.IdIconoNavigation)
                    .WithMany(p => p.EntidadEstructura)
                    .HasForeignKey(d => d.IdIcono)
                    .HasConstraintName("FK_EntidadEstructura_Icono");

                entity.HasOne(d => d.IdSeccionNavigation)
                    .WithMany(p => p.EntidadEstructura)
                    .HasForeignKey(d => d.IdSeccion)
                    .HasConstraintName("FK__EntidadEs__IdSec__77168B72");

                entity.HasOne(d => d.IdTipoWidgetNavigation)
                    .WithMany(p => p.EntidadEstructura)
                    .HasForeignKey(d => d.IdTipoWidget)
                    .HasConstraintName("FK_EntidadEstructura_TipoWidget");
            });

            modelBuilder.Entity<EntidadEstructuraTablaValor>(entity =>
            {
                entity.HasKey(e => e.IdEntidadEstructuraTablaValor)
                    .HasName("PK__EntidadE__7D213961FDB1973E");

                entity.Property(e => e.ClaveCampo).HasMaxLength(50);

                entity.Property(e => e.FechaMuestra).HasColumnType("datetime");

                entity.HasOne(d => d.IdEntidadEstructuraNavigation)
                    .WithMany(p => p.EntidadEstructuraTablaValor)
                    .HasForeignKey(d => d.IdEntidadEstructura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EntidadEs__IdEnt__09353BAD");
            });

            modelBuilder.Entity<EntidadEstructuraValor>(entity =>
            {
                entity.HasKey(e => e.IdEntidadEstructuraValor)
                    .HasName("PK__EntidadE__C42BD41247311D8D");

                entity.Property(e => e.ClaveCampo).HasMaxLength(50);

                entity.HasOne(d => d.IdEntidadEstructuraNavigation)
                    .WithMany(p => p.EntidadEstructuraValor)
                    .HasForeignKey(d => d.IdEntidadEstructura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EntidadEs__IdEnt__7AE71C56");
            });

            modelBuilder.Entity<EntradaPersonal>(entity =>
            {
                entity.HasKey(e => e.IdEntradaPersonal);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaBaja).HasColumnType("datetime");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.EntradaPersonal)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntradaPersonal_Hospital");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.EntradaPersonalIdUsuarioNavigation)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntradaPersonal_Usuario");

                entity.HasOne(d => d.IdUsuarioBajaNavigation)
                    .WithMany(p => p.EntradaPersonalIdUsuarioBajaNavigation)
                    .HasForeignKey(d => d.IdUsuarioBaja)
                    .HasConstraintName("FK_EntradaPersonal_UsuarioBaja");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad)
                    .HasName("PK__Especial__693FA0AFC5C935E0");

                entity.ToTable("Especialidad", "Trackr");

                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estado)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pais_Estado");
            });

            modelBuilder.Entity<EstadoCivil>(entity =>
            {
                entity.HasKey(e => e.IdEstadoCivil);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstadoProducto>(entity =>
            {
                entity.HasKey(e => e.IdEstadoProducto)
                    .HasName("PK__EstadoPr__C7C0DA9DC38EECFC");

                entity.Property(e => e.IdEstadoProducto).HasColumnName("idEstadoProducto");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<EstatusAlmacen>(entity =>
            {
                entity.HasKey(e => e.IdEstatusAlmacen);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusCita>(entity =>
            {
                entity.HasKey(e => e.IdEstatusCita);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusEstudioImagenologia>(entity =>
            {
                entity.HasKey(e => e.IdEstatusEstudioImagenologia);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusEstudioLaboratorio>(entity =>
            {
                entity.HasKey(e => e.IdEstatusEstudioLaboratorio);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusExamen>(entity =>
            {
                entity.HasKey(e => e.IdEstatusExamen)
                    .HasName("PK__EstatusE__B0AF3FCDEAA542C8");

                entity.ToTable("EstatusExamen", "Proyectos");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstatusFactura>(entity =>
            {
                entity.HasKey(e => e.IdEstatusFactura)
                    .HasName("PK__EstatusF__CFF444FBF0628D8D");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusInventarioFisico>(entity =>
            {
                entity.HasKey(e => e.IdEstatusInventarioFisico);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusLiquidacion>(entity =>
            {
                entity.HasKey(e => e.IdEstatusLiquidacion)
                    .HasName("PK__EstatusL__C7F813C372898F8B");

                entity.Property(e => e.IdEstatusLiquidacion).HasColumnName("idEstatusLiquidacion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<EstatusMovimientoMaterial>(entity =>
            {
                entity.HasKey(e => e.IdEstatusMovimientoMaterial);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusNotaFlujo>(entity =>
            {
                entity.HasKey(e => e.IdEstatusNotaFlujo)
                    .HasName("PK__EstatusN__BFC06B1882D651E6");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusNotaGasto>(entity =>
            {
                entity.HasKey(e => e.IdEstatusNotaGasto)
                    .HasName("PK__EstatusN__717E102CC4E8A62F");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusNotaVenta>(entity =>
            {
                entity.HasKey(e => e.IdEstatusNotaVenta)
                    .HasName("PK__EstatusN__55E9D6CD7A82C962");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusOrdenCompra>(entity =>
            {
                entity.HasKey(e => e.IdEstatusOrdenCompra);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusPaciente>(entity =>
            {
                entity.HasKey(e => e.IdEstatusPaciente);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusPago>(entity =>
            {
                entity.HasKey(e => e.IdEstatusPago);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusPedido>(entity =>
            {
                entity.HasKey(e => e.IdEstatusPedido);

                entity.Property(e => e.Clave).HasMaxLength(2);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EstatusRemision>(entity =>
            {
                entity.HasKey(e => e.IdEstatusRemision)
                    .HasName("PK__EstatusR__262B74FF7E110BEB");

                entity.Property(e => e.IdEstatusRemision).HasColumnName("idEstatusRemision");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<EstudioImagenologia>(entity =>
            {
                entity.HasKey(e => e.IdEstudioImagenologia);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdEstatusEstudioImagenologiaNavigation)
                    .WithMany(p => p.EstudioImagenologia)
                    .HasForeignKey(d => d.IdEstatusEstudioImagenologia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstudioImagenologia_EstatusEstudioImagenologia");

                entity.HasOne(d => d.IdOrdenImagenologiaNavigation)
                    .WithMany(p => p.EstudioImagenologia)
                    .HasForeignKey(d => d.IdOrdenImagenologia)
                    .HasConstraintName("FK_EstudioImagenologia_OrdenImagenologia");
            });

            modelBuilder.Entity<EstudioImagenologiaArchivo>(entity =>
            {
                entity.HasKey(e => e.IdEstudioImagenologiaArchivo);

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.Property(e => e.TipoMime).HasMaxLength(50);
            });

            modelBuilder.Entity<EstudioLaboratorio>(entity =>
            {
                entity.HasKey(e => e.IdEstudioLaboratorio);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstatusEstudioLaboratorioNavigation)
                    .WithMany(p => p.EstudioLaboratorio)
                    .HasForeignKey(d => d.IdEstatusEstudioLaboratorio)
                    .HasConstraintName("FK_EstudioLaboratorio_EstatusEstudioLaboratorio");

                entity.HasOne(d => d.IdOrdenLaboratorioNavigation)
                    .WithMany(p => p.EstudioLaboratorio)
                    .HasForeignKey(d => d.IdOrdenLaboratorio)
                    .HasConstraintName("FK_EstudioLaboratorio_OrdenLaboratorio");
            });

            modelBuilder.Entity<EstudioLaboratorioArchivo>(entity =>
            {
                entity.HasKey(e => e.IdEstudioLaboratorioArchivo);

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMime).HasMaxLength(50);

                entity.HasOne(d => d.IdEstudioLaboratorioNavigation)
                    .WithMany(p => p.EstudioLaboratorioArchivo)
                    .HasForeignKey(d => d.IdEstudioLaboratorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstudioLaboratorio");
            });

            modelBuilder.Entity<EstudioLaboratorioMuestra>(entity =>
            {
                entity.HasKey(e => e.IdEstudioLaboratorioMuestra);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.HasOne(d => d.IdEstudioLaboratorioNavigation)
                    .WithMany(p => p.EstudioLaboratorioMuestra)
                    .HasForeignKey(d => d.IdEstudioLaboratorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstudioLaboratorioMuestra_EstudioLaboratorio");

                entity.HasOne(d => d.IdTipoMuestraNavigation)
                    .WithMany(p => p.EstudioLaboratorioMuestra)
                    .HasForeignKey(d => d.IdTipoMuestra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstudioLaboratorioMuestra_TipoMuestra");
            });

            modelBuilder.Entity<Examen>(entity =>
            {
                entity.HasKey(e => e.IdExamen)
                    .HasName("PK__Examen__0E8DC9BE3F91F5F5");

                entity.ToTable("Examen", "Proyectos");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.HasOne(d => d.IdEstatusExamenNavigation)
                    .WithMany(p => p.Examen)
                    .HasForeignKey(d => d.IdEstatusExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Examen__IdEstatu__5E4ADDA8");

                entity.HasOne(d => d.IdProgramacionExamenNavigation)
                    .WithMany(p => p.Examen)
                    .HasForeignKey(d => d.IdProgramacionExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Examen__IdProgra__5C629536");

                entity.HasOne(d => d.IdUsuarioParticipanteNavigation)
                    .WithMany(p => p.Examen)
                    .HasForeignKey(d => d.IdUsuarioParticipante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Examen__IdUsuari__5D56B96F");
            });

            modelBuilder.Entity<ExamenReactivo>(entity =>
            {
                entity.HasKey(e => e.IdExamenReactivo)
                    .HasName("PK__ExamenRe__E8618A1557263B3A");

                entity.ToTable("ExamenReactivo", "Proyectos");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.RespuestaAlumno)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdExamenNavigation)
                    .WithMany(p => p.ExamenReactivo)
                    .HasForeignKey(d => d.IdExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExamenRea__IdExa__61274A53");

                entity.HasOne(d => d.IdReactivoNavigation)
                    .WithMany(p => p.ExamenReactivo)
                    .HasForeignKey(d => d.IdReactivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExamenRea__IdRea__621B6E8C");
            });

            modelBuilder.Entity<ExcelArchivo>(entity =>
            {
                entity.HasKey(e => e.IdExcelArchivo)
                    .HasName("PK__ExcelArc__04A05168E1D256AA");

                entity.ToTable("ExcelArchivo", "Proyectos");

                entity.Property(e => e.Estatus).HasMaxLength(50);

                entity.Property(e => e.FechaSubida).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            modelBuilder.Entity<ExcelError>(entity =>
            {
                entity.HasKey(e => e.IdExcelError)
                    .HasName("PK__ExcelErr__26E305CBE51EBE1D");

                entity.ToTable("ExcelError", "Proyectos");

                entity.Property(e => e.ErrorMensaje).HasMaxLength(500);

                entity.Property(e => e.Fila).HasMaxLength(50);

                entity.Property(e => e.Libro).HasMaxLength(100);

                entity.HasOne(d => d.IdExcelArchivoNavigation)
                    .WithMany(p => p.ExcelError)
                    .HasForeignKey(d => d.IdExcelArchivo)
                    .HasConstraintName("FK__ExcelErro__IdExc__14DBF883");
            });

            modelBuilder.Entity<ExcelPolizaCargaMasiva>(entity =>
            {
                entity.HasKey(e => e.IdExcelPolizaCargaMasiva)
                    .HasName("PK__ExcelPol__85F26ED9384BD9B5");

                entity.Property(e => e.Estatus).HasMaxLength(50);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.NombreArchivo).HasMaxLength(200);

                entity.Property(e => e.TotalPolizasExitosas).HasMaxLength(10);

                entity.Property(e => e.TotalPolizasFallidas).HasMaxLength(10);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.ExcelPolizaCargaMasiva)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExcelPoli__IdCom__56BECA79");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.ExcelPolizaCargaMasiva)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .HasConstraintName("FK__ExcelPoli__IdUsu__55CAA640");
            });

            modelBuilder.Entity<ExcelPolizaCargaMasivaError>(entity =>
            {
                entity.HasKey(e => e.IdExcelPolizaCargaMasivaError)
                    .HasName("PK__ExcelPol__7E706C277E636BCC");

                entity.Property(e => e.Hoja).HasMaxLength(100);

                entity.Property(e => e.MensajeError).HasMaxLength(500);

                entity.Property(e => e.Renglon).HasMaxLength(50);

                entity.HasOne(d => d.IdExcelPolizaCargaMasivaNavigation)
                    .WithMany(p => p.ExcelPolizaCargaMasivaError)
                    .HasForeignKey(d => d.IdExcelPolizaCargaMasiva)
                    .HasConstraintName("FK__ExcelPoli__IdExc__599B3724");
            });

            modelBuilder.Entity<Expediente>(entity =>
            {
                entity.HasKey(e => e.IdExpediente);

                entity.Property(e => e.FechaApertura).HasColumnType("date");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Expediente)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Expedient__IdCom__072CF7AA");

                entity.HasOne(d => d.IdExpedienteDatoSocialNavigation)
                    .WithMany(p => p.Expediente)
                    .HasForeignKey(d => d.IdExpedienteDatoSocial)
                    .HasConstraintName("FK_Expediente_ExpedienteDatoSocial");

                entity.HasOne(d => d.IdExpedientePacienteInformacionNavigation)
                    .WithMany(p => p.Expediente)
                    .HasForeignKey(d => d.IdExpedientePacienteInformacion)
                    .HasConstraintName("FK_Expediente_ExpedientePacienteInformacion");

                entity.HasOne(d => d.IdUsuarioPacienteNavigation)
                    .WithMany(p => p.Expediente)
                    .HasForeignKey(d => d.IdUsuarioPaciente)
                    .HasConstraintName("FK__Expedient__IdUsu__0544AF38");
            });

            modelBuilder.Entity<ExpedienteAdministrativo>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAdministrativo)
                    .HasName("PK__Expedien__7FB4510E00E5788D");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.ExpedienteAdministrativo)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__Expedient__IdAux__394E6323");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.ExpedienteAdministrativo)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdCom__1AA9E072");

                entity.HasOne(d => d.IdTipoExpedienteAdministrativoNavigation)
                    .WithMany(p => p.ExpedienteAdministrativo)
                    .HasForeignKey(d => d.IdTipoExpedienteAdministrativo)
                    .HasConstraintName("FK__Expedient__IdTip__1FC39B4A");
            });

            modelBuilder.Entity<ExpedienteAdministrativoMercancia>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAdministrativoMercancia)
                    .HasName("PK__Expedien__8C00CF7D3CFBE2BC");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.Peso).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.RfcDestinitario).HasMaxLength(20);

                entity.Property(e => e.Valor).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdDomicilioDestinoNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoMercancia)
                    .HasForeignKey(d => d.IdDomicilioDestino)
                    .HasConstraintName("FK__Expedient__IdDom__22A007F5");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoMercancia)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdExp__4AE30379");

                entity.HasOne(d => d.IdSatProductoServicioCartaPorteNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoMercancia)
                    .HasForeignKey(d => d.IdSatProductoServicioCartaPorte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdSat__4BD727B2");

                entity.HasOne(d => d.IdSatUnidadNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoMercancia)
                    .HasForeignKey(d => d.IdSatUnidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdSat__4CCB4BEB");

                entity.HasOne(d => d.IdUsuarioDestinatarioNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoMercancia)
                    .HasForeignKey(d => d.IdUsuarioDestinatario)
                    .HasConstraintName("FK__Expedient__IdUsu__23942C2E");
            });

            modelBuilder.Entity<ExpedienteAdministrativoViaje>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAdministrativoViaje)
                    .HasName("PK__Expedien__A4677E448620315E");

                entity.Property(e => e.FechaLlegada).HasColumnType("date");

                entity.Property(e => e.NombreDestinatario).HasMaxLength(200);

                entity.Property(e => e.RfcDestinatario).HasMaxLength(200);

                entity.HasOne(d => d.IdDomicilioEntregaNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoViaje)
                    .HasForeignKey(d => d.IdDomicilioEntrega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdDom__480696CE");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoViaje)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdExp__452A2A23");

                entity.HasOne(d => d.IdUsuarioChoferNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoViajeIdUsuarioChoferNavigation)
                    .HasForeignKey(d => d.IdUsuarioChofer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdUsu__47127295");

                entity.HasOne(d => d.IdUsuarioDestinatarioNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoViajeIdUsuarioDestinatarioNavigation)
                    .HasForeignKey(d => d.IdUsuarioDestinatario)
                    .HasConstraintName("FK__Expedient__IdUsu__24885067");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.ExpedienteAdministrativoViaje)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdVeh__461E4E5C");
            });

            modelBuilder.Entity<ExpedienteAntecedenteFamiliar>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAntecedenteFamiliar);

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.ExpedienteAntecedenteFamiliar)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedenteFamiliar_Enfermedad");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteAntecedenteFamiliar)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedenteFamiliar_Expediente");

                entity.HasOne(d => d.IdParentescoNavigation)
                    .WithMany(p => p.ExpedienteAntecedenteFamiliar)
                    .HasForeignKey(d => d.IdParentesco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedenteFamiliar_Parentesco");
            });

            modelBuilder.Entity<ExpedienteAntecedenteNoPatologico>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAntecedenteNoPatologico);

                entity.Property(e => e.CantidadDia).HasMaxLength(50);

                entity.Property(e => e.Frecuencia).HasMaxLength(50);

                entity.Property(e => e.Inactividad).HasMaxLength(50);

                entity.Property(e => e.Observaciones).HasMaxLength(50);

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.ExpedienteAntecedenteNoPatologico)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedenteNoPatologico_Enfermedad");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteAntecedenteNoPatologico)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedenteNoPatologico_Expediente");
            });

            modelBuilder.Entity<ExpedienteAntecedentePatologico>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAntecedentePatologico);

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.ExpedienteAntecedentePatologico)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedentePatologico_Enfermedad");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteAntecedentePatologico)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedentePatologico_Expediente");
            });

            modelBuilder.Entity<ExpedienteAntecedenteTratamiento>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAntecedenteTratamiento);

                entity.Property(e => e.Tratamiento).HasMaxLength(50);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteAntecedenteTratamiento)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteAntecedenteTratamiento_Expediente");
            });

            modelBuilder.Entity<ExpedienteAuxiliar>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteAuxiliar)
                    .HasName("PK__Expedien__49EE4AB9A7B7CFDA");

                entity.Property(e => e.CodigoTipoAuxiliar).HasMaxLength(2);

                entity.Property(e => e.NumeroAuxiliar).HasMaxLength(20);

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.ExpedienteAuxiliar)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdExp__1D864D1D");
            });

            modelBuilder.Entity<ExpedienteBitacora>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteBitacora);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Seccion).HasMaxLength(100);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteBitacora)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteBitacora_Expediente");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ExpedienteBitacora)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteBitacora_Usuario");
            });

            modelBuilder.Entity<ExpedienteCampo>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteCampo);

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.HasOne(d => d.IdDominioNavigation)
                    .WithMany(p => p.ExpedienteCampo)
                    .HasForeignKey(d => d.IdDominio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteCampo_Dominio");

                entity.HasOne(d => d.IdExpedienteSeccionNavigation)
                    .WithMany(p => p.ExpedienteCampo)
                    .HasForeignKey(d => d.IdExpedienteSeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteCampo_ExpedienteSeccion");
            });

            modelBuilder.Entity<ExpedienteCampoValor>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteCampoValor);

                entity.Property(e => e.Tabla).HasMaxLength(100);

                entity.HasOne(d => d.IdExpedienteCampoNavigation)
                    .WithMany(p => p.ExpedienteCampoValor)
                    .HasForeignKey(d => d.IdExpedienteCampo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteCampoValor_ExpedienteCampo");
            });

            modelBuilder.Entity<ExpedienteDatoSocial>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteDatoSocial);

                entity.Property(e => e.ApellidoMaternoMadre).HasMaxLength(50);

                entity.Property(e => e.ApellidoMaternoPadre).HasMaxLength(50);

                entity.Property(e => e.ApellidoPaternoMadre).HasMaxLength(50);

                entity.Property(e => e.ApellidoPaternoPadre).HasMaxLength(50);

                entity.Property(e => e.Nacionalidad).HasMaxLength(50);

                entity.Property(e => e.NombreMadre).HasMaxLength(50);

                entity.Property(e => e.NombrePadre).HasMaxLength(50);

                entity.Property(e => e.OtrosServiciosMedicos).HasMaxLength(500);

                entity.Property(e => e.PersonaCercanaApellidoMaterno).HasMaxLength(50);

                entity.Property(e => e.PersonaCercanaApellidoPaterno).HasMaxLength(50);

                entity.Property(e => e.PersonaCercanaNombre).HasMaxLength(50);

                entity.Property(e => e.ServicioMedico).HasMaxLength(200);

                entity.HasOne(d => d.IdCiudadNacimientoNavigation)
                    .WithMany(p => p.ExpedienteDatoSocial)
                    .HasForeignKey(d => d.IdCiudadNacimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteDatoSocial_CiudadNacimiento");

                entity.HasOne(d => d.IdEstadoCivilNavigation)
                    .WithMany(p => p.ExpedienteDatoSocial)
                    .HasForeignKey(d => d.IdEstadoCivil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteDatoSocial_EstadoCivil");

                entity.HasOne(d => d.IdParentescoNavigation)
                    .WithMany(p => p.ExpedienteDatoSocial)
                    .HasForeignKey(d => d.IdParentesco)
                    .HasConstraintName("FK_ExpedienteDatoSocial_Parentesco");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.ExpedienteDatoSocial)
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("FK_ExpedienteDatoSocial_Servicio");
            });

            modelBuilder.Entity<ExpedienteDoctor>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteDoctor)
                    .HasName("PK__Expedien__53EF564DC0EB4E2F");

                entity.ToTable("ExpedienteDoctor", "Trackr");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteDoctor)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdExp__463E49ED");

                entity.HasOne(d => d.IdUsuarioDoctorNavigation)
                    .WithMany(p => p.ExpedienteDoctor)
                    .HasForeignKey(d => d.IdUsuarioDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdUsu__47326E26");
            });

            modelBuilder.Entity<ExpedienteEstudio>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteEstudio);

                entity.ToTable("ExpedienteEstudio", "Trackr");

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.ArchivoNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoTipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRealizacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteEstudio)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteEstudio_ExpedienteTrackr");
            });

            modelBuilder.Entity<ExpedientePacienteInformacion>(entity =>
            {
                entity.HasKey(e => e.IdExpedientePacienteInformacion);

                entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);

                entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);

                entity.Property(e => e.Calle).HasMaxLength(50);

                entity.Property(e => e.CodigoPostal).HasMaxLength(5);

                entity.Property(e => e.Colonia).HasMaxLength(50);

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Curp).HasMaxLength(18);

                entity.Property(e => e.EntreCalles).HasMaxLength(50);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.MotivoPacienteEspecial).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.NumeroExterior).HasMaxLength(10);

                entity.Property(e => e.NumeroIdentificacion).HasMaxLength(50);

                entity.Property(e => e.NumeroInterior).HasMaxLength(10);

                entity.Property(e => e.Sexo).HasMaxLength(50);

                entity.Property(e => e.TelefonoCasa).HasMaxLength(10);

                entity.Property(e => e.TelefonoMovil).HasMaxLength(10);

                entity.HasOne(d => d.IdFactorRhNavigation)
                    .WithMany(p => p.ExpedientePacienteInformacion)
                    .HasForeignKey(d => d.IdFactorRh)
                    .HasConstraintName("FK_ExpedientePacienteInformacion_FactorRh");

                entity.HasOne(d => d.IdGrupoSanguineoNavigation)
                    .WithMany(p => p.ExpedientePacienteInformacion)
                    .HasForeignKey(d => d.IdGrupoSanguineo)
                    .HasConstraintName("FK_ExpedientePacienteInformacion_GrupoSanguineo");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.ExpedientePacienteInformacion)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_ExpedientePacienteInformacion_Municipio");

                entity.HasOne(d => d.IdTipoIdentificacionNavigation)
                    .WithMany(p => p.ExpedientePacienteInformacion)
                    .HasForeignKey(d => d.IdTipoIdentificacion)
                    .HasConstraintName("FK_ExpedientePacienteInformacion_TipoIdentificacion");
            });

            modelBuilder.Entity<ExpedientePadecimiento>(entity =>
            {
                entity.HasKey(e => e.IdExpedientePadecimiento)
                    .HasName("PK__Expedien__739E8F0A6E7C5D45");

                entity.ToTable("ExpedientePadecimiento", "Trackr");

                entity.Property(e => e.FechaDiagnostico).HasColumnType("date");

                entity.Property(e => e.IdUsuarioDoctor).HasDefaultValueSql("((5333))");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedientePadecimiento)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdExp__24DD5622");

                entity.HasOne(d => d.IdPadecimientoNavigation)
                    .WithMany(p => p.ExpedientePadecimiento)
                    .HasForeignKey(d => d.IdPadecimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdPad__25D17A5B");

                entity.HasOne(d => d.IdUsuarioDoctorNavigation)
                    .WithMany(p => p.ExpedientePadecimiento)
                    .HasForeignKey(d => d.IdUsuarioDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdUsu__40857097");
            });

            modelBuilder.Entity<ExpedienteRecomendaciones>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteRecomendaciones);

                entity.ToTable("ExpedienteRecomendaciones", "Trackr");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.FechaRealizacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteRecomendaciones)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteRecomendaciones_ExpedienteTrackr");

                entity.HasOne(d => d.IdNotificacionNavigation)
                    .WithMany(p => p.ExpedienteRecomendaciones)
                    .HasForeignKey(d => d.IdNotificacion)
                    .HasConstraintName("FK__Expedient__IdNot__5674B1B6");

                entity.HasOne(d => d.IdUsuarioDoctorNavigation)
                    .WithMany(p => p.ExpedienteRecomendaciones)
                    .HasForeignKey(d => d.IdUsuarioDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteRecomendaciones_Usuario");
            });

            modelBuilder.Entity<ExpedienteRecomendacionesGenerales>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteRecomendacionesGenerales)
                    .HasName("PK__Expedien__17FD3D5D639FA628");

                entity.ToTable("ExpedienteRecomendacionesGenerales", "Trackr");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.FechaRealizacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdAdministradorNavigation)
                    .WithMany(p => p.ExpedienteRecomendacionesGenerales)
                    .HasForeignKey(d => d.IdAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdAdm__7310F064");
            });

            modelBuilder.Entity<ExpedienteSeccion>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteSeccion);

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdExpedienteSeccionPadreNavigation)
                    .WithMany(p => p.InverseIdExpedienteSeccionPadreNavigation)
                    .HasForeignKey(d => d.IdExpedienteSeccionPadre)
                    .HasConstraintName("FK_ExpedienteSeccion_ExpedienteSeccionPadre");

                entity.HasOne(d => d.IdTipoExpedienteNavigation)
                    .WithMany(p => p.ExpedienteSeccion)
                    .HasForeignKey(d => d.IdTipoExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteSeccion_TipoExpediente");
            });

            modelBuilder.Entity<ExpedienteTrackr>(entity =>
            {
                entity.HasKey(e => e.IdExpediente)
                    .HasName("PK__Expedien__101235DAA05C1E9A");

                entity.ToTable("ExpedienteTrackr", "Trackr");

                entity.Property(e => e.Cintura).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Estatura).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.FechaAlta).HasColumnType("date");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Numero)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Peso).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.ExpedienteTrackr)
                    .HasForeignKey(d => d.IdGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdGen__4361DD42");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ExpedienteTrackr)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdUsu__2200E977");
            });

            modelBuilder.Entity<ExpedienteTratamiento>(entity =>
            {
                entity.HasKey(e => e.IdExpedienteTratamiento)
                    .HasName("PK__Expedien__58DDD7D813710334");

                entity.ToTable("ExpedienteTratamiento", "Trackr");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Farmaco).HasMaxLength(200);

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.ImagenTipoMime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Indicaciones).HasMaxLength(500);

                entity.Property(e => e.Unidad).HasMaxLength(100);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteTratamiento)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdExp__3CB4DFB3");

                entity.HasOne(d => d.IdPadecimientoNavigation)
                    .WithMany(p => p.ExpedienteTratamiento)
                    .HasForeignKey(d => d.IdPadecimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdPad__3DA903EC");

                entity.HasOne(d => d.IdUsuarioDoctorNavigation)
                    .WithMany(p => p.ExpedienteTratamiento)
                    .HasForeignKey(d => d.IdUsuarioDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expedient__IdUsu__3E9D2825");
            });

            modelBuilder.Entity<Fabricante>(entity =>
            {
                entity.HasKey(e => e.IdFabricante);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Fabricante)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Fabricant__IdCom__2FDA0782");
            });

            modelBuilder.Entity<FactorRh>(entity =>
            {
                entity.HasKey(e => e.IdFactorRh);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK__Factura__50E7BAF1777E9486");

                entity.Property(e => e.CalleEmisor).HasMaxLength(500);

                entity.Property(e => e.CalleReceptor).HasMaxLength(500);

                entity.Property(e => e.CiudadEmisor).HasMaxLength(500);

                entity.Property(e => e.CiudadReceptor).HasMaxLength(500);

                entity.Property(e => e.CodigoPostalEmisor).HasMaxLength(500);

                entity.Property(e => e.CodigoPostalReceptor).HasMaxLength(500);

                entity.Property(e => e.ColoniaEmisor).HasMaxLength(500);

                entity.Property(e => e.ColoniaReceptor).HasMaxLength(500);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.DescripcionError).HasMaxLength(500);

                entity.Property(e => e.FechaCancelacion).HasColumnType("datetime");

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaFactura).HasColumnType("datetime");

                entity.Property(e => e.FechaPagoFactura).HasColumnType("date");

                entity.Property(e => e.FechaSellado).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(500);

                entity.Property(e => e.LugarExpedicion).HasMaxLength(500);

                entity.Property(e => e.NombreEmisor).HasMaxLength(500);

                entity.Property(e => e.NombreReceptor).HasMaxLength(500);

                entity.Property(e => e.NumeroExteriorEmisor).HasMaxLength(500);

                entity.Property(e => e.NumeroExteriorReceptor).HasMaxLength(500);

                entity.Property(e => e.NumeroInteriorReceptor).HasMaxLength(500);

                entity.Property(e => e.Remision).HasMaxLength(500);

                entity.Property(e => e.RfcEmisor).HasMaxLength(500);

                entity.Property(e => e.RfcReceptor).HasMaxLength(500);

                entity.Property(e => e.SelloCancelacion).HasMaxLength(500);

                entity.Property(e => e.SelloCfd).HasMaxLength(500);

                entity.Property(e => e.SelloSat).HasMaxLength(500);

                entity.Property(e => e.SelloStr).HasMaxLength(500);

                entity.Property(e => e.Subtotal).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.UsoCfdi).HasMaxLength(500);

                entity.Property(e => e.Uuid).HasMaxLength(500);

                entity.HasOne(d => d.IdEstadoEmisorNavigation)
                    .WithMany(p => p.FacturaIdEstadoEmisorNavigation)
                    .HasForeignKey(d => d.IdEstadoEmisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__IdEstad__76026BA8");

                entity.HasOne(d => d.IdEstadoReceptorNavigation)
                    .WithMany(p => p.FacturaIdEstadoReceptorNavigation)
                    .HasForeignKey(d => d.IdEstadoReceptor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__IdEstad__76F68FE1");

                entity.HasOne(d => d.IdEstatusFacturaNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdEstatusFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__IdEstat__741A2336");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .HasConstraintName("FK__Factura__IdExped__62BA8D0A");

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdLocacion)
                    .HasConstraintName("FK__Factura__IdLocac__61C668D1");

                entity.HasOne(d => d.IdRegimenFiscalNavigation)
                    .WithMany(p => p.FacturaIdRegimenFiscalNavigation)
                    .HasForeignKey(d => d.IdRegimenFiscal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__IdRegim__750E476F");

                entity.HasOne(d => d.IdRegimenFiscalReceptorNavigation)
                    .WithMany(p => p.FacturaIdRegimenFiscalReceptorNavigation)
                    .HasForeignKey(d => d.IdRegimenFiscalReceptor)
                    .HasConstraintName("FK__Factura__IdRegim__1C281490");

                entity.HasOne(d => d.IdSatFormaPagoNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdSatFormaPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__IdSatFo__78DED853");

                entity.HasOne(d => d.IdSatMetodoPagoNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdSatMetodoPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__IdSatMe__77EAB41A");

                entity.HasOne(d => d.IdSatTipoComprobanteNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdSatTipoComprobante)
                    .HasConstraintName("FK__Factura__IdSatTi__7933DE0E");

                entity.HasOne(d => d.IdUsuarioClienteNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdUsuarioCliente)
                    .HasConstraintName("FK__Factura__IdUsuar__0638D371");
            });

            modelBuilder.Entity<FacturaConcepto>(entity =>
            {
                entity.HasKey(e => e.IdFacturaConcepto)
                    .HasName("PK__FacturaC__2185ED26231C906D");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Impuestos).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.ImpuestosRetenidos).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.MontoUnitario).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.FacturaConcepto)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaCo__IdCon__7DA38D70");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.FacturaConcepto)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaCo__IdFac__7CAF6937");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.FacturaConcepto)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaCo__IdImp__7E97B1A9");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.FacturaConcepto)
                    .HasForeignKey(d => d.IdPresentacion)
                    .HasConstraintName("FK__FacturaCo__IdPre__5DC0CDC3");
            });

            modelBuilder.Entity<FacturaConceptoImpuesto>(entity =>
            {
                entity.HasKey(e => e.IdFacturaConceptoImpuesto)
                    .HasName("PK__FacturaC__725139E6B2E8619B");

                entity.Property(e => e.Base).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.ClaveImpuesto).HasMaxLength(20);

                entity.Property(e => e.DescripcionImpuesto).HasMaxLength(500);

                entity.Property(e => e.Importe).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Tarifa).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdFacturaConceptoNavigation)
                    .WithMany(p => p.FacturaConceptoImpuesto)
                    .HasForeignKey(d => d.IdFacturaConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaCo__IdFac__035C66C6");

                entity.HasOne(d => d.IdSatTipoFactorNavigation)
                    .WithMany(p => p.FacturaConceptoImpuesto)
                    .HasForeignKey(d => d.IdSatTipoFactor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FacturaCo__IdSat__04508AFF");
            });

            modelBuilder.Entity<Flujo>(entity =>
            {
                entity.HasKey(e => e.IdFlujo)
                    .HasName("PK__Flujo__F347184CEB75A883");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Flujo)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Flujo__IdCompani__3592E0D8");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Flujo)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Flujo__IdRol__63449CEF");

                entity.HasOne(d => d.IdTipoFlujoNavigation)
                    .WithMany(p => p.Flujo)
                    .HasForeignKey(d => d.IdTipoFlujo)
                    .HasConstraintName("FK__Flujo__IdTipoFlu__5402595F");
            });

            modelBuilder.Entity<FlujoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdFlujoDetalle)
                    .HasName("PK__FlujoDet__B21A73DBCCE40FD2");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.FlujoDetalle)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK__FlujoDeta__IdAre__1ADEEA9C");

                entity.HasOne(d => d.IdEstatusPedidoNavigation)
                    .WithMany(p => p.FlujoDetalle)
                    .HasForeignKey(d => d.IdEstatusPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdEst__1BD30ED5");

                entity.HasOne(d => d.IdFlujoNavigation)
                    .WithMany(p => p.FlujoDetalle)
                    .HasForeignKey(d => d.IdFlujo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdFlu__19EAC663");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.FlujoDetalle)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__FlujoDeta__IdRol__3C94E422");
            });

            modelBuilder.Entity<FlujoDetalleAplicado>(entity =>
            {
                entity.HasKey(e => e.IdFlujoDetalleAplicado)
                    .HasName("PK__FlujoDet__A6B2CA93B6EC546F");

                entity.Property(e => e.Comentarios).HasMaxLength(500);

                entity.Property(e => e.FechaAplicacion).HasColumnType("datetime");

                entity.Property(e => e.Origen).HasMaxLength(500);

                entity.HasOne(d => d.IdFlujoDetalleNavigation)
                    .WithMany(p => p.FlujoDetalleAplicado)
                    .HasForeignKey(d => d.IdFlujoDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdFlu__46A85E41");

                entity.HasOne(d => d.IdUsuarioAplicacionNavigation)
                    .WithMany(p => p.FlujoDetalleAplicado)
                    .HasForeignKey(d => d.IdUsuarioAplicacion)
                    .HasConstraintName("FK__FlujoDeta__IdUsu__479C827A");
            });

            modelBuilder.Entity<FlujoDetalleAplicadoResponsable>(entity =>
            {
                entity.HasKey(e => e.IdFlujoDetalleAplicadoResponsable)
                    .HasName("PK__FlujoDet__397FEE1DF8A33877");

                entity.HasOne(d => d.IdFlujoDetalleAplicadoNavigation)
                    .WithMany(p => p.FlujoDetalleAplicadoResponsable)
                    .HasForeignKey(d => d.IdFlujoDetalleAplicado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdFlu__4C613797");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.FlujoDetalleAplicadoResponsable)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdUsu__4B6D135E");
            });

            modelBuilder.Entity<FlujoDetalleResponsable>(entity =>
            {
                entity.HasKey(e => e.IdFlujoDetalleResponsable)
                    .HasName("PK__FlujoDet__9D6477F4F5A613C0");

                entity.HasOne(d => d.IdFlujoDetalleNavigation)
                    .WithMany(p => p.FlujoDetalleResponsable)
                    .HasForeignKey(d => d.IdFlujoDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdFlu__42D7CD5D");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.FlujoDetalleResponsable)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FlujoDeta__IdUsu__43CBF196");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.HasKey(e => e.IdFormaPago);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdFormaPagoPadreNavigation)
                    .WithMany(p => p.InverseIdFormaPagoPadreNavigation)
                    .HasForeignKey(d => d.IdFormaPagoPadre)
                    .HasConstraintName("FK_FormaPago_FormaPagoPadre");

                entity.HasOne(d => d.IdSatFormaPagoNavigation)
                    .WithMany(p => p.FormaPago)
                    .HasForeignKey(d => d.IdSatFormaPago)
                    .HasConstraintName("FK__FormaPago__IdSat__257C74A0");
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.HasKey(e => e.IdGasto)
                    .HasName("PK__Gasto__F25CC32139EFECDC");

                entity.Property(e => e.IdGasto).HasColumnName("idGasto");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.FechaGasto)
                    .HasColumnType("date")
                    .HasColumnName("fechaGasto");

                entity.Property(e => e.Folio)
                    .HasMaxLength(20)
                    .HasColumnName("folio");

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.HasOne(d => d.IdLiquidacionNavigation)
                    .WithMany(p => p.Gasto)
                    .HasForeignKey(d => d.IdLiquidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Gasto__idLiquida__01DE32A8");
            });

            modelBuilder.Entity<GastoConcepto>(entity =>
            {
                entity.HasKey(e => e.IdGastoConcepto)
                    .HasName("PK__GastoCon__20E9218FC1068409");

                entity.Property(e => e.IdGastoConcepto).HasColumnName("idGastoConcepto");

                entity.Property(e => e.IdConcepto).HasColumnName("idConcepto");

                entity.Property(e => e.IdGasto).HasColumnName("idGasto");

                entity.Property(e => e.IdImpuesto).HasColumnName("idImpuesto");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.GastoConcepto)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GastoConc__idCon__04BA9F53");

                entity.HasOne(d => d.IdGastoNavigation)
                    .WithMany(p => p.GastoConcepto)
                    .HasForeignKey(d => d.IdGasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GastoConc__idGas__05AEC38C");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.GastoConcepto)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GastoConc__idImp__263B8EAF");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK__Genero__0F8349880120BCCC");

                entity.ToTable("Genero", "Trackr");

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<GiroComercial>(entity =>
            {
                entity.HasKey(e => e.IdGiroComercial)
                    .HasName("PK__GiroCome__70DCD4B5FD0A3197");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<GrupoActividadCita>(entity =>
            {
                entity.HasKey(e => e.IdGrupoActividadCita);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.GrupoActividadCita)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GrupoActividadCitaCita");

                entity.HasOne(d => d.IdGrupoPersonaActividadNavigation)
                    .WithMany(p => p.GrupoActividadCita)
                    .HasForeignKey(d => d.IdGrupoPersonaActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GrupoActividadCitaGrupoPersonaActividad");
            });

            modelBuilder.Entity<GrupoPersona>(entity =>
            {
                entity.HasKey(e => e.IdGrupoPersona);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Sexo).HasMaxLength(1);
            });

            modelBuilder.Entity<GrupoPersonaActividad>(entity =>
            {
                entity.HasKey(e => e.IdGrupoPersonaActividad);

                entity.Property(e => e.Clave).HasMaxLength(4);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.HasOne(d => d.IdGrupoPersonaNavigation)
                    .WithMany(p => p.GrupoPersonaActividad)
                    .HasForeignKey(d => d.IdGrupoPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GrupoPersonaActividad_GrupoPersona");
            });

            modelBuilder.Entity<GrupoSanguineo>(entity =>
            {
                entity.HasKey(e => e.IdGrupoSanguineo);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Guia>(entity =>
            {
                entity.HasKey(e => e.IdGuia)
                    .HasName("PK__Guia__838CF140168FF7CF");

                entity.ToTable("Guia", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoGuiaNavigation)
                    .WithMany(p => p.Guia)
                    .HasForeignKey(d => d.IdTipoGuia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Guia__IdTipoGuia__19A0ADA0");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.Guia)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Guia__IdUsuarioA__1A94D1D9");
            });

            modelBuilder.Entity<GuiaActividad>(entity =>
            {
                entity.HasKey(e => e.IdGuiaActividad)
                    .HasName("PK__GuiaActi__542E5CA354814247");

                entity.ToTable("GuiaActividad", "Proyectos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Responsable)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFlujoNavigation)
                    .WithMany(p => p.GuiaActividad)
                    .HasForeignKey(d => d.IdFlujo)
                    .HasConstraintName("FK__GuiaActiv__IdFlu__630F92C5");

                entity.HasOne(d => d.IdGuiaElementoTecnicaNavigation)
                    .WithMany(p => p.GuiaActividad)
                    .HasForeignKey(d => d.IdGuiaElementoTecnica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuiaActiv__IdGui__204DAB2F");
            });

            modelBuilder.Entity<GuiaActividadEvidencia>(entity =>
            {
                entity.HasKey(e => e.IdGuiaActividadEvidencia)
                    .HasName("PK__GuiaActi__B943EC04071C7242");

                entity.ToTable("GuiaActividadEvidencia", "Proyectos");

                entity.HasOne(d => d.IdArtefactoNavigation)
                    .WithMany(p => p.GuiaActividadEvidencia)
                    .HasForeignKey(d => d.IdArtefacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuiaActiv__IdArt__67D447E2");

                entity.HasOne(d => d.IdGuiaActividadNavigation)
                    .WithMany(p => p.GuiaActividadEvidencia)
                    .HasForeignKey(d => d.IdGuiaActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuiaActiv__IdGui__232A17DA");
            });

            modelBuilder.Entity<GuiaElementoTecnica>(entity =>
            {
                entity.HasKey(e => e.IdGuiaElementoTecnica)
                    .HasName("PK__GuiaElem__2AD1B5F35A3A06FB");

                entity.ToTable("GuiaElementoTecnica", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Elemento)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Tecnica)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.IdGuiaNavigation)
                    .WithMany(p => p.GuiaElementoTecnica)
                    .HasForeignKey(d => d.IdGuia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuiaEleme__IdGui__1D713E84");
            });

            modelBuilder.Entity<HistorialMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdHistorialMovimiento)
                    .HasName("PK__Historia__492B56C89A379CB3");

                entity.ToTable("HistorialMovimiento", "Proyectos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Folio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.HistorialMovimiento)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__IdPro__448B0BA5");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.HistorialMovimiento)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__IdUsu__4396E76C");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("PK__Horario__1539229BD597FC80");

                entity.ToTable("Horario", "Proyectos");

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasKey(e => e.IdHospital);

                entity.Property(e => e.Calle).HasMaxLength(50);

                entity.Property(e => e.Ciudad).HasMaxLength(50);

                entity.Property(e => e.Clabe).HasMaxLength(50);

                entity.Property(e => e.CodigoPostal).HasMaxLength(50);

                entity.Property(e => e.Colonia).HasMaxLength(50);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Cuenta).HasMaxLength(50);

                entity.Property(e => e.EntreCalles).HasMaxLength(50);

                entity.Property(e => e.FechaContableActual).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NombreComercial).HasMaxLength(50);

                entity.Property(e => e.NumeroExterior).HasMaxLength(50);

                entity.Property(e => e.NumeroInterior).HasMaxLength(50);

                entity.Property(e => e.PortalWeb).HasMaxLength(50);

                entity.Property(e => e.Rfc).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.HasOne(d => d.IdAlmacenCaducoNavigation)
                    .WithMany(p => p.HospitalIdAlmacenCaducoNavigation)
                    .HasForeignKey(d => d.IdAlmacenCaduco)
                    .HasConstraintName("FK__Hospital__IdAlma__0015E5C7");

                entity.HasOne(d => d.IdAlmacenProduccionNavigation)
                    .WithMany(p => p.HospitalIdAlmacenProduccionNavigation)
                    .HasForeignKey(d => d.IdAlmacenProduccion)
                    .HasConstraintName("FK__Hospital__IdAlma__01FE2E39");

                entity.HasOne(d => d.IdBancoNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdBanco)
                    .HasConstraintName("FK_Hospital_Banco");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK_Hospital_Compania");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Hospital_Estado");

                entity.HasOne(d => d.IdLadaNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdLada)
                    .HasConstraintName("FK_Hospital_Lada");

                entity.HasOne(d => d.IdListaPrecioDefaultNavigation)
                    .WithMany(p => p.HospitalIdListaPrecioDefaultNavigation)
                    .HasForeignKey(d => d.IdListaPrecioDefault)
                    .HasConstraintName("FK__Hospital__IdList__2764BD12");

                entity.HasOne(d => d.IdListaPrecioLineaNavigation)
                    .WithMany(p => p.HospitalIdListaPrecioLineaNavigation)
                    .HasForeignKey(d => d.IdListaPrecioLinea)
                    .HasConstraintName("FK__Hospital__IdList__2858E14B");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_Hospital_Municipio");

                entity.HasOne(d => d.IdRegimenFiscalNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdRegimenFiscal)
                    .HasConstraintName("FK_Hospital_RegimenFiscal");

                entity.HasOne(d => d.IdUsuarioGerenteNavigation)
                    .WithMany(p => p.Hospital)
                    .HasForeignKey(d => d.IdUsuarioGerente)
                    .HasConstraintName("FK_Hospital_Usuario");
            });

            modelBuilder.Entity<HospitalLogotipo>(entity =>
            {
                entity.HasKey(e => e.IdHospitalLogotipo)
                    .HasName("PK__Hospital__E901BAC5EAB5DD94");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.HospitalLogotipo)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HospitalLogotipo_Hospital");
            });

            modelBuilder.Entity<Icono>(entity =>
            {
                entity.HasKey(e => e.IdIcono);

                entity.Property(e => e.Clase).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Impuesto>(entity =>
            {
                entity.HasKey(e => e.IdImpuesto);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.PorcentajeNeto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Impuesto)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Impuesto__IdComp__32B6742D");

                entity.HasOne(d => d.IdCuentaContableRedondeoNavigation)
                    .WithMany(p => p.Impuesto)
                    .HasForeignKey(d => d.IdCuentaContableRedondeo)
                    .HasConstraintName("FK_Impuesto_IdCuen_CuentaContableRedondeo");
            });

            modelBuilder.Entity<ImpuestoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdImpuestoDetalle);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCuentaContableAbonoNavigation)
                    .WithMany(p => p.ImpuestoDetalleIdCuentaContableAbonoNavigation)
                    .HasForeignKey(d => d.IdCuentaContableAbono)
                    .HasConstraintName("FK_ImpuestoDetalle_CuentaContableAbono");

                entity.HasOne(d => d.IdCuentaContableCargoNavigation)
                    .WithMany(p => p.ImpuestoDetalleIdCuentaContableCargoNavigation)
                    .HasForeignKey(d => d.IdCuentaContableCargo)
                    .HasConstraintName("FK_ImpuestoDetalle_CuentaContableCargo");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.ImpuestoDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImpuestoDetalle_Impuesto");

                entity.HasOne(d => d.IdTipoImpuestoNavigation)
                    .WithMany(p => p.ImpuestoDetalle)
                    .HasForeignKey(d => d.IdTipoImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImpuestoDetalle_TipoImpuesto");
            });

            modelBuilder.Entity<Institucion>(entity =>
            {
                entity.HasKey(e => e.IdInstitucion);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<InventarioFisico>(entity =>
            {
                entity.HasKey(e => e.IdInventarioFisico);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.Property(e => e.ValorTotalDiferencia).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.InventarioFisico)
                    .HasForeignKey(d => d.IdAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisico_Almacen");

                entity.HasOne(d => d.IdEstatusInventarioFisicoNavigation)
                    .WithMany(p => p.InventarioFisico)
                    .HasForeignKey(d => d.IdEstatusInventarioFisico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisico_EstatusInventarioFisico");

                entity.HasOne(d => d.IdUsuarioAlmacenistaNavigation)
                    .WithMany(p => p.InventarioFisico)
                    .HasForeignKey(d => d.IdUsuarioAlmacenista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisico_UsuarioAlmacenista");
            });

            modelBuilder.Entity<InventarioFisicoAjuste>(entity =>
            {
                entity.HasKey(e => e.IdInventarioFisicoAjuste);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdInventarioFisicoNavigation)
                    .WithMany(p => p.InventarioFisicoAjuste)
                    .HasForeignKey(d => d.IdInventarioFisico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisicoAjuste_InventarioFisico");
            });

            modelBuilder.Entity<InventarioFisicoAjusteDetalle>(entity =>
            {
                entity.HasKey(e => e.IdInventarioFisicoAjusteDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.RazonAjuste).HasMaxLength(500);

                entity.HasOne(d => d.IdInventarioFisicoAjusteNavigation)
                    .WithMany(p => p.InventarioFisicoAjusteDetalle)
                    .HasForeignKey(d => d.IdInventarioFisicoAjuste)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisicoAjusteDetalle_InventarioFisicoAjuste");

                entity.HasOne(d => d.IdInventarioFisicoDetalleNavigation)
                    .WithMany(p => p.InventarioFisicoAjusteDetalle)
                    .HasForeignKey(d => d.IdInventarioFisicoDetalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisicoAjusteDetalle_InventarioFisicoDetalle");
            });

            modelBuilder.Entity<InventarioFisicoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdInventarioFisicoDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.ValorTotalDiferencia).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.InventarioFisicoDetalle)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisicoDetalle_Articulo");

                entity.HasOne(d => d.IdInventarioFisicoNavigation)
                    .WithMany(p => p.InventarioFisicoDetalle)
                    .HasForeignKey(d => d.IdInventarioFisico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventarioFisicoDetalle_InventarioFisico");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.InventarioFisicoDetalle)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_InventarioFisicoDetalle_Ubicacion");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.BankAccount).HasMaxLength(5);

                entity.Property(e => e.CancellationDate).HasColumnType("datetime");

                entity.Property(e => e.CaseFileDate).HasColumnType("date");

                entity.Property(e => e.DriverName).HasMaxLength(60);

                entity.Property(e => e.ExCity).HasMaxLength(50);

                entity.Property(e => e.ExCountry).HasMaxLength(50);

                entity.Property(e => e.ExFiscalRegime).HasMaxLength(50);

                entity.Property(e => e.ExLocation).HasMaxLength(50);

                entity.Property(e => e.ExName).HasMaxLength(60);

                entity.Property(e => e.ExNeighborhood).HasMaxLength(50);

                entity.Property(e => e.ExOutdoorNumber).HasMaxLength(35);

                entity.Property(e => e.ExRfc).HasMaxLength(15);

                entity.Property(e => e.ExState).HasMaxLength(50);

                entity.Property(e => e.ExStreet).HasMaxLength(50);

                entity.Property(e => e.ExZipCode).HasMaxLength(15);

                entity.Property(e => e.ExpeditionPlace).HasMaxLength(100);

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.FileNameCanceled).HasMaxLength(200);

                entity.Property(e => e.FiscalRegimeCode).HasMaxLength(3);

                entity.Property(e => e.FreightAmount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.HandlingAmount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Ieps)
                    .HasColumnType("numeric(8, 2)")
                    .HasColumnName("IEPS");

                entity.Property(e => e.Iepswithheld)
                    .HasColumnType("numeric(8, 2)")
                    .HasColumnName("IEPSWithheld");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoicePaymentDate).HasColumnType("date");

                entity.Property(e => e.InvoiceStampedDate).HasColumnType("datetime");

                entity.Property(e => e.Isr)
                    .HasColumnType("numeric(8, 2)")
                    .HasColumnName("ISR");

                entity.Property(e => e.OtherAmount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.PaymentAccountNumber).HasMaxLength(50);

                entity.Property(e => e.PaymentMethod).HasMaxLength(100);

                entity.Property(e => e.PaymentMethodCode).HasMaxLength(10);

                entity.Property(e => e.PaymentMode).HasMaxLength(100);

                entity.Property(e => e.PaymentModeCode).HasMaxLength(10);

                entity.Property(e => e.PolicyNumber).HasMaxLength(10);

                entity.Property(e => e.ReCity).HasMaxLength(50);

                entity.Property(e => e.ReCountry).HasMaxLength(50);

                entity.Property(e => e.ReInteriorNumber).HasMaxLength(35);

                entity.Property(e => e.ReName).HasMaxLength(100);

                entity.Property(e => e.ReNeighborhood).HasMaxLength(50);

                entity.Property(e => e.ReOrigin).HasMaxLength(100);

                entity.Property(e => e.ReOutdoorNumber).HasMaxLength(35);

                entity.Property(e => e.ReRfc).HasMaxLength(15);

                entity.Property(e => e.ReState).HasMaxLength(50);

                entity.Property(e => e.ReStreet).HasMaxLength(50);

                entity.Property(e => e.ReZipCode).HasMaxLength(15);

                entity.Property(e => e.Subtotal).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Tax).HasMaxLength(35);

                entity.Property(e => e.TaxAmount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.TaxRate).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.TaxWithheld).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.TotalAmount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.TransferredTax).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Type).HasMaxLength(20);

                entity.Property(e => e.Unit).HasMaxLength(35);

                entity.Property(e => e.UnitAmount).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Uuid).HasMaxLength(150);

                entity.Property(e => e.VehicleName).HasMaxLength(50);

                entity.Property(e => e.VehicleType).HasMaxLength(35);

                entity.Property(e => e.VoucherType).HasMaxLength(50);
            });

            modelBuilder.Entity<Jerarquia>(entity =>
            {
                entity.HasKey(e => e.IdJerarquia)
                    .HasName("PK__Jerarqui__2C6EC225992C435C");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Jerarquia)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdCom__613C58EC");

                entity.HasOne(d => d.IdTipoAuxiliarNavigation)
                    .WithMany(p => p.Jerarquia)
                    .HasForeignKey(d => d.IdTipoAuxiliar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdTip__604834B3");
            });

            modelBuilder.Entity<JerarquiaAcceso>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaAcceso)
                    .HasName("PK__Jerarqui__A237C536E662A723");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.JerarquiaAcceso)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdCom__7DF8932B");
            });

            modelBuilder.Entity<JerarquiaAccesoEstructura>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaAccesoEstructura)
                    .HasName("PK__Jerarqui__BC3E920F1B1C9A84");

                entity.HasOne(d => d.IdAccesoNavigation)
                    .WithMany(p => p.JerarquiaAccesoEstructura)
                    .HasForeignKey(d => d.IdAcceso)
                    .HasConstraintName("FK__Jerarquia__IdAcc__01C9240F");

                entity.HasOne(d => d.IdJerarquiaAccesoNavigation)
                    .WithMany(p => p.JerarquiaAccesoEstructura)
                    .HasForeignKey(d => d.IdJerarquiaAcceso)
                    .HasConstraintName("FK__Jerarquia__IdJer__00D4FFD6");

                entity.HasOne(d => d.IdJerarquiaAccesoEstructuraPadreNavigation)
                    .WithMany(p => p.InverseIdJerarquiaAccesoEstructuraPadreNavigation)
                    .HasForeignKey(d => d.IdJerarquiaAccesoEstructuraPadre)
                    .HasConstraintName("FK__Jerarquia__IdJer__02BD4848");
            });

            modelBuilder.Entity<JerarquiaAccesoTipoCompania>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaAccesoTipoCompania)
                    .HasName("PK__Jerarqui__91C01CF0D361E26C");

                entity.HasOne(d => d.IdJerarquiaAccesoNavigation)
                    .WithMany(p => p.JerarquiaAccesoTipoCompania)
                    .HasForeignKey(d => d.IdJerarquiaAcceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdJer__0599B4F3");

                entity.HasOne(d => d.IdTipoCompaniaNavigation)
                    .WithMany(p => p.JerarquiaAccesoTipoCompania)
                    .HasForeignKey(d => d.IdTipoCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdTip__068DD92C");
            });

            modelBuilder.Entity<JerarquiaColumna>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaColumna)
                    .HasName("PK__Jerarqui__BF08D36BBCA3530D");

                entity.Property(e => e.Acumula).HasMaxLength(50);

                entity.Property(e => e.Letra).HasMaxLength(2);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdJerarquiaColumnaRelacionadaNavigation)
                    .WithMany(p => p.InverseIdJerarquiaColumnaRelacionadaNavigation)
                    .HasForeignKey(d => d.IdJerarquiaColumnaRelacionada)
                    .HasConstraintName("FK__Jerarquia__IdJer__1B68FA81");

                entity.HasOne(d => d.IdVersionPolizaNavigation)
                    .WithMany(p => p.JerarquiaColumna)
                    .HasForeignKey(d => d.IdVersionPoliza)
                    .HasConstraintName("FK__Jerarquia__IdVer__1798699D");
            });

            modelBuilder.Entity<JerarquiaConfiguracion>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaConfiguracion)
                    .HasName("PK__Jerarqui__FA8816EDFC4F098A");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Formula).HasMaxLength(200);

                entity.HasOne(d => d.IdJerarquiaColumnaNavigation)
                    .WithMany(p => p.JerarquiaConfiguracion)
                    .HasForeignKey(d => d.IdJerarquiaColumna)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdJer__6F8A7843");

                entity.HasOne(d => d.IdJerarquiaConfiguracionRelacionadoNavigation)
                    .WithMany(p => p.InverseIdJerarquiaConfiguracionRelacionadoNavigation)
                    .HasForeignKey(d => d.IdJerarquiaConfiguracionRelacionado)
                    .HasConstraintName("FK__Jerarquia__IdJer__6DA22FD1");

                entity.HasOne(d => d.IdJerarquiaEstructuraNavigation)
                    .WithMany(p => p.JerarquiaConfiguracion)
                    .HasForeignKey(d => d.IdJerarquiaEstructura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdJer__6E96540A");
            });

            modelBuilder.Entity<JerarquiaEstructura>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaEstructura)
                    .HasName("PK__Jerarqui__DAAB434FCFCC38BE");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.Numero).HasMaxLength(20);

                entity.Property(e => e.Ruta).HasMaxLength(500);

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.JerarquiaEstructura)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__Jerarquia__IdAux__69D19EED");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.JerarquiaEstructura)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .HasConstraintName("FK__Jerarquia__IdCue__68DD7AB4");

                entity.HasOne(d => d.IdJerarquiaNavigation)
                    .WithMany(p => p.JerarquiaEstructura)
                    .HasForeignKey(d => d.IdJerarquia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdJer__67E9567B");

                entity.HasOne(d => d.IdJerarquiaEstructuraPadreNavigation)
                    .WithMany(p => p.InverseIdJerarquiaEstructuraPadreNavigation)
                    .HasForeignKey(d => d.IdJerarquiaEstructuraPadre)
                    .HasConstraintName("FK__Jerarquia__Jerar__6AC5C326");
            });

            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.HasKey(e => e.IdKardex);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaCaducidad).HasColumnType("date");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.Kardex)
                    .HasForeignKey(d => d.IdAlmacen)
                    .HasConstraintName("FK_Kardex_Almacen");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Kardex)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kardex_Articulo");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.Kardex)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_Kardex_Ubicacion");
            });

            modelBuilder.Entity<Lada>(entity =>
            {
                entity.HasKey(e => e.IdLada)
                    .HasName("PK__Lada__372897B722B451DA");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Liquidacion>(entity =>
            {
                entity.HasKey(e => e.IdLiquidacion)
                    .HasName("PK__Liquidac__AD38F40F077B7FD8");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.FechaLiquidacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaLiquidacion");

                entity.Property(e => e.Folio)
                    .HasMaxLength(20)
                    .HasColumnName("folio");

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.IdCompania).HasColumnName("idCompania");

                entity.Property(e => e.IdEstatusLiquidacion).HasColumnName("idEstatusLiquidacion");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Liquidaci__idCom__3C3FDE67");

                entity.HasOne(d => d.IdEstatusLiquidacionNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdEstatusLiquidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Liquidaci__idEst__3B4BBA2E");

                entity.HasOne(d => d.IdMovimientoMaterialSalidaNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdMovimientoMaterialSalida)
                    .HasConstraintName("FK__Liquidaci__IdMov__16F94B1F");
            });

            modelBuilder.Entity<ListaPrecio>(entity =>
            {
                entity.HasKey(e => e.IdListaPrecio);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaFinVigencia).HasColumnType("date");

                entity.Property(e => e.FechaInicioVigencia).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Observaciones).HasMaxLength(50);

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.ListaPrecio)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK__ListaPrec__IdMon__294D0584");
            });

            modelBuilder.Entity<ListaPrecioClinica>(entity =>
            {
                entity.HasKey(e => e.IdListaPrecioClinica);

                entity.HasOne(d => d.IdClinicaNavigation)
                    .WithMany(p => p.ListaPrecioClinica)
                    .HasForeignKey(d => d.IdClinica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioClinica_Clinica");

                entity.HasOne(d => d.IdListaPrecioNavigation)
                    .WithMany(p => p.ListaPrecioClinica)
                    .HasForeignKey(d => d.IdListaPrecio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioClinica_ListaPrecio");
            });

            modelBuilder.Entity<ListaPrecioDetalle>(entity =>
            {
                entity.HasKey(e => e.IdListaPrecioDetalle);

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.PrecioBase).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdComisionNavigation)
                    .WithMany(p => p.ListaPrecioDetalle)
                    .HasForeignKey(d => d.IdComision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioDetalle_Comision");

                entity.HasOne(d => d.IdDescuentoNavigation)
                    .WithMany(p => p.ListaPrecioDetalle)
                    .HasForeignKey(d => d.IdDescuento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioDetalle_Descuento");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.ListaPrecioDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioDetalle_Impuesto");

                entity.HasOne(d => d.IdListaPrecioNavigation)
                    .WithMany(p => p.ListaPrecioDetalle)
                    .HasForeignKey(d => d.IdListaPrecio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioDetalle_ListaPrecio");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ListaPrecioDetalle)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioDetalle_Presentacion");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.ListaPrecioDetalle)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListaPrecioDetalle_UsuarioAlta");
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad)
                    .HasName("PK__Localida__27432612EEE3BA42");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Localidad)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Localidad__IdEst__7E62A77F");
            });

            modelBuilder.Entity<Mercado>(entity =>
            {
                entity.HasKey(e => e.IdMercado)
                    .HasName("PK__Mercado__AB67703FFEF0BE15");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdGiroComercialNavigation)
                    .WithMany(p => p.Mercado)
                    .HasForeignKey(d => d.IdGiroComercial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mercado__IdGiroC__0A5E6A10");
            });

            modelBuilder.Entity<MercadoCompania>(entity =>
            {
                entity.HasKey(e => e.IdMercadoCompania)
                    .HasName("PK__MercadoC__83F4A6078EAE9301");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.MercadoCompania)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MercadoCo__IdCom__0E2EFAF4");

                entity.HasOne(d => d.IdMercadoNavigation)
                    .WithMany(p => p.MercadoCompania)
                    .HasForeignKey(d => d.IdMercado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MercadoCo__IdMer__0D3AD6BB");
            });

            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasKey(e => e.IdMetodoPago)
                    .HasName("PK__MetodoPa__6F49A9BE9C5887A4");

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.HasKey(e => e.IdMoneda)
                    .HasName("PK__Moneda__AA6906715839147A");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.Simbolo).HasMaxLength(10);
            });

            modelBuilder.Entity<MotivoVisita>(entity =>
            {
                entity.HasKey(e => e.IdMotivoVisita);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<MovimientoEstadoCuenta>(entity =>
            {
                entity.HasKey(e => e.IdMovimientoEstadoCuenta)
                    .HasName("PK__Movimien__FD3E0426ED311189");

                entity.Property(e => e.Abono).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Cargo).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.CodigoTransaccion).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.NumeroCheque).HasMaxLength(50);

                entity.HasOne(d => d.IdChequeraNavigation)
                    .WithMany(p => p.MovimientoEstadoCuenta)
                    .HasForeignKey(d => d.IdChequera)
                    .HasConstraintName("FK__Movimient__IdChe__4D8A65FA");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.MovimientoEstadoCuenta)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__IdCom__4E7E8A33");
            });

            modelBuilder.Entity<MovimientoMaterial>(entity =>
            {
                entity.HasKey(e => e.IdMovimientoMaterial);

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.MovimientoMaterial)
                    .HasForeignKey(d => d.IdAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimientoMaterial_Almacen");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.MovimientoMaterial)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Movimient__IdDep__1EE485AA");

                entity.HasOne(d => d.IdEstatusMovimientoMaterialNavigation)
                    .WithMany(p => p.MovimientoMaterial)
                    .HasForeignKey(d => d.IdEstatusMovimientoMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimientoMaterial_EstatusMovimientoMaterial");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.MovimientoMaterial)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .HasConstraintName("FK__Movimient__IdExp__3A42875C");

                entity.HasOne(d => d.IdOrdenCompraNavigation)
                    .WithMany(p => p.MovimientoMaterial)
                    .HasForeignKey(d => d.IdOrdenCompra)
                    .HasConstraintName("FK_MovimientoMaterial_OrdenCompra");

                entity.HasOne(d => d.IdTipoMovimientoMaterialNavigation)
                    .WithMany(p => p.MovimientoMaterial)
                    .HasForeignKey(d => d.IdTipoMovimientoMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimientoMaterial_TipoMovimientoMaterial");

                entity.HasOne(d => d.IdUsuarioAlmacenistaNavigation)
                    .WithMany(p => p.MovimientoMaterialIdUsuarioAlmacenistaNavigation)
                    .HasForeignKey(d => d.IdUsuarioAlmacenista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimientoMaterial_UsuarioAlmacenista");

                entity.HasOne(d => d.IdUsuarioProveedorNavigation)
                    .WithMany(p => p.MovimientoMaterialIdUsuarioProveedorNavigation)
                    .HasForeignKey(d => d.IdUsuarioProveedor)
                    .HasConstraintName("FK__Movimient__IdUsu__226AFDCB");
            });

            modelBuilder.Entity<MovimientoMaterialDetalle>(entity =>
            {
                entity.HasKey(e => e.IdMovimientoMaterialDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.FechaCaducidad).HasColumnType("date");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.MovimientoMaterialDetalle)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimientoMaterialDetalle_Articulo");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.MovimientoMaterialDetalle)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__Movimient__IdCon__2B0043CC");

                entity.HasOne(d => d.IdKardexNavigation)
                    .WithMany(p => p.MovimientoMaterialDetalle)
                    .HasForeignKey(d => d.IdKardex)
                    .HasConstraintName("FK_MovimientoMaterialDetalle_Kardex");

                entity.HasOne(d => d.IdMovimientoMaterialNavigation)
                    .WithMany(p => p.MovimientoMaterialDetalle)
                    .HasForeignKey(d => d.IdMovimientoMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovimientoMaterialDetalle_MovimientoMaterial");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.MovimientoMaterialDetalle)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_MovimientoMaterialDetalle_Ubicacion");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Estado");
            });

            modelBuilder.Entity<Necesidad>(entity =>
            {
                entity.HasKey(e => e.IdNecesidad)
                    .HasName("PK__Necesida__484FDA59D1EB65CE");

                entity.Property(e => e.IdNecesidad).HasColumnName("idNecesidad");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.Folio)
                    .HasMaxLength(35)
                    .HasColumnName("folio");

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.IdDomicilioSucursal).HasColumnName("idDomicilioSucursal");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.Property(e => e.IdUsuarioEmpleado).HasColumnName("idUsuarioEmpleado");

                entity.HasOne(d => d.IdDomicilioSucursalNavigation)
                    .WithMany(p => p.Necesidad)
                    .HasForeignKey(d => d.IdDomicilioSucursal)
                    .HasConstraintName("FK__Necesidad__idDom__12349602");

                entity.HasOne(d => d.IdLiquidacionNavigation)
                    .WithMany(p => p.Necesidad)
                    .HasForeignKey(d => d.IdLiquidacion)
                    .HasConstraintName("FK__Necesidad__idLiq__5BB889C0");

                entity.HasOne(d => d.IdUsuarioEmpleadoNavigation)
                    .WithMany(p => p.Necesidad)
                    .HasForeignKey(d => d.IdUsuarioEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Necesidad__idUsu__160526E6");
            });

            modelBuilder.Entity<NecesidadPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdNecesidadPresentacion)
                    .HasName("PK__Necesida__1A6E3531237E719C");

                entity.Property(e => e.IdNecesidadPresentacion).HasColumnName("idNecesidadPresentacion");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdNecesidad).HasColumnName("idNecesidad");

                entity.Property(e => e.IdPresentacion).HasColumnName("idPresentacion");

                entity.HasOne(d => d.IdNecesidadNavigation)
                    .WithMany(p => p.NecesidadPresentacion)
                    .HasForeignKey(d => d.IdNecesidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Necesidad__idNec__5E94F66B");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.NecesidadPresentacion)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Necesidad__idPre__5F891AA4");
            });

            modelBuilder.Entity<NivelExamen>(entity =>
            {
                entity.HasKey(e => e.IdNivelExamen)
                    .HasName("PK__NivelExa__1F1A11FB608A3D28");

                entity.ToTable("NivelExamen", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            });

            modelBuilder.Entity<NotaFlujo>(entity =>
            {
                entity.HasKey(e => e.IdNotaFlujo)
                    .HasName("PK__NotaFluj__6DEF5973897ED6EE");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.FechaContable).HasColumnType("datetime");

                entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Origen).HasMaxLength(500);

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.NotaFlujo)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__NotaFlujo__IdCon__53433F50");

                entity.HasOne(d => d.IdEstatusNotaFlujoNavigation)
                    .WithMany(p => p.NotaFlujo)
                    .HasForeignKey(d => d.IdEstatusNotaFlujo)
                    .HasConstraintName("FK__NotaFlujo__IdEst__61915EA7");

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.NotaFlujo)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaFlujo__IdLoc__54376389");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.NotaFlujo)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK__NotaFlujo__IdMon__5EB4F1FC");

                entity.HasOne(d => d.IdMovimientoEstadoCuentaNavigation)
                    .WithMany(p => p.NotaFlujo)
                    .HasForeignKey(d => d.IdMovimientoEstadoCuenta)
                    .HasConstraintName("FK__NotaFlujo__IdMov__5AE46118");

                entity.HasOne(d => d.IdObjetoFlujoNavigation)
                    .WithMany(p => p.NotaFlujo)
                    .HasForeignKey(d => d.IdObjetoFlujo)
                    .HasConstraintName("FK__NotaFlujo__IdObj__524F1B17");
            });

            modelBuilder.Entity<NotaFlujoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdNotaFlujoDetalle)
                    .HasName("PK__NotaFluj__B0454B6D9527598B");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Origen).HasMaxLength(500);

                entity.Property(e => e.TipoMovimiento).HasMaxLength(1);

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.NotaFlujoDetalle)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__NotaFlujo__IdAux__5BD88551");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.NotaFlujoDetalle)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaFlujo__IdCon__58FC18A6");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.NotaFlujoDetalle)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .HasConstraintName("FK__NotaFlujo__IdExp__59F03CDF");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.NotaFlujoDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("FK__NotaFlujo__IdImp__5CCCA98A");

                entity.HasOne(d => d.IdNotaFlujoNavigation)
                    .WithMany(p => p.NotaFlujoDetalle)
                    .HasForeignKey(d => d.IdNotaFlujo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaFlujo__IdNot__5713D034");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.NotaFlujoDetalle)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__NotaFlujo__IdUsu__5807F46D");
            });

            modelBuilder.Entity<NotaFlujoPago>(entity =>
            {
                entity.HasKey(e => e.IdNotaFlujoPago)
                    .HasName("PK__NotaFluj__A1E3272609AD2C87");

                entity.HasOne(d => d.IdNotaFlujoNavigation)
                    .WithMany(p => p.NotaFlujoPago)
                    .HasForeignKey(d => d.IdNotaFlujo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaFlujo__IdNot__32A16594");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.NotaFlujoPago)
                    .HasForeignKey(d => d.IdPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaFlujo__IdPag__339589CD");
            });

            modelBuilder.Entity<NotaGasto>(entity =>
            {
                entity.HasKey(e => e.IdNotaGasto)
                    .HasName("PK__NotaGast__A86C59AA826141E9");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

                entity.Property(e => e.FechaProgramada).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaGasto__IdCon__43ABF605");

                entity.HasOne(d => d.IdEstatusNotaGastoNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdEstatusNotaGasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaGasto__IdEst__30992191");

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaGasto__IdLoc__79D2FC8C");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdMoneda)
                    .HasConstraintName("FK__NotaGasto__IdMon__33CA93F7");

                entity.HasOne(d => d.IdMovimientoMaterialNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdMovimientoMaterial)
                    .HasConstraintName("FK__NotaGasto__IdMov__235F2204");

                entity.HasOne(d => d.IdSatMovimientoNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdSatMovimiento)
                    .HasConstraintName("FK__NotaGasto__IdSat__5E7FE7D2");

                entity.HasOne(d => d.IdTipoNotaGastoNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdTipoNotaGasto)
                    .HasConstraintName("FK__NotaGasto__IdTip__67B44C51");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.NotaGasto)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaGasto__IdUsu__16D94F8E");
            });

            modelBuilder.Entity<NotaGastoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdNotaGastoDetalle)
                    .HasName("PK__NotaGast__4852B384C81F8543");

                entity.Property(e => e.Anexo).HasMaxLength(500);

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.NotaGastoDetalle)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__NotaGasto__IdAux__25E688F4");

                entity.HasOne(d => d.IdComisionNavigation)
                    .WithMany(p => p.NotaGastoDetalle)
                    .HasForeignKey(d => d.IdComision)
                    .HasConstraintName("FK__NotaGasto__IdCom__1AFEE62D");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.NotaGastoDetalle)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaGasto__IdCon__23FE4082");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.NotaGastoDetalle)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .HasConstraintName("FK__NotaGasto__IdExp__24F264BB");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.NotaGastoDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("FK__NotaGasto__IdImp__26DAAD2D");

                entity.HasOne(d => d.IdNotaGastoNavigation)
                    .WithMany(p => p.NotaGastoDetalle)
                    .HasForeignKey(d => d.IdNotaGasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NotaGasto__IdNot__230A1C49");
            });

            modelBuilder.Entity<NotaVenta>(entity =>
            {
                entity.HasKey(e => e.IdNotaVenta);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.Identificador).HasMaxLength(500);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.Property(e => e.Total).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__NotaVenta__IdCon__3CFEF876");

                entity.HasOne(d => d.IdEstatusNotaVentaNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdEstatusNotaVenta)
                    .HasConstraintName("FK__NotaVenta__IdEst__21C0F255");

                entity.HasOne(d => d.IdExpedienteAdministrativoNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdExpedienteAdministrativo)
                    .HasConstraintName("FK__NotaVenta__IdExp__0D05CC91");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK__NotaVenta__IdFac__25476A76");

                entity.HasOne(d => d.IdMovimientoMaterialNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdMovimientoMaterial)
                    .HasConstraintName("FK_NotaVenta_MovimientoMaterial");

                entity.HasOne(d => d.IdNotaVentaPadreDevolucionNavigation)
                    .WithMany(p => p.InverseIdNotaVentaPadreDevolucionNavigation)
                    .HasForeignKey(d => d.IdNotaVentaPadreDevolucion)
                    .HasConstraintName("FK__NotaVenta__IdNot__0FA2421A");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("FK_NotaVenta_Paciente");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__NotaVenta__IdPed__1486F2C8");

                entity.HasOne(d => d.IdPuntoVentaNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdPuntoVenta)
                    .HasConstraintName("FK_NotaVenta_PuntoVenta");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaVenta_Recibo");

                entity.HasOne(d => d.IdTipoNotaVentaNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdTipoNotaVenta)
                    .HasConstraintName("FK__NotaVenta__IdTip__0EAE1DE1");

                entity.HasOne(d => d.IdUbicacionVentaNavigation)
                    .WithMany(p => p.NotaVenta)
                    .HasForeignKey(d => d.IdUbicacionVenta)
                    .HasConstraintName("FK_NotaVenta_Adquiriente");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.NotaVentaIdUsuarioAltaNavigation)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaVenta_UsuarioAlta");

                entity.HasOne(d => d.IdUsuarioClienteNavigation)
                    .WithMany(p => p.NotaVentaIdUsuarioClienteNavigation)
                    .HasForeignKey(d => d.IdUsuarioCliente)
                    .HasConstraintName("FK__NotaVenta__IdUsu__3DF31CAF");
            });

            modelBuilder.Entity<NotaVentaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdNotaVentaDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Descuento).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Impuesto).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Lote).HasMaxLength(50);

                entity.Property(e => e.PrecioBase).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.NotaVentaDetalle)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__NotaVenta__IdCon__3EE740E8");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.NotaVentaDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaVentaDetalle_Impuesto");

                entity.HasOne(d => d.IdNotaVentaNavigation)
                    .WithMany(p => p.NotaVentaDetalleIdNotaVentaNavigation)
                    .HasForeignKey(d => d.IdNotaVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaVentaDetalle_NotaVenta");

                entity.HasOne(d => d.IdNotaVentaPadreDevolucionNavigation)
                    .WithMany(p => p.NotaVentaDetalleIdNotaVentaPadreDevolucionNavigation)
                    .HasForeignKey(d => d.IdNotaVentaPadreDevolucion)
                    .HasConstraintName("FK__NotaVenta__IdNot__1CFC3D38");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.NotaVentaDetalle)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaVentaDetalle_Presentacion");

                entity.HasOne(d => d.IdTipoDescuentoNavigation)
                    .WithMany(p => p.NotaVentaDetalle)
                    .HasForeignKey(d => d.IdTipoDescuento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaVentaDetalle_TipoDescuento");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.NotaVentaDetalle)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_NotaVentaDetalle_Ubicacion");

                entity.HasOne(d => d.IdUsuarioMedicoNavigation)
                    .WithMany(p => p.NotaVentaDetalle)
                    .HasForeignKey(d => d.IdUsuarioMedico)
                    .HasConstraintName("FK__NotaVenta__IdUsu__1DF06171");
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion)
                    .HasName("PK__Notifica__F6CA0A850829CF10");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Mensaje).HasMaxLength(500);

                entity.Property(e => e.Titulo).HasMaxLength(1000);

                entity.HasOne(d => d.IdTipoNotificacionNavigation)
                    .WithMany(p => p.Notificacion)
                    .HasForeignKey(d => d.IdTipoNotificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoNotificacion_Notificacion");
            });

            modelBuilder.Entity<NotificacionDoctor>(entity =>
            {
                entity.HasKey(e => e.IdNotificacionDoctor)
                    .HasName("PK__Notifica__09846837BF6B6039");

                entity.HasOne(d => d.IdNotificacionNavigation)
                    .WithMany(p => p.NotificacionDoctor)
                    .HasForeignKey(d => d.IdNotificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificac__IdNot__548C6944");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.NotificacionDoctor)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificac__IdPac__55808D7D");
            });

            modelBuilder.Entity<NotificacionUsuario>(entity =>
            {
                entity.HasKey(e => e.IdNotificacionUsuario)
                    .HasName("PK__Notifica__5E4FD393AD1E6DD1");

                entity.HasOne(d => d.IdNotificacionNavigation)
                    .WithMany(p => p.NotificacionUsuario)
                    .HasForeignKey(d => d.IdNotificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificac__IdNot__0564AAC9");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.NotificacionUsuario)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificac__IdUsu__0658CF02");
            });

            modelBuilder.Entity<OpcionVenta>(entity =>
            {
                entity.HasKey(e => e.IdOpcionVenta)
                    .HasName("PK__OpcionVe__6BBE690A6A60449F");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<OrdenCompra>(entity =>
            {
                entity.HasKey(e => e.IdOrdenCompra);

                entity.Property(e => e.Concepto).HasMaxLength(50);

                entity.Property(e => e.FechaEmision).HasColumnType("datetime");

                entity.Property(e => e.FechaEntregaProveedor).HasColumnType("date");

                entity.Property(e => e.FechaRequerida).HasColumnType("date");

                entity.Property(e => e.Numero).HasMaxLength(50);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenCompra_Almacen");

                entity.HasOne(d => d.IdDomicilioNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdDomicilio)
                    .HasConstraintName("FK_OrdenCompra_Domicilio");

                entity.HasOne(d => d.IdEstatusOrdenCompraNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.IdEstatusOrdenCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenCompra_EstatusOrdenCompra");

                entity.HasOne(d => d.IdUsuarioCompradorNavigation)
                    .WithMany(p => p.OrdenCompraIdUsuarioCompradorNavigation)
                    .HasForeignKey(d => d.IdUsuarioComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenCompra_UsuarioComprador");

                entity.HasOne(d => d.IdUsuarioProveedorNavigation)
                    .WithMany(p => p.OrdenCompraIdUsuarioProveedorNavigation)
                    .HasForeignKey(d => d.IdUsuarioProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdenComp__IdUsu__2082B559");
            });

            modelBuilder.Entity<OrdenCompraDetalle>(entity =>
            {
                entity.HasKey(e => e.IdOrdenCompraDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.CantidadRecibida).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Impuesto).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.OrdenCompraDetalle)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenCompraDetalle_Articulo");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.OrdenCompraDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdenComp__IdImp__2176D992");

                entity.HasOne(d => d.IdOrdenCompraNavigation)
                    .WithMany(p => p.OrdenCompraDetalle)
                    .HasForeignKey(d => d.IdOrdenCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenCompraDetalle_OrdenCompra");
            });

            modelBuilder.Entity<OrdenImagenologia>(entity =>
            {
                entity.HasKey(e => e.IdOrdenImagenologia);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaProgramacion).HasColumnType("datetime");

                entity.Property(e => e.FirmaCoordinadorClinica).HasMaxLength(50);

                entity.Property(e => e.Indicaciones).HasMaxLength(500);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.PrecioEstudio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProcedenciaPaciente).HasMaxLength(50);

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.OrdenImagenologia)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenImagenologia_Cita");

                entity.HasOne(d => d.IdInstitucionEstudioNavigation)
                    .WithMany(p => p.OrdenImagenologia)
                    .HasForeignKey(d => d.IdInstitucionEstudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenImagenologia_Institucion");

                entity.HasOne(d => d.IdPresentacionEstudioNavigation)
                    .WithMany(p => p.OrdenImagenologia)
                    .HasForeignKey(d => d.IdPresentacionEstudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenImagenologia_PresentacionEstudio");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.OrdenImagenologia)
                    .HasForeignKey(d => d.IdRecibo)
                    .HasConstraintName("FK__OrdenImag__IdRec__679450C0");
            });

            modelBuilder.Entity<OrdenLaboratorio>(entity =>
            {
                entity.HasKey(e => e.IdOrdenLaboratorio);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaProgramacion).HasColumnType("datetime");

                entity.Property(e => e.FirmaCoordinadorClinica).HasMaxLength(50);

                entity.Property(e => e.Indicaciones).HasMaxLength(500);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.PrecioEstudio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProcedenciaPaciente).HasMaxLength(50);

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.OrdenLaboratorio)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenLaboratorio_Cita");

                entity.HasOne(d => d.IdInstitucionEstudioNavigation)
                    .WithMany(p => p.OrdenLaboratorio)
                    .HasForeignKey(d => d.IdInstitucionEstudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenLaboratorio_Institucion");

                entity.HasOne(d => d.IdPresentacionEstudioNavigation)
                    .WithMany(p => p.OrdenLaboratorio)
                    .HasForeignKey(d => d.IdPresentacionEstudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenLaboratorio_PresentacionEstudio");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.OrdenLaboratorio)
                    .HasForeignKey(d => d.IdRecibo)
                    .HasConstraintName("FK__OrdenLabo__IdRec__66A02C87");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente);

                entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);

                entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdEstatusPacienteNavigation)
                    .WithMany(p => p.Paciente)
                    .HasForeignKey(d => d.IdEstatusPaciente)
                    .HasConstraintName("FK_Paciente_EstatusPaciente");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.Paciente)
                    .HasForeignKey(d => d.IdExpediente)
                    .HasConstraintName("FK_Paciente_Expediente");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago);

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(50);

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumeroMovimientoBancario).HasMaxLength(200);

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdCaja)
                    .HasConstraintName("FK__Pago__IdCaja__4282C7A2");

                entity.HasOne(d => d.IdCajaTurnoNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdCajaTurno)
                    .HasConstraintName("FK_Pago_CajaTurno");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdFormaPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pago_FormaPago");

                entity.HasOne(d => d.IdNotaGastoNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdNotaGasto)
                    .HasConstraintName("FK__Pago__IdNotaGast__4376EBDB");

                entity.HasOne(d => d.IdTipoPagoNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdTipoPago)
                    .HasConstraintName("FK__Pago__IdTipoPago__39ED81A1");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .HasConstraintName("FK_Pago_UsuarioAlta");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Parentesco>(entity =>
            {
                entity.HasKey(e => e.IdParentesco);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(12);

                entity.Property(e => e.OpenpayIdTransferencia).HasMaxLength(100);

                entity.Property(e => e.PaypalIdTransferencia).HasMaxLength(100);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK__Pedido__IdArea__15261146");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Pedido__IdCompan__2803DB90");

                entity.HasOne(d => d.IdDomicilioNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdDomicilio)
                    .HasConstraintName("FK__Pedido__IdDomici__1BF30A66");

                entity.HasOne(d => d.IdEstatusPedidoNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdEstatusPedido)
                    .HasConstraintName("FK_Pedido_EstatusPedido");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdFormaPago)
                    .HasConstraintName("FK_Pedido_FormaPago");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdPago)
                    .HasConstraintName("FK__Pedido__IdPago__21ABE3BC");
            });

            modelBuilder.Entity<PedidoBitacora>(entity =>
            {
                entity.HasKey(e => e.IdPedidoBitacora)
                    .HasName("PK__PedidoBi__1AEBA67379D4D85C");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdPedidoPresentacionNavigation)
                    .WithMany(p => p.PedidoBitacora)
                    .HasForeignKey(d => d.IdPedidoPresentacion)
                    .HasConstraintName("FK__PedidoBit__IdPed__2A4129BD");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.PedidoBitacora)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__PedidoBit__IdUsu__133DC8D4");
            });

            modelBuilder.Entity<PedidoPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdPedidoPresentacion);

                entity.Property(e => e.IdPedidoPresentacion).HasColumnName("ID_PedidoPresentacion");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ComentarioCalificacion).HasMaxLength(500);

                entity.Property(e => e.Comentarios).HasMaxLength(500);

                entity.Property(e => e.Precio).HasColumnType("numeric(18, 5)");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK__PedidoPre__IdAre__446B1014");

                entity.HasOne(d => d.IdEstatusPedidoNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdEstatusPedido)
                    .HasConstraintName("FK__PedidoPre__IdEst__455F344D");

                entity.HasOne(d => d.IdFlujoDetalleAplicadoNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdFlujoDetalleAplicado)
                    .HasConstraintName("FK__PedidoPre__IdFlu__5031C87B");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoPresentacion_Pedido");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoPresentacion_Presentacion");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__PedidoPre__IdRol__3D89085B");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoPresentacion_Sucursal");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.PedidoPresentacion)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .HasConstraintName("FK__PedidoPre__IdUsu__1DDB52D8");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil);

                entity.Property(e => e.Clave).HasMaxLength(7);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Perfil)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Perfil_Compania");

                entity.HasOne(d => d.IdJerarquiaAccesoNavigation)
                    .WithMany(p => p.Perfil)
                    .HasForeignKey(d => d.IdJerarquiaAcceso)
                    .HasConstraintName("FK__Perfil__IdJerarq__0781FD65");

                entity.HasOne(d => d.IdTipoCompaniaNavigation)
                    .WithMany(p => p.Perfil)
                    .HasForeignKey(d => d.IdTipoCompania)
                    .HasConstraintName("FK__Perfil__IdTipoCo__78A9CE29");
            });

            modelBuilder.Entity<Plantilla>(entity =>
            {
                entity.HasKey(e => e.IdPlantilla)
                    .HasName("PK__Plantill__F2E097D76FEAB996");

                entity.Property(e => e.IdPlantilla).HasColumnName("idPlantilla");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Formato)
                    .HasColumnType("text")
                    .HasColumnName("formato");
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasKey(e => e.IdPoliza)
                    .HasName("PK__Poliza__8E3943B3ED7C0E36");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaMovimiento).HasColumnType("date");

                entity.Property(e => e.Identificador).HasMaxLength(10);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.Origen).HasMaxLength(500);

                entity.Property(e => e.TipoCambio).HasColumnType("money");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Poliza)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Poliza__IdCompan__735B0927");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.Poliza)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Poliza__IdMoneda__744F2D60");

                entity.HasOne(d => d.IdPolizaReversaNavigation)
                    .WithMany(p => p.InverseIdPolizaReversaNavigation)
                    .HasForeignKey(d => d.IdPolizaReversa)
                    .HasConstraintName("FK__Poliza__IdPoliza__75435199");

                entity.HasOne(d => d.IdTipoPolizaNavigation)
                    .WithMany(p => p.Poliza)
                    .HasForeignKey(d => d.IdTipoPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Poliza__IdTipoPo__7266E4EE");

                entity.HasOne(d => d.IdVersionPolizaNavigation)
                    .WithMany(p => p.Poliza)
                    .HasForeignKey(d => d.IdVersionPoliza)
                    .HasConstraintName("FK__Poliza__IdVersio__15B0212B");
            });

            modelBuilder.Entity<PolizaAplicada>(entity =>
            {
                entity.HasKey(e => e.IdPolizaAplicada)
                    .HasName("PK__PolizaAp__DD803A06F4FF3EA0");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.FechaMovimiento).HasColumnType("date");

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.PolizaAplicada)
                    .HasForeignKey(d => d.IdPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaApl__IdPol__7913E27D");

                entity.HasOne(d => d.IdTipoPolizaNavigation)
                    .WithMany(p => p.PolizaAplicada)
                    .HasForeignKey(d => d.IdTipoPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaApl__IdTip__781FBE44");
            });

            modelBuilder.Entity<PolizaAplicadaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdPolizaAplicadaDetalle)
                    .HasName("PK__PolizaAp__3ABB07EB251FC74B");

                entity.Property(e => e.Abono).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Cargo).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Concepto).HasMaxLength(500);

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.PolizaAplicadaDetalle)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__PolizaApl__IdAux__0B3292B8");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.PolizaAplicadaDetalle)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaApl__IdCue__0A3E6E7F");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.PolizaAplicadaDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("FK__PolizaApl__IdImp__0D1ADB2A");

                entity.HasOne(d => d.IdImpuestoDetalleNavigation)
                    .WithMany(p => p.PolizaAplicadaDetalle)
                    .HasForeignKey(d => d.IdImpuestoDetalle)
                    .HasConstraintName("FK__PolizaApl__IdImp__0C26B6F1");

                entity.HasOne(d => d.IdPartidaVivaNavigation)
                    .WithMany(p => p.InverseIdPartidaVivaNavigation)
                    .HasForeignKey(d => d.IdPartidaViva)
                    .HasConstraintName("FK__PolizaApl__IdPar__0E0EFF63");

                entity.HasOne(d => d.IdPolizaAplicadaNavigation)
                    .WithMany(p => p.PolizaAplicadaDetalle)
                    .HasForeignKey(d => d.IdPolizaAplicada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaApl__IdPol__094A4A46");

                entity.HasOne(d => d.IdPolizaDetalleNavigation)
                    .WithMany(p => p.PolizaAplicadaDetalle)
                    .HasForeignKey(d => d.IdPolizaDetalle)
                    .HasConstraintName("FK__PolizaApl__IdPol__16A44564");
            });

            modelBuilder.Entity<PolizaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdPolizaDetalle)
                    .HasName("PK__PolizaDe__FFCE0242E58C6512");

                entity.Property(e => e.Abono).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Cargo).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Concepto).HasMaxLength(500);

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.PolizaDetalle)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__PolizaDet__IdAux__04859529");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.PolizaDetalle)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaDet__IdCue__039170F0");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.PolizaDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("FK__PolizaDet__IdImp__066DDD9B");

                entity.HasOne(d => d.IdImpuestoDetalleNavigation)
                    .WithMany(p => p.PolizaDetalle)
                    .HasForeignKey(d => d.IdImpuestoDetalle)
                    .HasConstraintName("FK__PolizaDet__IdImp__0579B962");

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.PolizaDetalle)
                    .HasForeignKey(d => d.IdPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaDet__IdPol__029D4CB7");
            });

            modelBuilder.Entity<PolizaOrigen>(entity =>
            {
                entity.HasKey(e => e.IdPolizaOrigen)
                    .HasName("PK__PolizaOr__6BDFFF89020FC41B");

                entity.Property(e => e.Origen).HasMaxLength(500);

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.PolizaOrigen)
                    .HasForeignKey(d => d.IdPoliza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PolizaOri__IdPol__7FC0E00C");
            });

            modelBuilder.Entity<Presentacion>(entity =>
            {
                entity.HasKey(e => e.IdPresentacion);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Indicaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(256);

                entity.Property(e => e.Sku).HasMaxLength(50);

                entity.Property(e => e.Upc).HasMaxLength(50);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_Presentacion_Area");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Presentac__IdCom__30CE2BBB");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__Presentac__IdCon__3FDB6521");

                entity.HasOne(d => d.IdFlujoNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.IdFlujo)
                    .HasConstraintName("FK__Presentac__IdFlu__0AFD888E");

                entity.HasOne(d => d.IdTipoPresentacionNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.IdTipoPresentacion)
                    .HasConstraintName("FK_Presentacion_TipoPresentacion");
            });

            modelBuilder.Entity<PresentacionArticulo>(entity =>
            {
                entity.HasKey(e => e.IdPresentacionArticulo);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.PresentacionArticulo)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PresentacionArticulo_Articulo");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.PresentacionArticulo)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PresentacionArticulo_Presentacion");
            });

            modelBuilder.Entity<PresentacionImagen>(entity =>
            {
                entity.HasKey(e => e.IdPresentacionImagen);

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.TipoMime).HasMaxLength(50);

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.PresentacionImagen)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PresentacionImagen_Presentacion");
            });

            modelBuilder.Entity<ProgramacionExamen>(entity =>
            {
                entity.HasKey(e => e.IdProgramacionExamen)
                    .HasName("PK__Programa__6EF8F9C5339054C3");

                entity.ToTable("ProgramacionExamen", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaExamen).HasColumnType("datetime");

                entity.HasOne(d => d.IdProyectoElementoTecnicaNavigation)
                    .WithMany(p => p.ProgramacionExamen)
                    .HasForeignKey(d => d.IdProyectoElementoTecnica)
                    .HasConstraintName("FK__Programac__IdPro__55B597A7");

                entity.HasOne(d => d.IdTipoExamenNavigation)
                    .WithMany(p => p.ProgramacionExamen)
                    .HasForeignKey(d => d.IdTipoExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Programac__IdTip__53CD4F35");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.ProgramacionExamen)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Programac__IdUsu__54C1736E");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.Property(e => e.Calle).HasMaxLength(100);

                entity.Property(e => e.CodigoPostal).HasMaxLength(5);

                entity.Property(e => e.Colonia).HasMaxLength(50);

                entity.Property(e => e.Contacto).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Localidad).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NumeroExterior).HasMaxLength(6);

                entity.Property(e => e.NumeroInterior).HasMaxLength(6);

                entity.Property(e => e.TelefonoDos).HasMaxLength(15);

                entity.Property(e => e.TelefonoUno).HasMaxLength(15);

                entity.Property(e => e.UrlMapa).HasMaxLength(500);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Proveedor__IdCom__2C09769E");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proveedor_Estado");

                entity.HasOne(d => d.IdTipoProveedorNavigation)
                    .WithMany(p => p.Proveedor)
                    .HasForeignKey(d => d.IdTipoProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proveedor_TipoProveedor");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PK__Proyecto__F4888673578B14F6");

                entity.ToTable("Proyecto", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGuiaNavigation)
                    .WithMany(p => p.Proyecto)
                    .HasForeignKey(d => d.IdGuia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proyecto__IdGuia__28E2F130");

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.Proyecto)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proyecto__IdLoca__27EECCF7");

                entity.HasOne(d => d.IdUsuarioAdministradorNavigation)
                    .WithMany(p => p.ProyectoIdUsuarioAdministradorNavigation)
                    .HasForeignKey(d => d.IdUsuarioAdministrador)
                    .HasConstraintName("FK__Proyecto__IdUsua__009FF5AC");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.ProyectoIdUsuarioAltaNavigation)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proyecto__IdUsua__29D71569");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.ProyectoIdUsuarioResponsableNavigation)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proyecto__IdUsua__2ACB39A2");
            });

            modelBuilder.Entity<ProyectoActividad>(entity =>
            {
                entity.HasKey(e => e.IdProyectoActividad)
                    .HasName("PK__Proyecto__21ABEB69FA5CFAEA");

                entity.ToTable("ProyectoActividad", "Proyectos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.HasOne(d => d.IdFlujoNavigation)
                    .WithMany(p => p.ProyectoActividad)
                    .HasForeignKey(d => d.IdFlujo)
                    .HasConstraintName("FK__ProyectoA__IdFlu__7BDB408F");

                entity.HasOne(d => d.IdFlujoDetalleAplicadoNavigation)
                    .WithMany(p => p.ProyectoActividad)
                    .HasForeignKey(d => d.IdFlujoDetalleAplicado)
                    .HasConstraintName("FK__ProyectoA__IdFlu__0B1D841F");

                entity.HasOne(d => d.IdProyectoElementoTecnicaNavigation)
                    .WithMany(p => p.ProyectoActividad)
                    .HasForeignKey(d => d.IdProyectoElementoTecnica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdPro__31783731");

                entity.HasOne(d => d.IdProyectoEstatusNavigation)
                    .WithMany(p => p.ProyectoActividad)
                    .HasForeignKey(d => d.IdProyectoEstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdPro__326C5B6A");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                    .WithMany(p => p.ProyectoActividad)
                    .HasForeignKey(d => d.IdUsuarioResponsable)
                    .HasConstraintName("FK__ProyectoA__IdUsu__33607FA3");
            });

            modelBuilder.Entity<ProyectoActividadEvidencia>(entity =>
            {
                entity.HasKey(e => e.IdProyectoActividadEvidencia)
                    .HasName("PK__Proyecto__1F92B0141851B2CC");

                entity.ToTable("ProyectoActividadEvidencia", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProyectoActividadNavigation)
                    .WithMany(p => p.ProyectoActividadEvidencia)
                    .HasForeignKey(d => d.IdProyectoActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdPro__363CEC4E");
            });

            modelBuilder.Entity<ProyectoActividadEvidenciaArchivo>(entity =>
            {
                entity.HasKey(e => e.IdProyectoActividadEvidenciaArchivo)
                    .HasName("PK__Proyecto__FE7BC01E6C912907");

                entity.ToTable("ProyectoActividadEvidenciaArchivo", "Proyectos");

                entity.Property(e => e.Archivo).HasColumnType("image");

                entity.Property(e => e.ArchivoNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoTipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProyectoActividadEvidenciaNavigation)
                    .WithMany(p => p.ProyectoActividadEvidenciaArchivo)
                    .HasForeignKey(d => d.IdProyectoActividadEvidencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdPro__391958F9");
            });

            modelBuilder.Entity<ProyectoActividadHora>(entity =>
            {
                entity.HasKey(e => e.IdProyectoActividadHora)
                    .HasName("PK__Proyecto__1B7A042AAC6C1A69");

                entity.ToTable("ProyectoActividadHora", "Proyectos");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.HasOne(d => d.IdProyectoActividadNavigation)
                    .WithMany(p => p.ProyectoActividadHora)
                    .HasForeignKey(d => d.IdProyectoActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdPro__3BF5C5A4");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.ProyectoActividadHora)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdUsu__3CE9E9DD");
            });

            modelBuilder.Entity<ProyectoActividadParticipante>(entity =>
            {
                entity.HasKey(e => e.IdProyectoActividadParticipante)
                    .HasName("PK__Proyecto__50C0CC25902947AA");

                entity.ToTable("ProyectoActividadParticipante", "Proyectos");

                entity.HasOne(d => d.IdProyectoActividadNavigation)
                    .WithMany(p => p.ProyectoActividadParticipante)
                    .HasForeignKey(d => d.IdProyectoActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdPro__3FC65688");

                entity.HasOne(d => d.IdUsuarioParticipanteNavigation)
                    .WithMany(p => p.ProyectoActividadParticipante)
                    .HasForeignKey(d => d.IdUsuarioParticipante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoA__IdUsu__40BA7AC1");
            });

            modelBuilder.Entity<ProyectoElementoTecnica>(entity =>
            {
                entity.HasKey(e => e.IdProyectoElementoTecnica)
                    .HasName("PK__Proyecto__EC4B6B040507E546");

                entity.ToTable("ProyectoElementoTecnica", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Elemento)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Tecnica)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ProyectoElementoTecnica)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoE__IdPro__2DA7A64D");

                entity.HasOne(d => d.IdProyectoEstatusNavigation)
                    .WithMany(p => p.ProyectoElementoTecnica)
                    .HasForeignKey(d => d.IdProyectoEstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectoE__IdPro__2E9BCA86");
            });

            modelBuilder.Entity<ProyectoEstatus>(entity =>
            {
                entity.HasKey(e => e.IdProyectoEstatus)
                    .HasName("PK__Proyecto__3E819E8C04A51D52");

                entity.ToTable("ProyectoEstatus", "Proyectos");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PuntoVenta>(entity =>
            {
                entity.HasKey(e => e.IdPuntoVenta);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.PuntoVenta)
                    .HasForeignKey(d => d.IdAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_PuntoVenta_Almacen");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.PuntoVenta)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__PuntoVent__IdCon__28C2F59F");

                entity.HasOne(d => d.IdTipoPuntoVentaNavigation)
                    .WithMany(p => p.PuntoVenta)
                    .HasForeignKey(d => d.IdTipoPuntoVenta)
                    .HasConstraintName("FK__PuntoVent__IdTip__218BE82B");

                entity.HasOne(d => d.IdUbicacionVentaNavigation)
                    .WithMany(p => p.PuntoVenta)
                    .HasForeignKey(d => d.IdUbicacionVenta)
                    .HasConstraintName("FK_PuntoVenta_Adquiriente");
            });

            modelBuilder.Entity<Reactivo>(entity =>
            {
                entity.HasKey(e => e.IdReactivo)
                    .HasName("PK__Reactivo__6BD920EB1D925DC7");

                entity.ToTable("Reactivo", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.ImagenNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenTipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Pregunta)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Respuesta).HasColumnType("text");

                entity.Property(e => e.RespuestaCorrecta)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAsignaturaNavigation)
                    .WithMany(p => p.Reactivo)
                    .HasForeignKey(d => d.IdAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reactivo__IdAsig__58920452");

                entity.HasOne(d => d.IdNivelExamenNavigation)
                    .WithMany(p => p.Reactivo)
                    .HasForeignKey(d => d.IdNivelExamen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reactivo__IdNive__5986288B");
            });

            modelBuilder.Entity<Recepcion>(entity =>
            {
                entity.HasKey(e => e.IdRecepcion)
                    .HasName("PK__Recepcio__822118391767E3B1");

                entity.Property(e => e.IdRecepcion).HasColumnName("idRecepcion");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("date")
                    .HasColumnName("fechaRecepcion");

                entity.Property(e => e.Folio)
                    .HasMaxLength(20)
                    .HasColumnName("folio");

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.IdEstadoProducto).HasColumnName("idEstadoProducto");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.HasOne(d => d.IdEstadoProductoNavigation)
                    .WithMany(p => p.Recepcion)
                    .HasForeignKey(d => d.IdEstadoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recepcion__idEst__70B3A6A6");

                entity.HasOne(d => d.IdLiquidacionNavigation)
                    .WithMany(p => p.Recepcion)
                    .HasForeignKey(d => d.IdLiquidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recepcion__idLiq__6FBF826D");
            });

            modelBuilder.Entity<RecepcionPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdRecepcionPresentacion)
                    .HasName("PK__Recepcio__641C814C1F37ADB1");

                entity.Property(e => e.IdRecepcionPresentacion).HasColumnName("idRecepcionPresentacion");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdPresentacion).HasColumnName("idPresentacion");

                entity.Property(e => e.IdRecepcion).HasColumnName("idRecepcion");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.RecepcionPresentacion)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recepcion__idPre__7484378A");

                entity.HasOne(d => d.IdRecepcionNavigation)
                    .WithMany(p => p.RecepcionPresentacion)
                    .HasForeignKey(d => d.IdRecepcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recepcion__idRec__73901351");
            });

            modelBuilder.Entity<Receta>(entity =>
            {
                entity.HasKey(e => e.IdReceta);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.Receta)
                    .HasForeignKey(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receta_Cita");
            });

            modelBuilder.Entity<RecetaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdRecetaDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Dosis).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Duracion).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Frecuencia).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Indicaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.RecetaDetalle)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecetaDetalle_Presentacion");

                entity.HasOne(d => d.IdRecetaNavigation)
                    .WithMany(p => p.RecetaDetalle)
                    .HasForeignKey(d => d.IdReceta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecetaDetalle_Receta");
            });

            modelBuilder.Entity<Recibo>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.Property(e => e.DescuentoAdicional).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaContable).HasColumnType("date");

                entity.Property(e => e.Folio).HasMaxLength(10);

                entity.Property(e => e.MotivoDescuentoAdicional).HasMaxLength(500);

                entity.Property(e => e.NumeroMovimientoBancario).HasMaxLength(200);

                entity.Property(e => e.Saldo).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.ValorCapturadoDescuentoAdicional).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recibo_Area");

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdCaja)
                    .HasConstraintName("FK_Recibo_Caja");

                entity.HasOne(d => d.IdEstatusPagoNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdEstatusPago)
                    .HasConstraintName("FK_Recibo_EstatusPago");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdHospital)
                    .HasConstraintName("FK__Recibo__IdHospit__0BD1B136");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("FK_Recibo_Paciente");

                entity.HasOne(d => d.IdTipoReciboNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdTipoRecibo)
                    .HasConstraintName("FK__Recibo__IdTipoRe__1C0818FF");

                entity.HasOne(d => d.IdUbicacionVentaNavigation)
                    .WithMany(p => p.Recibo)
                    .HasForeignKey(d => d.IdUbicacionVenta)
                    .HasConstraintName("FK_Recibo_Adquiriente");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.ReciboIdUsuarioAltaNavigation)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recibo_UsuarioAlta");

                entity.HasOne(d => d.IdUsuarioCajaNavigation)
                    .WithMany(p => p.ReciboIdUsuarioCajaNavigation)
                    .HasForeignKey(d => d.IdUsuarioCaja)
                    .HasConstraintName("FK_Recibo_UsuarioCaja");

                entity.HasOne(d => d.IdUsuarioClienteNavigation)
                    .WithMany(p => p.ReciboIdUsuarioClienteNavigation)
                    .HasForeignKey(d => d.IdUsuarioCliente)
                    .HasConstraintName("FK__Recibo__IdUsuari__5125ECB4");
            });

            modelBuilder.Entity<ReciboDetalle>(entity =>
            {
                entity.HasKey(e => e.IdReciboDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Descuento).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Impuesto).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.PrecioBase).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.ReciboDetalle)
                    .HasForeignKey(d => d.IdImpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReciboDetalle_Impuesto");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.ReciboDetalle)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReciboDetalle_Presentacion");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.ReciboDetalle)
                    .HasForeignKey(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReciboDetalle_Recibo");

                entity.HasOne(d => d.IdTipoDescuentoNavigation)
                    .WithMany(p => p.ReciboDetalle)
                    .HasForeignKey(d => d.IdTipoDescuento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReciboDetalle_TipoDescuento");
            });

            modelBuilder.Entity<ReciboEnglobado>(entity =>
            {
                entity.HasKey(e => e.IdReciboEnglobado)
                    .HasName("IdReciboEnglobado");

                entity.Property(e => e.DescuentoGlobal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(50);

                entity.Property(e => e.MotivoDescuento).HasMaxLength(500);

                entity.Property(e => e.ValorCapturadoDescuentoGlobal).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdUsuarioAltaNavigation)
                    .WithMany(p => p.ReciboEnglobado)
                    .HasForeignKey(d => d.IdUsuarioAlta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReciboEng__IdUsu__08012052");
            });

            modelBuilder.Entity<ReciboPago>(entity =>
            {
                entity.HasKey(e => e.IdReciboPago);

                entity.HasOne(d => d.IdNotaFlujoNavigation)
                    .WithMany(p => p.ReciboPago)
                    .HasForeignKey(d => d.IdNotaFlujo)
                    .HasConstraintName("FK__ReciboPag__IdNot__4D555BD0");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.ReciboPago)
                    .HasForeignKey(d => d.IdPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReciboPago_Pago");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.ReciboPago)
                    .HasForeignKey(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReciboPago_Recibo");
            });

            modelBuilder.Entity<Recordatorio>(entity =>
            {
                entity.HasKey(e => e.IdRecordatorio);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Recordatorio)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recordatorio_Usuario");
            });

            modelBuilder.Entity<RegimenFiscal>(entity =>
            {
                entity.HasKey(e => e.IdRegimenFiscal);

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Remision>(entity =>
            {
                entity.HasKey(e => e.IdRemision)
                    .HasName("PK__Remision__D2D9FAC4FC8899AF");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.Folio)
                    .HasMaxLength(35)
                    .HasColumnName("folio");

                entity.Property(e => e.Habilitado).HasColumnName("habilitado");

                entity.Property(e => e.IdDomicilioSucursal).HasColumnName("idDomicilioSucursal");

                entity.Property(e => e.IdEstatusRemision).HasColumnName("idEstatusRemision");

                entity.Property(e => e.IdLiquidacion).HasColumnName("idLiquidacion");

                entity.HasOne(d => d.IdDomicilioSucursalNavigation)
                    .WithMany(p => p.Remision)
                    .HasForeignKey(d => d.IdDomicilioSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Remision__idDomi__1328BA3B");

                entity.HasOne(d => d.IdEstatusRemisionNavigation)
                    .WithMany(p => p.Remision)
                    .HasForeignKey(d => d.IdEstatusRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Remision__idEsta__672A3C6C");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.RemisionNavigation)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK__Remision__IdFact__2453463D");

                entity.HasOne(d => d.IdLiquidacionNavigation)
                    .WithMany(p => p.Remision)
                    .HasForeignKey(d => d.IdLiquidacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Remision__idLiqu__6541F3FA");

                entity.HasOne(d => d.IdMetodoPagoNavigation)
                    .WithMany(p => p.Remision)
                    .HasForeignKey(d => d.IdMetodoPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Remision__IdMeto__1E9A6CE7");
            });

            modelBuilder.Entity<RemisionPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdRemisionPresentacion)
                    .HasName("PK__Remision__C5F834601699744E");

                entity.Property(e => e.IdRemisionPresentacion).HasColumnName("idRemisionPresentacion");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.IdPresentacion).HasColumnName("idPresentacion");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.PrecioBase).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.RemisionPresentacion)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("FK__RemisionP__IdImp__1F8E9120");

                entity.HasOne(d => d.IdPresentacionNavigation)
                    .WithMany(p => p.RemisionPresentacion)
                    .HasForeignKey(d => d.IdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RemisionP__idPre__6AFACD50");

                entity.HasOne(d => d.IdRemisionNavigation)
                    .WithMany(p => p.RemisionPresentacion)
                    .HasForeignKey(d => d.IdRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RemisionP__idRem__6A06A917");
            });

            modelBuilder.Entity<RestablecerContrasena>(entity =>
            {
                entity.HasKey(e => e.IdRestablecerContrasena);

                entity.Property(e => e.Clave).HasMaxLength(500);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.RestablecerContrasena)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestablecerContrasena_Usuario");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Rol)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__Rol__IdCompania__5A5A5133");
            });

            modelBuilder.Entity<RolAcceso>(entity =>
            {
                entity.HasKey(e => e.IdRolAcceso)
                    .HasName("PK__RolAcces__D98AE388F293E4C9");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<SatFormaPago>(entity =>
            {
                entity.HasKey(e => e.IdSatFormaPago)
                    .HasName("PK__SatForma__B215187AA47E1F8B");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            modelBuilder.Entity<SatMetodoPago>(entity =>
            {
                entity.HasKey(e => e.IdSatMetodoPago)
                    .HasName("PK__SatMetod__49B81901696D92B6");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            modelBuilder.Entity<SatMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdSatMovimiento)
                    .HasName("PK__SatMovim__972DD1E62C098CD1");

                entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(500);

                entity.Property(e => e.LugarExpedicion).HasMaxLength(20);

                entity.Property(e => e.NombreEmisor).HasMaxLength(500);

                entity.Property(e => e.RfcEmisor).HasMaxLength(500);

                entity.Property(e => e.RfcReceptor).HasMaxLength(500);

                entity.Property(e => e.TipoComprobante).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Uuid).HasMaxLength(500);

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.SatMovimiento)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SatMovimi__IdLoc__59BB32B5");

                entity.HasOne(d => d.IdRegimenFiscalEmisorNavigation)
                    .WithMany(p => p.SatMovimiento)
                    .HasForeignKey(d => d.IdRegimenFiscalEmisor)
                    .HasConstraintName("FK__SatMovimi__IdReg__625078B6");
            });

            modelBuilder.Entity<SatMovimientoConcepto>(entity =>
            {
                entity.HasKey(e => e.IdSatMovimientoConcepto)
                    .HasName("PK__SatMovim__97F41903A21742FE");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Importe).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.MontoUnitario).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.IdSatMovimientoNavigation)
                    .WithMany(p => p.SatMovimientoConcepto)
                    .HasForeignKey(d => d.IdSatMovimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SatMovimi__IdSat__5C979F60");
            });

            modelBuilder.Entity<SatProductoServicio>(entity =>
            {
                entity.HasKey(e => e.IdSatProductoServicio)
                    .HasName("PK__SatProdu__A7908B7CFFC12AD5");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(500);
            });

            modelBuilder.Entity<SatProductoServicioCartaPorte>(entity =>
            {
                entity.HasKey(e => e.IdSatProductoServicioCartaPorte)
                    .HasName("PK__SatProdu__589242B01BC191B9");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<SatSolicitud>(entity =>
            {
                entity.HasKey(e => e.IdSatSolicitud)
                    .HasName("PK__SatSolic__0F0EF767840513C6");

                entity.Property(e => e.FechaFinal).HasColumnType("datetime");

                entity.Property(e => e.FechaInicial).HasColumnType("datetime");

                entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");

                entity.Property(e => e.FechaVerificacion).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(100);

                entity.Property(e => e.RfcSolicitante).HasMaxLength(50);

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.SatSolicitud)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SatSolici__IdLoc__56DEC60A");
            });

            modelBuilder.Entity<SatTipoComprobante>(entity =>
            {
                entity.HasKey(e => e.IdSatTipoComprobante)
                    .HasName("PK__SatTipoC__9B6AA2DA4D64F916");

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<SatTipoFactor>(entity =>
            {
                entity.HasKey(e => e.IdSatTipoFactor)
                    .HasName("PK__SatTipoF__582BC20ADF8A8590");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            modelBuilder.Entity<SatUnidad>(entity =>
            {
                entity.HasKey(e => e.IdSatUnidad)
                    .HasName("PK__SatUnida__CEE89D6DB7FA88E0");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(500);
            });

            modelBuilder.Entity<ScriptCambio>(entity =>
            {
                entity.HasKey(e => e.IdScriptCambio);

                entity.Property(e => e.FechaEjecucion).HasColumnType("datetime");
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.HasKey(e => e.IdSeccion)
                    .HasName("PK__Seccion__CD2B049F59C34F5E");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<SeccionCampo>(entity =>
            {
                entity.HasKey(e => e.IdSeccionCampo)
                    .HasName("PK__SeccionC__A4CF246CF505A904");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.Grupo).HasMaxLength(50);

                entity.Property(e => e.MostrarDashboard).HasColumnName("mostrarDashboard");

                entity.HasOne(d => d.IdDominioNavigation)
                    .WithMany(p => p.SeccionCampo)
                    .HasForeignKey(d => d.IdDominio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SeccionCa__IdDom__6F7569AA");

                entity.HasOne(d => d.IdIconoNavigation)
                    .WithMany(p => p.SeccionCampo)
                    .HasForeignKey(d => d.IdIcono)
                    .HasConstraintName("SeccionCampo_Icono");

                entity.HasOne(d => d.IdSeccionNavigation)
                    .WithMany(p => p.SeccionCampo)
                    .HasForeignKey(d => d.IdSeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SeccionCa__IdSec__70698DE3");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<SubtipoCuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdSubtipoCuentaContable);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdTipoCuentaContableNavigation)
                    .WithMany(p => p.SubtipoCuentaContable)
                    .HasForeignKey(d => d.IdTipoCuentaContable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubtipoCuentaContable_TipoCuentaContable");
            });

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.HasKey(e => e.IdTarjeta);

                entity.Property(e => e.AnioVencimiento).HasMaxLength(4);

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.Property(e => e.MesVencimiento).HasMaxLength(2);

                entity.Property(e => e.OpenpayIdTarjeta).HasMaxLength(50);

                entity.Property(e => e.TerminacionNumero).HasMaxLength(4);

                entity.Property(e => e.Titular).HasMaxLength(100);

                entity.HasOne(d => d.IdUsuarioCompradorNavigation)
                    .WithMany(p => p.Tarjeta)
                    .HasForeignKey(d => d.IdUsuarioComprador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tarjeta_UsuarioComprador");
            });

            modelBuilder.Entity<TipoAcceso>(entity =>
            {
                entity.HasKey(e => e.IdTipoAcceso);

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoActivo>(entity =>
            {
                entity.HasKey(e => e.IdTipoActivo)
                    .HasName("PK__TipoActi__10ABF1158F53643B");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoActivo)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoActiv__IdCom__409A7F30");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.TipoActivo)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .HasConstraintName("FK__TipoActiv__IdCue__3FA65AF7");
            });

            modelBuilder.Entity<TipoArticulo>(entity =>
            {
                entity.HasKey(e => e.IdTipoArticulo);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoAuxiliar>(entity =>
            {
                entity.HasKey(e => e.IdTipoAuxiliar)
                    .HasName("PK__TipoAuxi__62F2145BCE8BD198");

                entity.Property(e => e.Clave).HasMaxLength(2);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.HasOne(d => d.IdTipoCuentaContableNavigation)
                    .WithMany(p => p.TipoAuxiliar)
                    .HasForeignKey(d => d.IdTipoCuentaContable)
                    .HasConstraintName("FK__TipoAuxil__IdTip__4AADF94F");
            });

            modelBuilder.Entity<TipoCambio>(entity =>
            {
                entity.HasKey(e => e.IdTipoCambio)
                    .HasName("PK__TipoCamb__3D56BA43D1711352");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.Valor).HasColumnType("money");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoCambio)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoCambi__IdCom__52EE3995");

                entity.HasOne(d => d.IdMonedaNavigation)
                    .WithMany(p => p.TipoCambio)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoCambi__IdMon__51FA155C");
            });

            modelBuilder.Entity<TipoCliente>(entity =>
            {
                entity.HasKey(e => e.IdTipoCliente)
                    .HasName("PK__TipoClie__FE7DCABCCE3320CA");

                entity.Property(e => e.IdTipoCliente).HasColumnName("idTipoCliente");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdCompania).HasColumnName("idCompania");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoCliente)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoClien__idCom__3F1C4B12");
            });

            modelBuilder.Entity<TipoComision>(entity =>
            {
                entity.HasKey(e => e.IdTipoComision)
                    .HasName("PK_Comision");

                entity.Property(e => e.CifraControl).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Estatus).HasMaxLength(50);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoComision)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__TipoComis__IdCom__33AA9866");
            });

            modelBuilder.Entity<TipoComisionDetalle>(entity =>
            {
                entity.HasKey(e => e.IdTipoComisionDetalle);

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Porcentaje).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TipoComisionDetalle)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoComisionDetalle_Rol");

                entity.HasOne(d => d.IdTipoComisionNavigation)
                    .WithMany(p => p.TipoComisionDetalle)
                    .HasForeignKey(d => d.IdTipoComision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoComisionDetalle_TipoComision");
            });

            modelBuilder.Entity<TipoCompania>(entity =>
            {
                entity.HasKey(e => e.IdTipoCompania)
                    .HasName("PK__TipoComp__3367F94A22F545A7");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<TipoConcepto>(entity =>
            {
                entity.HasKey(e => e.IdTipoConcepto)
                    .HasName("PK__TipoConc__582DEADB70F20EBC");

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoCuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdTipoCuentaContable);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoDescuento>(entity =>
            {
                entity.HasKey(e => e.IdTipoDescuento)
                    .HasName("PK_Descuento");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Estatus).HasMaxLength(50);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoDescuento)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__TipoDescu__IdCom__349EBC9F");
            });

            modelBuilder.Entity<TipoDescuentoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdTipoDescuentoDetalle)
                    .HasName("PK_DescuentoDetalle");

                entity.Property(e => e.DiasSemana).HasMaxLength(50);

                entity.Property(e => e.Porcentaje).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdTipoDescuentoNavigation)
                    .WithMany(p => p.TipoDescuentoDetalle)
                    .HasForeignKey(d => d.IdTipoDescuento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoDescuentoDetalle_TipoDescuento");
            });

            modelBuilder.Entity<TipoDevolucion>(entity =>
            {
                entity.HasKey(e => e.IdTipoDevolucion)
                    .HasName("PK__TipoDevo__0A4907F10DCBA74F");

                entity.Property(e => e.IdTipoDevolucion).HasColumnName("idTipoDevolucion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TipoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdTipoEmpleado)
                    .HasName("PK__TipoEmpl__8034DC3BA982F7EF");

                entity.Property(e => e.IdTipoEmpleado).HasColumnName("idTipoEmpleado");

                entity.Property(e => e.Clave)
                    .HasMaxLength(3)
                    .HasColumnName("clave");

                entity.Property(e => e.IdCompania).HasColumnName("idCompania");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoEmpleado)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoEmple__idCom__4B8221F7");
            });

            modelBuilder.Entity<TipoExamen>(entity =>
            {
                entity.HasKey(e => e.IdTipoExamen)
                    .HasName("PK__TipoExam__FF2B2118F7FD9C28");

                entity.ToTable("TipoExamen", "Proyectos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoExpediente>(entity =>
            {
                entity.HasKey(e => e.IdTipoExpediente);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoExpedienteAdministrativo>(entity =>
            {
                entity.HasKey(e => e.IdTipoExpedienteAdministrativo)
                    .HasName("PK__TipoExpe__59A6B0CE54A71C80");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoExpedienteAdministrativo)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoExped__IdCom__40657506");
            });

            modelBuilder.Entity<TipoFlujo>(entity =>
            {
                entity.HasKey(e => e.IdTipoFlujo)
                    .HasName("PK__TipoFluj__636A46BC1134C553");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoGuia>(entity =>
            {
                entity.HasKey(e => e.IdTipoGuia)
                    .HasName("PK__TipoGuia__24358BAA9EAF8EAB");

                entity.ToTable("TipoGuia", "Proyectos");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoIdentificacion);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoImpuesto>(entity =>
            {
                entity.HasKey(e => e.IdTipoImpuesto);

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoMovimientoMaterial>(entity =>
            {
                entity.HasKey(e => e.IdTipoMovimientoMaterial);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoMovimientoMaterialCuentaContable>(entity =>
            {
                entity.HasKey(e => e.IdTipoMovimientoMaterialCuentaContable)
                    .HasName("PK__TipoMovi__D452BAD093E94632");

                entity.HasOne(d => d.IdCuentaContableAbonoNavigation)
                    .WithMany(p => p.TipoMovimientoMaterialCuentaContableIdCuentaContableAbonoNavigation)
                    .HasForeignKey(d => d.IdCuentaContableAbono)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoMovim__IdCue__3FFB60B2");

                entity.HasOne(d => d.IdCuentaContableCargoNavigation)
                    .WithMany(p => p.TipoMovimientoMaterialCuentaContableIdCuentaContableCargoNavigation)
                    .HasForeignKey(d => d.IdCuentaContableCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoMovim__IdCue__3F073C79");

                entity.HasOne(d => d.IdTipoMovimientoMaterialNavigation)
                    .WithMany(p => p.TipoMovimientoMaterialCuentaContable)
                    .HasForeignKey(d => d.IdTipoMovimientoMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoMovim__IdTip__3D1EF407");
            });

            modelBuilder.Entity<TipoMuestra>(entity =>
            {
                entity.HasKey(e => e.IdTipoMuestra);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoNotaGasto>(entity =>
            {
                entity.HasKey(e => e.IdTipoNotaGasto)
                    .HasName("PK__TipoNota__F63C5BAD889E99E9");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoNotaVenta>(entity =>
            {
                entity.HasKey(e => e.IdTipoNotaVenta)
                    .HasName("PK__TipoNota__B1F7C977F7E93A03");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoNotificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoNotificacion)
                    .HasName("PK__TipoNoti__0ECE0435F8C7AA48");

                entity.Property(e => e.IdTipoNotificacion).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.HasKey(e => e.IdTipoPago)
                    .HasName("PK__TipoPago__EB0AA9E738D3846C");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.TipoPago)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__TipoPago__IdConc__4F72AE6C");
            });

            modelBuilder.Entity<TipoPermisoTransporte>(entity =>
            {
                entity.HasKey(e => e.IdTipoPermisoTransporte)
                    .HasName("PK__TipoPerm__26A4C6866BAEBA9D");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(500);
            });

            modelBuilder.Entity<TipoPoliza>(entity =>
            {
                entity.HasKey(e => e.IdTipoPoliza)
                    .HasName("PK__TipoPoli__1BF2E5A7FD82484E");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoPoliza)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TipoPoliz__IdCom__4D35603F");
            });

            modelBuilder.Entity<TipoPresentacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoPresentacion);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoProveedor>(entity =>
            {
                entity.HasKey(e => e.IdTipoProveedor);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.TipoProveedor)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__TipoProve__IdCom__2CFD9AD7");
            });

            modelBuilder.Entity<TipoPuntoVenta>(entity =>
            {
                entity.HasKey(e => e.IdTipoPuntoVenta)
                    .HasName("PK__TipoPunt__6D150B1E2A21244E");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<TipoRecibo>(entity =>
            {
                entity.HasKey(e => e.IdTipoRecibo)
                    .HasName("PK__TipoReci__3ED7B50FC14976BE");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoRemolque>(entity =>
            {
                entity.HasKey(e => e.IdTipoRemolque)
                    .HasName("PK__TipoRemo__6A4BB116EAF24134");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdTipoVehiculo)
                    .HasName("PK__TipoVehi__DC20741E2E0A0F5F");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(500);
            });

            modelBuilder.Entity<TipoVigencia>(entity =>
            {
                entity.HasKey(e => e.IdTipoVigencia)
                    .HasName("PK__TipoVige__88A7595108D06BE8");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoWidget>(entity =>
            {
                entity.HasKey(e => e.IdTipoWidget)
                    .HasName("PK__TipoWidg__D8FA44A275CEC789");

                entity.ToTable("TipoWidget", "Trackr");

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<TituloAcademico>(entity =>
            {
                entity.HasKey(e => e.IdTituloAcademico);

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TraspasoMovimientoMaterial>(entity =>
            {
                entity.HasKey(e => e.IdTraspasoMovimientoMaterial);

                entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");

                entity.Property(e => e.Folio).HasMaxLength(20);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.IdAlmacenDestinoNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialIdAlmacenDestinoNavigation)
                    .HasForeignKey(d => d.IdAlmacenDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TraspasoMovimientoMaterial_AlmacenDestino");

                entity.HasOne(d => d.IdAlmacenOrigenNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialIdAlmacenOrigenNavigation)
                    .HasForeignKey(d => d.IdAlmacenOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TraspasoMovimientoMaterial_AlmacenOrigen");

                entity.HasOne(d => d.IdMovimientoMaterialEntradaNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialIdMovimientoMaterialEntradaNavigation)
                    .HasForeignKey(d => d.IdMovimientoMaterialEntrada)
                    .HasConstraintName("FK_TraspasoMovimientoMaterial_MovimientoMaterialEntrada");

                entity.HasOne(d => d.IdMovimientoMaterialSalidaNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialIdMovimientoMaterialSalidaNavigation)
                    .HasForeignKey(d => d.IdMovimientoMaterialSalida)
                    .HasConstraintName("FK_TraspasoMovimientoMaterial_MovimientoMaterialSalida");

                entity.HasOne(d => d.IdUsuarioAlmacenistaNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterial)
                    .HasForeignKey(d => d.IdUsuarioAlmacenista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TraspasoMovimientoMaterial_UsuarioAlmacenista");
            });

            modelBuilder.Entity<TraspasoMovimientoMaterialDetalle>(entity =>
            {
                entity.HasKey(e => e.IdTraspasoMovimientoMaterialDetalle);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.CantidadDestino).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.LoteDestino).HasMaxLength(50);

                entity.Property(e => e.LoteOrigen).HasMaxLength(50);

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialDetalleIdArticuloNavigation)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TraspasoMovimientoMaterialDetalle_Articulo");

                entity.HasOne(d => d.IdArticuloDestinoNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialDetalleIdArticuloDestinoNavigation)
                    .HasForeignKey(d => d.IdArticuloDestino)
                    .HasConstraintName("FK__TraspasoM__IdArt__164F3FA9");

                entity.HasOne(d => d.IdTraspasoMovimientoMaterialNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialDetalle)
                    .HasForeignKey(d => d.IdTraspasoMovimientoMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TraspasoMovimientoMaterialDetalle_TraspasoMovimientoMaterial");

                entity.HasOne(d => d.IdUbicacionDestinoNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialDetalleIdUbicacionDestinoNavigation)
                    .HasForeignKey(d => d.IdUbicacionDestino)
                    .HasConstraintName("FK__TraspasoM__IdUbi__4EC8A2F6");

                entity.HasOne(d => d.IdUbicacionOrigenNavigation)
                    .WithMany(p => p.TraspasoMovimientoMaterialDetalleIdUbicacionOrigenNavigation)
                    .HasForeignKey(d => d.IdUbicacionOrigen)
                    .HasConstraintName("FK__TraspasoM__IdUbi__4DD47EBD");
            });

            modelBuilder.Entity<TratamientoRecordatorio>(entity =>
            {
                entity.HasKey(e => e.IdTratamientoRecordatorio)
                    .HasName("PK__Tratamie__55A5A7F2349A34AD");

                entity.ToTable("TratamientoRecordatorio", "Trackr");

                entity.HasOne(d => d.IdExpedienteTratamientoNavigation)
                    .WithMany(p => p.TratamientoRecordatorio)
                    .HasForeignKey(d => d.IdExpedienteTratamiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tratamien__IdExp__4BF72343");
            });

            modelBuilder.Entity<TratamientoToma>(entity =>
            {
                entity.HasKey(e => e.IdTomaTratamiento)
                    .HasName("PK__Tratamie__93830D97D2170B82");

                entity.ToTable("TratamientoToma", "Trackr");

                entity.Property(e => e.FechaEnvio).HasColumnType("datetime");

                entity.Property(e => e.FechaToma).HasColumnType("datetime");

                entity.HasOne(d => d.IdNotificacionNavigation)
                    .WithMany(p => p.TratamientoToma)
                    .HasForeignKey(d => d.IdNotificacion)
                    .HasConstraintName("FK__Tratamien__IdNot__61E66462");

                entity.HasOne(d => d.IdTratamientoRecordatorioNavigation)
                    .WithMany(p => p.TratamientoToma)
                    .HasForeignKey(d => d.IdTratamientoRecordatorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tratamien__IdTra__4ED38FEE");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.HasKey(e => e.IdUbicacion);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.Ubicacion)
                    .HasForeignKey(d => d.IdAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ubicacion_Almacen");
            });

            modelBuilder.Entity<UbicacionVenta>(entity =>
            {
                entity.HasKey(e => e.IdUbicacionVenta)
                    .HasName("PK_Adquiriente");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdPuntoVentaNavigation)
                    .WithMany(p => p.UbicacionVenta)
                    .HasForeignKey(d => d.IdPuntoVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PuntoVentaUbicacionVenta");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.UnidadMedida)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__UnidadMed__IdCom__31C24FF4");
            });

            modelBuilder.Entity<Urgencia>(entity =>
            {
                entity.HasKey(e => e.IdUrgencia);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.FechaEgreso).HasColumnType("datetime");

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Urgencia)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Urgencia_Hospital");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Urgencia)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Urgencia_Paciente");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithMany(p => p.Urgencia)
                    .HasForeignKey(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Urgencia_Recibo");
            });

            modelBuilder.Entity<UrgenciaTratamiento>(entity =>
            {
                entity.HasKey(e => e.IdUrgenciaTratamiento);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.HasOne(d => d.IdPresentacionTratamientoNavigation)
                    .WithMany(p => p.UrgenciaTratamiento)
                    .HasForeignKey(d => d.IdPresentacionTratamiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UrgenciaTratamiento_PresentacionTratamiento");

                entity.HasOne(d => d.IdReciboDetalleNavigation)
                    .WithMany(p => p.UrgenciaTratamiento)
                    .HasForeignKey(d => d.IdReciboDetalle)
                    .HasConstraintName("FK__UrgenciaT__IdRec__74EE4BDE");

                entity.HasOne(d => d.IdUrgenciaNavigation)
                    .WithMany(p => p.UrgenciaTratamiento)
                    .HasForeignKey(d => d.IdUrgencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UrgenciaTratamiento_Urgencia");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.ApellidoMaterno).HasMaxLength(200);

                entity.Property(e => e.ApellidoPaterno).HasMaxLength(200);

                entity.Property(e => e.Calle).HasMaxLength(100);

                entity.Property(e => e.Cedula).HasMaxLength(50);

                entity.Property(e => e.Ciudad).HasMaxLength(50);

                entity.Property(e => e.CodigoPostal).HasMaxLength(5);

                entity.Property(e => e.Colonia).HasMaxLength(100);

                entity.Property(e => e.Contrasena).HasMaxLength(500);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.CorreoPersonal).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(500);

                entity.Property(e => e.EntreCalles).HasMaxLength(200);

                entity.Property(e => e.ImagenTipoMime).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.NumeroExterior).HasMaxLength(6);

                entity.Property(e => e.NumeroInterior).HasMaxLength(6);

                entity.Property(e => e.NumeroLicencia).HasMaxLength(100);

                entity.Property(e => e.OpenpayIdCustomer).HasMaxLength(500);

                entity.Property(e => e.Rfc).HasMaxLength(50);

                entity.Property(e => e.SueldoDiario).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TelefonoMovil).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK__Usuario__IdArea__59662CFA");

                entity.HasOne(d => d.IdColoniaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdColonia)
                    .HasConstraintName("FK__Usuario__IdColon__08E035F2");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Compania");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_Usuario_Departamento");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_Usuario_Estado");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Hospital");

                entity.HasOne(d => d.IdListaPrecioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdListaPrecio)
                    .HasConstraintName("FK__Usuario__IdLista__141CDE74");

                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdLocalidad)
                    .HasConstraintName("FK__Usuario__IdLocal__09D45A2B");

                entity.HasOne(d => d.IdMetodoPagoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdMetodoPago)
                    .HasConstraintName("FK__Usuario__IdMetod__1DA648AE");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Usuario__IdMunic__07EC11B9");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_Usuario_Perfil");

                entity.HasOne(d => d.IdPuntoVentaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPuntoVenta)
                    .HasConstraintName("FK_Usuario_PuntoVenta");

                entity.HasOne(d => d.IdRegimenFiscalNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRegimenFiscal)
                    .HasConstraintName("FK__Usuario__IdRegim__799DF262");

                entity.HasOne(d => d.IdSatFormaPagoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdSatFormaPago)
                    .HasConstraintName("FK__Usuario__IdSatFo__1CB22475");

                entity.HasOne(d => d.IdTipoClienteNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoCliente)
                    .HasConstraintName("FK__Usuario__IdTipoC__02F25272");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_TipoUsuario");

                entity.HasOne(d => d.IdTituloAcademicoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTituloAcademico)
                    .HasConstraintName("FK_Usuario_TituloAcademico");
            });

            modelBuilder.Entity<UsuarioAlmacen>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioAlmacen)
                    .HasName("PK__UsuarioA__A45E7A33B18BE129");

                entity.HasOne(d => d.IdAlmacenNavigation)
                    .WithMany(p => p.UsuarioAlmacen)
                    .HasForeignKey(d => d.IdAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioAl__IdAlm__155B1B70");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioAlmacen)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioAl__IdUsu__1466F737");
            });

            modelBuilder.Entity<UsuarioLocacion>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioLocacion)
                    .HasName("PK__UsurioLo__5354DB914E27E0CD");

                entity.HasOne(d => d.IdLocacionNavigation)
                    .WithMany(p => p.UsuarioLocacion)
                    .HasForeignKey(d => d.IdLocacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsurioLoc__IdLoc__26509D48");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.UsuarioLocacion)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsurioLoc__IdPer__2744C181");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioLocacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsurioLoc__IdUsu__246854D6");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioRol);

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdConcepto)
                    .HasConstraintName("FK__UsuarioRo__IdCon__0AC87E64");

                entity.HasOne(d => d.IdCuentaContableNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdCuentaContable)
                    .HasConstraintName("FK__UsuarioRo__IdCue__4A58F394");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRol_Rol");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRol_Usuario");
            });

            modelBuilder.Entity<UsuarioWidget>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioWidget)
                    .HasName("PK_IdUuarioWidget");

                entity.ToTable("UsuarioWidget", "Trackr");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioWidget)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdUsuarioWidget_Usuario");

                entity.HasOne(d => d.IdWidgetNavigation)
                    .WithMany(p => p.UsuarioWidget)
                    .HasForeignKey(d => d.IdWidget)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdUsuarioWidget_Widget");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo)
                    .HasName("PK__Vehiculo__70861215523B764F");

                entity.Property(e => e.Marca).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NombreAseguradora).HasMaxLength(200);

                entity.Property(e => e.NumeroPermisoSct)
                    .HasMaxLength(50)
                    .HasColumnName("NumeroPermisoSCT");

                entity.Property(e => e.NumeroPolizaSeguro).HasMaxLength(50);

                entity.Property(e => e.Placas).HasMaxLength(20);

                entity.Property(e => e.PlacasRemolque).HasMaxLength(50);

                entity.HasOne(d => d.IdAuxiliarNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdAuxiliar)
                    .HasConstraintName("FK__Vehiculo__IdAuxi__55959C16");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vehiculo__IdComp__54A177DD");

                entity.HasOne(d => d.IdConfiguracionAutotransporteNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdConfiguracionAutotransporte)
                    .HasConstraintName("FK__Vehiculo__IdConf__577DE488");

                entity.HasOne(d => d.IdTipoPermisoTransporteNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdTipoPermisoTransporte)
                    .HasConstraintName("FK__Vehiculo__IdTipo__5689C04F");

                entity.HasOne(d => d.IdTipoRemolqueNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdTipoRemolque)
                    .HasConstraintName("FK__Vehiculo__IdTipo__587208C1");

                entity.HasOne(d => d.IdTipoVehiculoNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.IdTipoVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vehiculo__IdTipo__53AD53A4");
            });

            modelBuilder.Entity<VehiculoMantenimiento>(entity =>
            {
                entity.HasKey(e => e.IdVehiculoMantenimiento)
                    .HasName("PK__Vehiculo__143994FBE3A36D2B");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaMantenimiento).HasColumnType("date");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.VehiculoMantenimiento)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VehiculoM__IdVeh__6B84DD35");
            });

            modelBuilder.Entity<VersionPoliza>(entity =>
            {
                entity.HasKey(e => e.IdVersionPoliza)
                    .HasName("PK__VersionP__A32742A63B4B0F7E");

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.VersionPoliza)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VersionPo__IdCom__14BBFCF2");
            });

            modelBuilder.Entity<ViaAdministracion>(entity =>
            {
                entity.HasKey(e => e.IdViaAdministracion);

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<VistaBalanzaComprobacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VistaBalanzaComprobacion");

                entity.Property(e => e.Abono).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.Cargo).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.CuentaContable).HasMaxLength(521);
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.HasKey(e => e.IdWidget)
                    .HasName("PK_IdWidget");

                entity.ToTable("Widget", "Trackr");

                entity.Property(e => e.Clave)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
