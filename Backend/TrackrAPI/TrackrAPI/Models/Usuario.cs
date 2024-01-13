using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Almacen = new HashSet<Almacen>();
            Archivo = new HashSet<Archivo>();
            BitacoraMovimientoUsuario = new HashSet<BitacoraMovimientoUsuario>();
            Caja = new HashSet<Caja>();
            CajaTurno = new HashSet<CajaTurno>();
            Carrito = new HashSet<Carrito>();
            ChatMensaje = new HashSet<ChatMensaje>();
            ChatMensajeVisto = new HashSet<ChatMensajeVisto>();
            ChatPersona = new HashSet<ChatPersona>();
            CitaIdUsuarioDoctorNavigation = new HashSet<Cita>();
            CitaIdUsuarioTomaSomatometriaNavigation = new HashSet<Cita>();
            Comision = new HashSet<Comision>();
            ComplementoPago = new HashSet<ComplementoPago>();
            ConfirmacionCorreo = new HashSet<ConfirmacionCorreo>();
            DomicilioIdUsuarioNavigation = new HashSet<Domicilio>();
            DomicilioIdUsuarioRepartidorNavigation = new HashSet<Domicilio>();
            EntradaPersonalIdUsuarioBajaNavigation = new HashSet<EntradaPersonal>();
            EntradaPersonalIdUsuarioNavigation = new HashSet<EntradaPersonal>();
            Examen = new HashSet<Examen>();
            ExcelPolizaCargaMasiva = new HashSet<ExcelPolizaCargaMasiva>();
            Expediente = new HashSet<Expediente>();
            ExpedienteAdministrativoMercancia = new HashSet<ExpedienteAdministrativoMercancia>();
            ExpedienteAdministrativoViajeIdUsuarioChoferNavigation = new HashSet<ExpedienteAdministrativoViaje>();
            ExpedienteAdministrativoViajeIdUsuarioDestinatarioNavigation = new HashSet<ExpedienteAdministrativoViaje>();
            ExpedienteBitacora = new HashSet<ExpedienteBitacora>();
            ExpedienteDoctor = new HashSet<ExpedienteDoctor>();
            ExpedientePadecimiento = new HashSet<ExpedientePadecimiento>();
            ExpedienteRecomendaciones = new HashSet<ExpedienteRecomendaciones>();
            ExpedienteRecomendacionesGenerales = new HashSet<ExpedienteRecomendacionesGenerales>();
            ExpedienteTrackr = new HashSet<ExpedienteTrackr>();
            ExpedienteTratamiento = new HashSet<ExpedienteTratamiento>();
            Factura = new HashSet<Factura>();
            FlujoDetalleAplicado = new HashSet<FlujoDetalleAplicado>();
            FlujoDetalleAplicadoResponsable = new HashSet<FlujoDetalleAplicadoResponsable>();
            FlujoDetalleResponsable = new HashSet<FlujoDetalleResponsable>();
            Guia = new HashSet<Guia>();
            HistorialMovimiento = new HashSet<HistorialMovimiento>();
            Hospital = new HashSet<Hospital>();
            InventarioFisico = new HashSet<InventarioFisico>();
            ListaPrecioDetalle = new HashSet<ListaPrecioDetalle>();
            MovimientoMaterialIdUsuarioAlmacenistaNavigation = new HashSet<MovimientoMaterial>();
            MovimientoMaterialIdUsuarioProveedorNavigation = new HashSet<MovimientoMaterial>();
            Necesidad = new HashSet<Necesidad>();
            NotaFlujoDetalle = new HashSet<NotaFlujoDetalle>();
            NotaGasto = new HashSet<NotaGasto>();
            NotaVentaDetalle = new HashSet<NotaVentaDetalle>();
            NotaVentaIdUsuarioAltaNavigation = new HashSet<NotaVenta>();
            NotaVentaIdUsuarioClienteNavigation = new HashSet<NotaVenta>();
            Notificacion = new HashSet<Notificacion>();
            NotificacionDoctor = new HashSet<NotificacionDoctor>();
            NotificacionUsuario = new HashSet<NotificacionUsuario>();
            OrdenCompraIdUsuarioCompradorNavigation = new HashSet<OrdenCompra>();
            OrdenCompraIdUsuarioProveedorNavigation = new HashSet<OrdenCompra>();
            Pago = new HashSet<Pago>();
            PedidoBitacora = new HashSet<PedidoBitacora>();
            PedidoPresentacion = new HashSet<PedidoPresentacion>();
            ProgramacionExamen = new HashSet<ProgramacionExamen>();
            ProyectoActividad = new HashSet<ProyectoActividad>();
            ProyectoActividadHora = new HashSet<ProyectoActividadHora>();
            ProyectoActividadParticipante = new HashSet<ProyectoActividadParticipante>();
            ProyectoIdUsuarioAdministradorNavigation = new HashSet<Proyecto>();
            ProyectoIdUsuarioAltaNavigation = new HashSet<Proyecto>();
            ProyectoIdUsuarioResponsableNavigation = new HashSet<Proyecto>();
            ReciboEnglobado = new HashSet<ReciboEnglobado>();
            ReciboIdUsuarioAltaNavigation = new HashSet<Recibo>();
            ReciboIdUsuarioCajaNavigation = new HashSet<Recibo>();
            ReciboIdUsuarioClienteNavigation = new HashSet<Recibo>();
            Recordatorio = new HashSet<Recordatorio>();
            RestablecerContrasena = new HashSet<RestablecerContrasena>();
            Tarjeta = new HashSet<Tarjeta>();
            TraspasoMovimientoMaterial = new HashSet<TraspasoMovimientoMaterial>();
            UsuarioAlmacen = new HashSet<UsuarioAlmacen>();
            UsuarioLocacion = new HashSet<UsuarioLocacion>();
            UsuarioRol = new HashSet<UsuarioRol>();
            UsuarioWidget = new HashSet<UsuarioWidget>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public string? Ciudad { get; set; }
        public string? TelefonoMovil { get; set; }
        public bool Habilitado { get; set; }
        public int? IdEstado { get; set; }
        public int IdTipoUsuario { get; set; }
        public int? IdPerfil { get; set; }
        public int IdCompania { get; set; }
        public string? Username { get; set; }
        public string? ImagenTipoMime { get; set; }
        public int IdHospital { get; set; }
        public int? IdTituloAcademico { get; set; }
        public int? IdDepartamento { get; set; }
        public string? Calle { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }
        public string? Colonia { get; set; }
        public string? CorreoPersonal { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Cedula { get; set; }
        public int? IdPuntoVenta { get; set; }
        public string? Direccion { get; set; }
        public string? OpenpayIdCustomer { get; set; }
        public string? Rfc { get; set; }
        public decimal? SueldoDiario { get; set; }
        public int? IdArea { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdColonia { get; set; }
        public int? IdLocalidad { get; set; }
        public string? NumeroLicencia { get; set; }
        public int? DiasPago { get; set; }
        public int? IdTipoCliente { get; set; }
        public int? IdListaPrecio { get; set; }
        public int? IdSatFormaPago { get; set; }
        public int? IdMetodoPago { get; set; }
        public string? EntreCalles { get; set; }
        public bool CorreoConfirmado { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual Colonia? IdColoniaNavigation { get; set; }
        public virtual Compania IdCompaniaNavigation { get; set; } = null!;
        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual Hospital IdHospitalNavigation { get; set; } = null!;
        public virtual ListaPrecio? IdListaPrecioNavigation { get; set; }
        public virtual Localidad? IdLocalidadNavigation { get; set; }
        public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual Perfil? IdPerfilNavigation { get; set; }
        public virtual PuntoVenta? IdPuntoVentaNavigation { get; set; }
        public virtual RegimenFiscal? IdRegimenFiscalNavigation { get; set; }
        public virtual SatFormaPago? IdSatFormaPagoNavigation { get; set; }
        public virtual TipoCliente? IdTipoClienteNavigation { get; set; }
        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;
        public virtual TituloAcademico? IdTituloAcademicoNavigation { get; set; }
        public virtual ICollection<Almacen> Almacen { get; set; }
        public virtual ICollection<Archivo> Archivo { get; set; }
        public virtual ICollection<BitacoraMovimientoUsuario> BitacoraMovimientoUsuario { get; set; }
        public virtual ICollection<Caja> Caja { get; set; }
        public virtual ICollection<CajaTurno> CajaTurno { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<ChatMensaje> ChatMensaje { get; set; }
        public virtual ICollection<ChatMensajeVisto> ChatMensajeVisto { get; set; }
        public virtual ICollection<ChatPersona> ChatPersona { get; set; }
        public virtual ICollection<Cita> CitaIdUsuarioDoctorNavigation { get; set; }
        public virtual ICollection<Cita> CitaIdUsuarioTomaSomatometriaNavigation { get; set; }
        public virtual ICollection<Comision> Comision { get; set; }
        public virtual ICollection<ComplementoPago> ComplementoPago { get; set; }
        public virtual ICollection<ConfirmacionCorreo> ConfirmacionCorreo { get; set; }
        public virtual ICollection<Domicilio> DomicilioIdUsuarioNavigation { get; set; }
        public virtual ICollection<Domicilio> DomicilioIdUsuarioRepartidorNavigation { get; set; }
        public virtual ICollection<EntradaPersonal> EntradaPersonalIdUsuarioBajaNavigation { get; set; }
        public virtual ICollection<EntradaPersonal> EntradaPersonalIdUsuarioNavigation { get; set; }
        public virtual ICollection<Examen> Examen { get; set; }
        public virtual ICollection<ExcelPolizaCargaMasiva> ExcelPolizaCargaMasiva { get; set; }
        public virtual ICollection<Expediente> Expediente { get; set; }
        public virtual ICollection<ExpedienteAdministrativoMercancia> ExpedienteAdministrativoMercancia { get; set; }
        public virtual ICollection<ExpedienteAdministrativoViaje> ExpedienteAdministrativoViajeIdUsuarioChoferNavigation { get; set; }
        public virtual ICollection<ExpedienteAdministrativoViaje> ExpedienteAdministrativoViajeIdUsuarioDestinatarioNavigation { get; set; }
        public virtual ICollection<ExpedienteBitacora> ExpedienteBitacora { get; set; }
        public virtual ICollection<ExpedienteDoctor> ExpedienteDoctor { get; set; }
        public virtual ICollection<ExpedientePadecimiento> ExpedientePadecimiento { get; set; }
        public virtual ICollection<ExpedienteRecomendaciones> ExpedienteRecomendaciones { get; set; }
        public virtual ICollection<ExpedienteRecomendacionesGenerales> ExpedienteRecomendacionesGenerales { get; set; }
        public virtual ICollection<ExpedienteTrackr> ExpedienteTrackr { get; set; }
        public virtual ICollection<ExpedienteTratamiento> ExpedienteTratamiento { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<FlujoDetalleAplicado> FlujoDetalleAplicado { get; set; }
        public virtual ICollection<FlujoDetalleAplicadoResponsable> FlujoDetalleAplicadoResponsable { get; set; }
        public virtual ICollection<FlujoDetalleResponsable> FlujoDetalleResponsable { get; set; }
        public virtual ICollection<Guia> Guia { get; set; }
        public virtual ICollection<HistorialMovimiento> HistorialMovimiento { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<InventarioFisico> InventarioFisico { get; set; }
        public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalle { get; set; }
        public virtual ICollection<MovimientoMaterial> MovimientoMaterialIdUsuarioAlmacenistaNavigation { get; set; }
        public virtual ICollection<MovimientoMaterial> MovimientoMaterialIdUsuarioProveedorNavigation { get; set; }
        public virtual ICollection<Necesidad> Necesidad { get; set; }
        public virtual ICollection<NotaFlujoDetalle> NotaFlujoDetalle { get; set; }
        public virtual ICollection<NotaGasto> NotaGasto { get; set; }
        public virtual ICollection<NotaVentaDetalle> NotaVentaDetalle { get; set; }
        public virtual ICollection<NotaVenta> NotaVentaIdUsuarioAltaNavigation { get; set; }
        public virtual ICollection<NotaVenta> NotaVentaIdUsuarioClienteNavigation { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<NotificacionDoctor> NotificacionDoctor { get; set; }
        public virtual ICollection<NotificacionUsuario> NotificacionUsuario { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompraIdUsuarioCompradorNavigation { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompraIdUsuarioProveedorNavigation { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
        public virtual ICollection<PedidoBitacora> PedidoBitacora { get; set; }
        public virtual ICollection<PedidoPresentacion> PedidoPresentacion { get; set; }
        public virtual ICollection<ProgramacionExamen> ProgramacionExamen { get; set; }
        public virtual ICollection<ProyectoActividad> ProyectoActividad { get; set; }
        public virtual ICollection<ProyectoActividadHora> ProyectoActividadHora { get; set; }
        public virtual ICollection<ProyectoActividadParticipante> ProyectoActividadParticipante { get; set; }
        public virtual ICollection<Proyecto> ProyectoIdUsuarioAdministradorNavigation { get; set; }
        public virtual ICollection<Proyecto> ProyectoIdUsuarioAltaNavigation { get; set; }
        public virtual ICollection<Proyecto> ProyectoIdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<ReciboEnglobado> ReciboEnglobado { get; set; }
        public virtual ICollection<Recibo> ReciboIdUsuarioAltaNavigation { get; set; }
        public virtual ICollection<Recibo> ReciboIdUsuarioCajaNavigation { get; set; }
        public virtual ICollection<Recibo> ReciboIdUsuarioClienteNavigation { get; set; }
        public virtual ICollection<Recordatorio> Recordatorio { get; set; }
        public virtual ICollection<RestablecerContrasena> RestablecerContrasena { get; set; }
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
        public virtual ICollection<TraspasoMovimientoMaterial> TraspasoMovimientoMaterial { get; set; }
        public virtual ICollection<UsuarioAlmacen> UsuarioAlmacen { get; set; }
        public virtual ICollection<UsuarioLocacion> UsuarioLocacion { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
        public virtual ICollection<UsuarioWidget> UsuarioWidget { get; set; }
    }
}
