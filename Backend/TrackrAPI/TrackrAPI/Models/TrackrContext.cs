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
        public virtual DbSet<Archivo> Archivo { get; set; } = null!;
        public virtual DbSet<Area> Area { get; set; } = null!;
        public virtual DbSet<Artefacto> Artefacto { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignatura { get; set; } = null!;
        public virtual DbSet<AsistenteDoctor> AsistenteDoctor { get; set; } = null!;
        public virtual DbSet<AyudaSeccion> AyudaSeccion { get; set; } = null!;
        public virtual DbSet<Chat> Chat { get; set; } = null!;
        public virtual DbSet<ChatMensaje> ChatMensaje { get; set; } = null!;
        public virtual DbSet<ChatMensajeVisto> ChatMensajeVisto { get; set; } = null!;
        public virtual DbSet<ChatPersona> ChatPersona { get; set; } = null!;
        public virtual DbSet<ClasificacionPregunta> ClasificacionPregunta { get; set; } = null!;
        public virtual DbSet<CodigoPostal> CodigoPostal { get; set; } = null!;
        public virtual DbSet<Colonia> Colonia { get; set; } = null!;
        public virtual DbSet<Compania> Compania { get; set; } = null!;
        public virtual DbSet<CompaniaContacto> CompaniaContacto { get; set; } = null!;
        public virtual DbSet<CompaniaLogotipo> CompaniaLogotipo { get; set; } = null!;
        public virtual DbSet<ConfirmacionCorreo> ConfirmacionCorreo { get; set; } = null!;
        public virtual DbSet<ContenidoExamen> ContenidoExamen { get; set; } = null!;
        public virtual DbSet<Departamento> Departamento { get; set; } = null!;
        public virtual DbSet<DetalleExpedienteRecomendacionesGenerales> DetalleExpedienteRecomendacionesGenerales { get; set; } = null!;
        public virtual DbSet<Direccion> Direccion { get; set; } = null!;
        public virtual DbSet<DistributedLocks> DistributedLocks { get; set; } = null!;
        public virtual DbSet<Domicilio> Domicilio { get; set; } = null!;
        public virtual DbSet<Dominio> Dominio { get; set; } = null!;
        public virtual DbSet<DominioDetalle> DominioDetalle { get; set; } = null!;
        public virtual DbSet<DominioHospital> DominioHospital { get; set; } = null!;
        public virtual DbSet<Entidad> Entidad { get; set; } = null!;
        public virtual DbSet<EntidadEstructura> EntidadEstructura { get; set; } = null!;
        public virtual DbSet<EntidadEstructuraTablaValor> EntidadEstructuraTablaValor { get; set; } = null!;
        public virtual DbSet<EntidadEstructuraValor> EntidadEstructuraValor { get; set; } = null!;
        public virtual DbSet<Especialidad> Especialidad { get; set; } = null!;
        public virtual DbSet<EspecialidadUsuario> EspecialidadUsuario { get; set; } = null!;
        public virtual DbSet<Estado> Estado { get; set; } = null!;
        public virtual DbSet<EstadoCivil> EstadoCivil { get; set; } = null!;
        public virtual DbSet<EstatusExamen> EstatusExamen { get; set; } = null!;
        public virtual DbSet<Examen> Examen { get; set; } = null!;
        public virtual DbSet<ExamenReactivo> ExamenReactivo { get; set; } = null!;
        public virtual DbSet<ExcelArchivo> ExcelArchivo { get; set; } = null!;
        public virtual DbSet<ExcelError> ExcelError { get; set; } = null!;
        public virtual DbSet<ExpedienteDoctor> ExpedienteDoctor { get; set; } = null!;
        public virtual DbSet<ExpedienteEstudio> ExpedienteEstudio { get; set; } = null!;
        public virtual DbSet<ExpedientePadecimiento> ExpedientePadecimiento { get; set; } = null!;
        public virtual DbSet<ExpedienteRecomendaciones> ExpedienteRecomendaciones { get; set; } = null!;
        public virtual DbSet<ExpedienteRecomendacionesGenerales> ExpedienteRecomendacionesGenerales { get; set; } = null!;
        public virtual DbSet<ExpedienteTrackr> ExpedienteTrackr { get; set; } = null!;
        public virtual DbSet<ExpedienteTratamiento> ExpedienteTratamiento { get; set; } = null!;
        public virtual DbSet<Genero> Genero { get; set; } = null!;
        public virtual DbSet<GiroComercial> GiroComercial { get; set; } = null!;
        public virtual DbSet<Guia> Guia { get; set; } = null!;
        public virtual DbSet<GuiaActividad> GuiaActividad { get; set; } = null!;
        public virtual DbSet<GuiaActividadEvidencia> GuiaActividadEvidencia { get; set; } = null!;
        public virtual DbSet<GuiaElementoTecnica> GuiaElementoTecnica { get; set; } = null!;
        public virtual DbSet<HistorialMovimiento> HistorialMovimiento { get; set; } = null!;
        public virtual DbSet<Horario> Horario { get; set; } = null!;
        public virtual DbSet<Hospital> Hospital { get; set; } = null!;
        public virtual DbSet<HospitalLogotipo> HospitalLogotipo { get; set; } = null!;
        public virtual DbSet<Icono> Icono { get; set; } = null!;
        public virtual DbSet<Jerarquia> Jerarquia { get; set; } = null!;
        public virtual DbSet<JerarquiaAcceso> JerarquiaAcceso { get; set; } = null!;
        public virtual DbSet<JerarquiaAccesoEstructura> JerarquiaAccesoEstructura { get; set; } = null!;
        public virtual DbSet<JerarquiaAccesoTipoCompania> JerarquiaAccesoTipoCompania { get; set; } = null!;
        public virtual DbSet<Lada> Lada { get; set; } = null!;
        public virtual DbSet<Localidad> Localidad { get; set; } = null!;
        public virtual DbSet<Municipio> Municipio { get; set; } = null!;
        public virtual DbSet<NivelExamen> NivelExamen { get; set; } = null!;
        public virtual DbSet<Notificacion> Notificacion { get; set; } = null!;
        public virtual DbSet<NotificacionDoctor> NotificacionDoctor { get; set; } = null!;
        public virtual DbSet<NotificacionUsuario> NotificacionUsuario { get; set; } = null!;
        public virtual DbSet<Pais> Pais { get; set; } = null!;
        public virtual DbSet<Perfil> Perfil { get; set; } = null!;
        public virtual DbSet<ProgramacionExamen> ProgramacionExamen { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyecto { get; set; } = null!;
        public virtual DbSet<ProyectoActividad> ProyectoActividad { get; set; } = null!;
        public virtual DbSet<ProyectoActividadEvidencia> ProyectoActividadEvidencia { get; set; } = null!;
        public virtual DbSet<ProyectoActividadEvidenciaArchivo> ProyectoActividadEvidenciaArchivo { get; set; } = null!;
        public virtual DbSet<ProyectoActividadHora> ProyectoActividadHora { get; set; } = null!;
        public virtual DbSet<ProyectoActividadParticipante> ProyectoActividadParticipante { get; set; } = null!;
        public virtual DbSet<ProyectoElementoTecnica> ProyectoElementoTecnica { get; set; } = null!;
        public virtual DbSet<ProyectoEstatus> ProyectoEstatus { get; set; } = null!;
        public virtual DbSet<Reactivo> Reactivo { get; set; } = null!;
        public virtual DbSet<RegimenFiscal> RegimenFiscal { get; set; } = null!;
        public virtual DbSet<Respuesta> Respuesta { get; set; } = null!;
        public virtual DbSet<RespuestasClasificacionPregunta> RespuestasClasificacionPregunta { get; set; } = null!;
        public virtual DbSet<RestablecerContrasena> RestablecerContrasena { get; set; } = null!;
        public virtual DbSet<Rol> Rol { get; set; } = null!;
        public virtual DbSet<RolAcceso> RolAcceso { get; set; } = null!;
        public virtual DbSet<Seccion> Seccion { get; set; } = null!;
        public virtual DbSet<SeccionCampo> SeccionCampo { get; set; } = null!;
        public virtual DbSet<SftpCache> SftpCache { get; set; } = null!;
        public virtual DbSet<TipoAcceso> TipoAcceso { get; set; } = null!;
        public virtual DbSet<TipoChatPersona> TipoChatPersona { get; set; } = null!;
        public virtual DbSet<TipoCompania> TipoCompania { get; set; } = null!;
        public virtual DbSet<TipoExamen> TipoExamen { get; set; } = null!;
        public virtual DbSet<TipoExpedienteRecomendacionGeneral> TipoExpedienteRecomendacionGeneral { get; set; } = null!;
        public virtual DbSet<TipoGuia> TipoGuia { get; set; } = null!;
        public virtual DbSet<TipoNotificacion> TipoNotificacion { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; } = null!;
        public virtual DbSet<TipoWidget> TipoWidget { get; set; } = null!;
        public virtual DbSet<TituloAcademico> TituloAcademico { get; set; } = null!;
        public virtual DbSet<TratamientoRecordatorio> TratamientoRecordatorio { get; set; } = null!;
        public virtual DbSet<TratamientoToma> TratamientoToma { get; set; } = null!;
        public virtual DbSet<Turno> Turno { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; } = null!;
        public virtual DbSet<UnidadesMedida> UnidadesMedida { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<UsuarioLocacion> UsuarioLocacion { get; set; } = null!;
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; } = null!;
        public virtual DbSet<UsuarioWidget> UsuarioWidget { get; set; } = null!;
        public virtual DbSet<VistaBalanzaComprobacion> VistaBalanzaComprobacion { get; set; } = null!;
        public virtual DbSet<Widget> Widget { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<Acceso>(entity =>
            {
                entity.HasKey(e => e.IdAcceso);

                entity.ToTable("Acceso", "Configuracion");

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

                entity.ToTable("AccesoAyuda", "Configuracion");

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

                entity.ToTable("AccesoPerfil", "Configuracion");

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

            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => e.IdArchivo)
                    .HasName("PK__Archivo__26B9211157A0384D");

                entity.ToTable("Archivo", "Trackr");

                entity.Property(e => e.Archivo1)
                    .HasColumnType("image")
                    .HasColumnName("Archivo");

                entity.Property(e => e.ArchivoNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoTipoMime)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivoUrl).HasMaxLength(255);

                entity.Property(e => e.FechaRealizacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Archivo)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Archivo_Usuario");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea);

                entity.ToTable("Area", "Configuracion");

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

            modelBuilder.Entity<AsistenteDoctor>(entity =>
            {
                entity.HasKey(e => e.IdAsistenteDoctor)
                    .HasName("PK__Asistent__021DF896B836A7E7");

                entity.ToTable("AsistenteDoctor", "Trackr");

                entity.HasOne(d => d.IdAsistenteNavigation)
                    .WithMany(p => p.AsistenteDoctorIdAsistenteNavigation)
                    .HasForeignKey(d => d.IdAsistente)
                    .HasConstraintName("FK__Asistente__IdAsi__21CBDF4D");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.AsistenteDoctorIdDoctorNavigation)
                    .HasForeignKey(d => d.IdDoctor)
                    .HasConstraintName("FK__Asistente__IdDoc__20D7BB14");
            });

            modelBuilder.Entity<AyudaSeccion>(entity =>
            {
                entity.HasKey(e => e.IdAyudaSeccion)
                    .HasName("PK__AyudaSec__56D54A221972AA0B");

                entity.ToTable("AyudaSeccion", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.IdChat)
                    .HasName("PK__Chat__3817F38CDAF94781");

                entity.ToTable("Chat", "Trackr");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChatMensaje>(entity =>
            {
                entity.HasKey(e => e.IdChatMensaje)
                    .HasName("PK__ChatMens__CE49C95E3E6B4469");

                entity.ToTable("ChatMensaje", "Trackr");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Mensaje).HasMaxLength(700);

                entity.HasOne(d => d.IdArchivoNavigation)
                    .WithMany(p => p.ChatMensaje)
                    .HasForeignKey(d => d.IdArchivo)
                    .HasConstraintName("FK_Mensaje_Aechivo");

                entity.HasOne(d => d.IdChatNavigation)
                    .WithMany(p => p.ChatMensaje)
                    .HasForeignKey(d => d.IdChat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatMensa__IdCha__0AE879F5");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.ChatMensaje)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatMensa__IdPer__0BDC9E2E");
            });

            modelBuilder.Entity<ChatMensajeVisto>(entity =>
            {
                entity.HasKey(e => e.IdMensajeVisto)
                    .HasName("PK__ChatMens__7829C499A0058423");

                entity.ToTable("ChatMensajeVisto", "Trackr");

                entity.HasOne(d => d.IdMensajeNavigation)
                    .WithMany(p => p.ChatMensajeVisto)
                    .HasForeignKey(d => d.IdMensaje)
                    .HasConstraintName("FK__ChatMensa__IdMen__165A2CA1");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.ChatMensajeVisto)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK__ChatMensa__IdPer__15660868");
            });

            modelBuilder.Entity<ChatPersona>(entity =>
            {
                entity.HasKey(e => e.IdChatPersona)
                    .HasName("PK__ChatPers__54BD8486EA0D1448");

                entity.ToTable("ChatPersona", "Trackr");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.ChatPersona)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatPerso__IdPer__0717E911");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.ChatPersona)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatPerso__IdTip__080C0D4A");
            });

            modelBuilder.Entity<ClasificacionPregunta>(entity =>
            {
                entity.HasKey(e => e.IdClasificacionPregunta);

                entity.ToTable("ClasificacionPregunta", "Proyectos");

                entity.Property(e => e.IdClasificacionPregunta).HasColumnName("idClasificacionPregunta");

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CodigoPostal>(entity =>
            {
                entity.HasKey(e => e.IdCodigoPostal);

                entity.ToTable("CodigoPostal", "Configuracion");

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

                entity.ToTable("Colonia", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Colonia)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Colonia__IdMunic__7A7D0802");
            });

            modelBuilder.Entity<Compania>(entity =>
            {
                entity.HasKey(e => e.IdCompania);

                entity.ToTable("Compania", "Configuracion");

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

                entity.ToTable("CompaniaContacto", "Configuracion");

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

                entity.ToTable("CompaniaLogotipo", "Configuracion");

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

            modelBuilder.Entity<ConfirmacionCorreo>(entity =>
            {
                entity.HasKey(e => e.IdConfirmacionCorreo)
                    .HasName("PK__Confirma__C8248F4C009E0B79");

                entity.ToTable("ConfirmacionCorreo", "Configuracion");

                entity.Property(e => e.IdConfirmacionCorreo).HasColumnName("idConfirmacionCorreo");

                entity.Property(e => e.Clave).HasMaxLength(500);

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ConfirmacionCorreo)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Confirmac__idUsu__3D73F9C2");
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

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK_Deparamento");

                entity.ToTable("Departamento", "Configuracion");

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

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__B49878C9BA481958");

                entity.ToTable("Direccion", "Configuracion");

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

                entity.ToTable("Domicilio", "Configuracion");

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

                entity.ToTable("Dominio", "Configuracion");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.FechaMaxima).HasColumnType("date");

                entity.Property(e => e.FechaMinima).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.TipoCampo).HasMaxLength(50);

                entity.Property(e => e.TipoDato).HasMaxLength(50);

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.ValorMaximo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ValorMinimo).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Dominio)
                    .HasForeignKey(d => d.IdHospital)
                    .HasConstraintName("FK_Dominio_Hospital");
            });

            modelBuilder.Entity<DominioDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDominioDetalle);

                entity.ToTable("DominioDetalle", "Configuracion");

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

            modelBuilder.Entity<DominioHospital>(entity =>
            {
                entity.HasKey(e => e.IdDominioHospital)
                    .HasName("PK__DominioH__001F4EB6474874F1");

                entity.ToTable("DominioHospital", "Configuracion");

                entity.Property(e => e.FechaMaxima).HasColumnType("date");

                entity.Property(e => e.FechaMinima).HasColumnType("date");

                entity.Property(e => e.UnidadMedida).HasMaxLength(50);

                entity.Property(e => e.ValorMaximo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ValorMinimo).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdDominioNavigation)
                    .WithMany(p => p.DominioHospital)
                    .HasForeignKey(d => d.IdDominio)
                    .HasConstraintName("FK__DominioHo__Unida__259C7031");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.DominioHospital)
                    .HasForeignKey(d => d.IdHospital)
                    .HasConstraintName("FK__DominioHo__IdHos__2690946A");
            });

            modelBuilder.Entity<Entidad>(entity =>
            {
                entity.HasKey(e => e.IdEntidad)
                    .HasName("PK__Entidad__7D6628682E7BC423");

                entity.ToTable("Entidad", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<EntidadEstructura>(entity =>
            {
                entity.HasKey(e => e.IdEntidadEstructura)
                    .HasName("PK__EntidadE__319BD1B0BD0852C9");

                entity.ToTable("EntidadEstructura", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EsAntecedente).HasColumnName("esAntecedente");

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

                entity.ToTable("EntidadEstructuraTablaValor", "Configuracion");

                entity.Property(e => e.FechaMuestra).HasColumnType("datetime");

                entity.HasOne(d => d.IdEntidadEstructuraNavigation)
                    .WithMany(p => p.EntidadEstructuraTablaValor)
                    .HasForeignKey(d => d.IdEntidadEstructura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EntidadEs__IdEnt__09353BAD");

                entity.HasOne(d => d.IdSeccionNavigation)
                    .WithMany(p => p.EntidadEstructuraTablaValor)
                    .HasForeignKey(d => d.IdSeccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EntidadEs__IdSec__432CD318");
            });

            modelBuilder.Entity<EntidadEstructuraValor>(entity =>
            {
                entity.HasKey(e => e.IdEntidadEstructuraValor)
                    .HasName("PK__EntidadE__C42BD41247311D8D");

                entity.ToTable("EntidadEstructuraValor", "Configuracion");

                entity.Property(e => e.ClaveCampo).HasMaxLength(50);

                entity.HasOne(d => d.IdEntidadEstructuraNavigation)
                    .WithMany(p => p.EntidadEstructuraValor)
                    .HasForeignKey(d => d.IdEntidadEstructura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EntidadEs__IdEnt__7AE71C56");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad)
                    .HasName("PK__Especial__693FA0AFC5C935E0");

                entity.ToTable("Especialidad", "Trackr");

                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<EspecialidadUsuario>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidadUsuario)
                    .HasName("PK__Especial__8E3C25650A92899F");

                entity.ToTable("EspecialidadUsuario", "Trackr");

                entity.Property(e => e.IdEspecialidadUsuario).HasColumnName("idEspecialidadUsuario");

                entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.EspecialidadUsuario)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .HasConstraintName("FK__Especiali__idEsp__77A09B57");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.EspecialidadUsuario)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Especiali__idUsu__7894BF90");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("Estado", "Configuracion");

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

                entity.ToTable("EstadoCivil", "Configuracion");

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

                entity.Property(e => e.ArchivoUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRealizacion).HasColumnType("datetime");

                entity.Property(e => e.IdArchivo).HasColumnName("idArchivo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdArchivoNavigation)
                    .WithMany(p => p.ExpedienteEstudio)
                    .HasForeignKey(d => d.IdArchivo)
                    .HasConstraintName("FK_ExpedienteEstudio_Archivo");

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.ExpedienteEstudio)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpedienteEstudio_ExpedienteTrackr");
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

                entity.Property(e => e.ArchivoUrl).HasMaxLength(250);

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Farmaco).HasMaxLength(200);

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.IdArchivo).HasColumnName("idArchivo");

                entity.Property(e => e.Indicaciones).HasMaxLength(500);

                entity.Property(e => e.Unidad).HasMaxLength(100);

                entity.HasOne(d => d.IdArchivoNavigation)
                    .WithMany(p => p.ExpedienteTratamiento)
                    .HasForeignKey(d => d.IdArchivo)
                    .HasConstraintName("FK_ExpedienteTratamiento_Archivo");

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

                entity.ToTable("GiroComercial", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(100);
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

                entity.ToTable("Hospital", "Configuracion");

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

                entity.ToTable("HospitalLogotipo", "Configuracion");

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

                entity.ToTable("Icono", "Configuracion");

                entity.Property(e => e.Clase).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Jerarquia>(entity =>
            {
                entity.HasKey(e => e.IdJerarquia)
                    .HasName("PK__Jerarqui__2C6EC225992C435C");

                entity.ToTable("Jerarquia", "Configuracion");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Jerarquia)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jerarquia__IdCom__613C58EC");
            });

            modelBuilder.Entity<JerarquiaAcceso>(entity =>
            {
                entity.HasKey(e => e.IdJerarquiaAcceso)
                    .HasName("PK__Jerarqui__A237C536E662A723");

                entity.ToTable("JerarquiaAcceso", "Configuracion");

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

                entity.ToTable("JerarquiaAccesoEstructura", "Configuracion");

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

                entity.ToTable("JerarquiaAccesoTipoCompania", "Configuracion");

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

            modelBuilder.Entity<Lada>(entity =>
            {
                entity.HasKey(e => e.IdLada)
                    .HasName("PK__Lada__372897B722B451DA");

                entity.ToTable("Lada", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad)
                    .HasName("PK__Localida__27432612EEE3BA42");

                entity.ToTable("Localidad", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(500);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Localidad)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Localidad__IdEst__7E62A77F");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);

                entity.ToTable("Municipio", "Configuracion");

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

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion)
                    .HasName("PK__Notifica__F6CA0A850829CF10");

                entity.ToTable("Notificacion", "Configuracion");

                entity.Property(e => e.ComplementoMensaje).HasMaxLength(300);

                entity.Property(e => e.FechaAlta).HasColumnType("datetime");

                entity.Property(e => e.Mensaje).HasMaxLength(500);

                entity.Property(e => e.Titulo).HasMaxLength(1000);

                entity.HasOne(d => d.IdChatNavigation)
                    .WithMany(p => p.Notificacion)
                    .HasForeignKey(d => d.IdChat)
                    .HasConstraintName("FK_Notificacion_Chat");

                entity.HasOne(d => d.IdPadecimientoNavigation)
                    .WithMany(p => p.Notificacion)
                    .HasForeignKey(d => d.IdPadecimiento)
                    .HasConstraintName("FK_Notificacion_EntidadEstructura");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Notificacion)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_NotificacionAUsuario");

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

                entity.ToTable("NotificacionDoctor", "Configuracion");

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

                entity.ToTable("NotificacionUsuario", "Configuracion");

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

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.ToTable("Pais", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil);

                entity.ToTable("Perfil", "Configuracion");

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

            modelBuilder.Entity<RegimenFiscal>(entity =>
            {
                entity.HasKey(e => e.IdRegimenFiscal);

                entity.ToTable("RegimenFiscal", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("PK__Respuest__D3480198C0EB8F2F");

                entity.ToTable("Respuesta", "Proyectos");

                entity.Property(e => e.Clave).HasMaxLength(30);

                entity.Property(e => e.Respuesta1)
                    .HasMaxLength(2000)
                    .HasColumnName("Respuesta");

                entity.HasOne(d => d.IdReactivoNavigation)
                    .WithMany(p => p.RespuestaNavigation)
                    .HasForeignKey(d => d.IdReactivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Respuesta__IdRea__5733CBC5");
            });

            modelBuilder.Entity<RespuestasClasificacionPregunta>(entity =>
            {
                entity.HasKey(e => e.IdRespuestasClasificacionPregunta)
                    .HasName("PK__Respuest__B00235C9F755D13F");

                entity.Property(e => e.Identificador)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClasificacionPreguntaNavigation)
                    .WithMany(p => p.RespuestasClasificacionPregunta)
                    .HasForeignKey(d => d.IdClasificacionPregunta)
                    .HasConstraintName("FK__Respuesta__IdCla__5086CE36");
            });

            modelBuilder.Entity<RestablecerContrasena>(entity =>
            {
                entity.HasKey(e => e.IdRestablecerContrasena);

                entity.ToTable("RestablecerContrasena", "Configuracion");

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

                entity.ToTable("Rol", "Configuracion");

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

                entity.ToTable("RolAcceso", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.HasKey(e => e.IdSeccion)
                    .HasName("PK__Seccion__CD2B049F59C34F5E");

                entity.ToTable("Seccion", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<SeccionCampo>(entity =>
            {
                entity.HasKey(e => e.IdSeccionCampo)
                    .HasName("PK__SeccionC__A4CF246CF505A904");

                entity.ToTable("SeccionCampo", "Configuracion");

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

            modelBuilder.Entity<SftpCache>(entity =>
            {
                entity.ToTable("SftpCache", "Trackr");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastWriteTime).HasColumnType("datetime");
            });


            modelBuilder.Entity<TipoAcceso>(entity =>
            {
                entity.HasKey(e => e.IdTipoAcceso);

                entity.ToTable("TipoAcceso", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoChatPersona>(entity =>
            {
                entity.HasKey(e => e.IdTipoChatPersona)
                    .HasName("PK__TipoChat__08BEEFFADFBF89A1");

                entity.ToTable("TipoChatPersona", "Trackr");

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoCompania>(entity =>
            {
                entity.HasKey(e => e.IdTipoCompania)
                    .HasName("PK__TipoComp__3367F94A22F545A7");

                entity.ToTable("TipoCompania", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(500);
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

            modelBuilder.Entity<TipoExpedienteRecomendacionGeneral>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__TipoExpe__9E3A29A5C22BD333");

                entity.ToTable("TipoExpedienteRecomendacionGeneral", "Trackr");

                entity.Property(e => e.Tipo).HasMaxLength(100);
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

            modelBuilder.Entity<TipoNotificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoNotificacion)
                    .HasName("PK__TipoNoti__0ECE0435F8C7AA48");

                entity.ToTable("TipoNotificacion", "Configuracion");

                entity.Property(e => e.IdTipoNotificacion).ValueGeneratedNever();

                entity.Property(e => e.Clave).HasMaxLength(3);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.ToTable("TipoUsuario", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(3);

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

                entity.ToTable("TituloAcademico", "Configuracion");

                entity.Property(e => e.Clave)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);
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

                entity.ToTable("Turno", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida);

                entity.ToTable("UnidadMedida", "Configuracion");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.UnidadMedida)
                    .HasForeignKey(d => d.IdCompania)
                    .HasConstraintName("FK__UnidadMed__IdCom__31C24FF4");
            });

            modelBuilder.Entity<UnidadesMedida>(entity =>
            {
                entity.ToTable("UnidadesMedida", "Configuracion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("Usuario", "Configuracion");

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

                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdLocalidad)
                    .HasConstraintName("FK__Usuario__IdLocal__09D45A2B");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK__Usuario__IdMunic__07EC11B9");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_Usuario_Perfil");

                entity.HasOne(d => d.IdRegimenFiscalNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRegimenFiscal)
                    .HasConstraintName("FK__Usuario__IdRegim__799DF262");

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

            modelBuilder.Entity<UsuarioLocacion>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioLocacion)
                    .HasName("PK__UsurioLo__5354DB914E27E0CD");

                entity.ToTable("UsuarioLocacion", "Configuracion");

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

                entity.ToTable("UsuarioRol", "Configuracion");

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

                entity.ToTable("UsuarioWidget", "Configuracion");

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

                entity.HasOne(d => d.IdPadecimientoNavigation)
                    .WithMany(p => p.Widget)
                    .HasForeignKey(d => d.IdPadecimiento)
                    .HasConstraintName("FK__Widget__IdPadeci__0DC4E6A0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
