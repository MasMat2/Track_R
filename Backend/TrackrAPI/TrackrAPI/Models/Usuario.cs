using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Archivo = new HashSet<Archivo>();
            AsistenteDoctorIdAsistenteNavigation = new HashSet<AsistenteDoctor>();
            AsistenteDoctorIdDoctorNavigation = new HashSet<AsistenteDoctor>();
            ChatMensaje = new HashSet<ChatMensaje>();
            ChatMensajeVisto = new HashSet<ChatMensajeVisto>();
            ChatPersona = new HashSet<ChatPersona>();
            ConfirmacionCorreo = new HashSet<ConfirmacionCorreo>();
            DomicilioIdUsuarioNavigation = new HashSet<Domicilio>();
            DomicilioIdUsuarioRepartidorNavigation = new HashSet<Domicilio>();
            Examen = new HashSet<Examen>();
            ExpedienteDoctor = new HashSet<ExpedienteDoctor>();
            ExpedientePadecimiento = new HashSet<ExpedientePadecimiento>();
            ExpedienteRecomendaciones = new HashSet<ExpedienteRecomendaciones>();
            ExpedienteRecomendacionesGenerales = new HashSet<ExpedienteRecomendacionesGenerales>();
            ExpedienteTrackr = new HashSet<ExpedienteTrackr>();
            ExpedienteTratamiento = new HashSet<ExpedienteTratamiento>();
            Guia = new HashSet<Guia>();
            HistorialMovimiento = new HashSet<HistorialMovimiento>();
            Hospital = new HashSet<Hospital>();
            Notificacion = new HashSet<Notificacion>();
            NotificacionDoctor = new HashSet<NotificacionDoctor>();
            NotificacionUsuario = new HashSet<NotificacionUsuario>();
            ProgramacionExamen = new HashSet<ProgramacionExamen>();
            ProyectoActividad = new HashSet<ProyectoActividad>();
            ProyectoActividadHora = new HashSet<ProyectoActividadHora>();
            ProyectoActividadParticipante = new HashSet<ProyectoActividadParticipante>();
            ProyectoIdUsuarioAdministradorNavigation = new HashSet<Proyecto>();
            ProyectoIdUsuarioAltaNavigation = new HashSet<Proyecto>();
            ProyectoIdUsuarioResponsableNavigation = new HashSet<Proyecto>();
            RestablecerContrasena = new HashSet<RestablecerContrasena>();
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
        public virtual Localidad? IdLocalidadNavigation { get; set; }
        public virtual Municipio? IdMunicipioNavigation { get; set; }
        public virtual Perfil? IdPerfilNavigation { get; set; }
        public virtual RegimenFiscal? IdRegimenFiscalNavigation { get; set; }
        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;
        public virtual TituloAcademico? IdTituloAcademicoNavigation { get; set; }
        public virtual ICollection<Archivo> Archivo { get; set; }
        public virtual ICollection<AsistenteDoctor> AsistenteDoctorIdAsistenteNavigation { get; set; }
        public virtual ICollection<AsistenteDoctor> AsistenteDoctorIdDoctorNavigation { get; set; }
        public virtual ICollection<ChatMensaje> ChatMensaje { get; set; }
        public virtual ICollection<ChatMensajeVisto> ChatMensajeVisto { get; set; }
        public virtual ICollection<ChatPersona> ChatPersona { get; set; }
        public virtual ICollection<ConfirmacionCorreo> ConfirmacionCorreo { get; set; }
        public virtual ICollection<Domicilio> DomicilioIdUsuarioNavigation { get; set; }
        public virtual ICollection<Domicilio> DomicilioIdUsuarioRepartidorNavigation { get; set; }
        public virtual ICollection<Examen> Examen { get; set; }
        public virtual ICollection<ExpedienteDoctor> ExpedienteDoctor { get; set; }
        public virtual ICollection<ExpedientePadecimiento> ExpedientePadecimiento { get; set; }
        public virtual ICollection<ExpedienteRecomendaciones> ExpedienteRecomendaciones { get; set; }
        public virtual ICollection<ExpedienteRecomendacionesGenerales> ExpedienteRecomendacionesGenerales { get; set; }
        public virtual ICollection<ExpedienteTrackr> ExpedienteTrackr { get; set; }
        public virtual ICollection<ExpedienteTratamiento> ExpedienteTratamiento { get; set; }
        public virtual ICollection<Guia> Guia { get; set; }
        public virtual ICollection<HistorialMovimiento> HistorialMovimiento { get; set; }
        public virtual ICollection<Hospital> Hospital { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<NotificacionDoctor> NotificacionDoctor { get; set; }
        public virtual ICollection<NotificacionUsuario> NotificacionUsuario { get; set; }
        public virtual ICollection<ProgramacionExamen> ProgramacionExamen { get; set; }
        public virtual ICollection<ProyectoActividad> ProyectoActividad { get; set; }
        public virtual ICollection<ProyectoActividadHora> ProyectoActividadHora { get; set; }
        public virtual ICollection<ProyectoActividadParticipante> ProyectoActividadParticipante { get; set; }
        public virtual ICollection<Proyecto> ProyectoIdUsuarioAdministradorNavigation { get; set; }
        public virtual ICollection<Proyecto> ProyectoIdUsuarioAltaNavigation { get; set; }
        public virtual ICollection<Proyecto> ProyectoIdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<RestablecerContrasena> RestablecerContrasena { get; set; }
        public virtual ICollection<UsuarioLocacion> UsuarioLocacion { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
        public virtual ICollection<UsuarioWidget> UsuarioWidget { get; set; }
    }
}
