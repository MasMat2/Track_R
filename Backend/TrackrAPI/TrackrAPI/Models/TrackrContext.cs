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
        public virtual DbSet<AccesoPerfil> AccesoPerfil { get; set; } = null!;
        public virtual DbSet<Compania> Compania { get; set; } = null!;
        public virtual DbSet<Estado> Estado { get; set; } = null!;
        public virtual DbSet<Icono> Icono { get; set; } = null!;
        public virtual DbSet<Lada> Lada { get; set; } = null!;
        public virtual DbSet<Pais> Pais { get; set; } = null!;
        public virtual DbSet<Perfil> Perfil { get; set; } = null!;
        public virtual DbSet<Rol> Rol { get; set; } = null!;
        public virtual DbSet<TipoAcceso> TipoAcceso { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; } = null!;
        public virtual DbSet<UsuarioWidget> UsuarioWidget { get; set; } = null!;
        public virtual DbSet<Widget> Widget { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acceso>(entity =>
            {
                entity.HasKey(e => e.IdAcceso)
                    .HasName("PK__Acceso__99B2858F1DE0EA67");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.HasOne(d => d.IdAccesoPadreNavigation)
                    .WithMany(p => p.InverseIdAccesoPadreNavigation)
                    .HasForeignKey(d => d.IdAccesoPadre)
                    .HasConstraintName("FK__Acceso__IdAcceso__440B1D61");

                entity.HasOne(d => d.IdIconoNavigation)
                    .WithMany(p => p.Acceso)
                    .HasForeignKey(d => d.IdIcono)
                    .HasConstraintName("FK__Acceso__IdIcono__44FF419A");

                entity.HasOne(d => d.IdTipoAccesoNavigation)
                    .WithMany(p => p.Acceso)
                    .HasForeignKey(d => d.IdTipoAcceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Acceso__IdTipoAc__4316F928");
            });

            modelBuilder.Entity<AccesoPerfil>(entity =>
            {
                entity.HasKey(e => e.IdAccesoPerfil)
                    .HasName("PK__AccesoPe__DD31ED4545061A16");

                entity.HasOne(d => d.IdAccesoNavigation)
                    .WithMany(p => p.AccesoPerfil)
                    .HasForeignKey(d => d.IdAcceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccesoPer__IdAcc__4E88ABD4");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.AccesoPerfil)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccesoPer__IdPer__4F7CD00D");
            });

            modelBuilder.Entity<Compania>(entity =>
            {
                entity.HasKey(e => e.IdCompania)
                    .HasName("PK__Compania__12C8F033F60CAAEA");

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

                entity.Property(e => e.Rfc)
                    .HasMaxLength(50)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK__Compania__IdEsta__47DBAE45");

                entity.HasOne(d => d.IdLadaNavigation)
                    .WithMany(p => p.Compania)
                    .HasForeignKey(d => d.IdLada)
                    .HasConstraintName("FK__Compania__IdLada__48CFD27E");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC175427BB9");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estado)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Estado__IdPais__3E52440B");
            });

            modelBuilder.Entity<Icono>(entity =>
            {
                entity.HasKey(e => e.IdIcono)
                    .HasName("PK__Icono__4882B61206CFFE84");

                entity.Property(e => e.Clase).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Lada>(entity =>
            {
                entity.HasKey(e => e.IdLada)
                    .HasName("PK__Lada__372897B7CA6197CF");

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Numero)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__Pais__FC850A7B9F36A8E3");

                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__Perfil__C7BD5CC12281B1FE");

                entity.Property(e => e.Clave).HasMaxLength(8);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Perfil)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Perfil__IdCompan__4BAC3F29");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C34702C05");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoAcceso>(entity =>
            {
                entity.HasKey(e => e.IdTipoAcceso)
                    .HasName("PK__TipoAcce__F55E50ECDD23B06D");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__CA04062B05531269");

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF972C40F025");

                entity.Property(e => e.ApellidoMaterno).HasMaxLength(200);

                entity.Property(e => e.ApellidoPaterno).HasMaxLength(200);

                entity.Property(e => e.Contrasena).HasMaxLength(500);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.ImagenTipoMime).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.TelefonoMovil).HasMaxLength(50);

                entity.HasOne(d => d.IdCompaniaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdCompania)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IdCompa__5629CD9C");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IdPerfi__5535A963");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IdTipoU__5441852A");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioRol)
                    .HasName("PK__UsuarioR__6806BF4A5201DEF6");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioRo__IdRol__59FA5E80");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioRo__IdUsu__59063A47");
            });

            modelBuilder.Entity<UsuarioWidget>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioWidget)
                    .HasName("PK__UsuarioW__E3280363ADFCEDB8");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioWidget)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioWi__IdUsu__5FB337D6");

                entity.HasOne(d => d.IdWidgetNavigation)
                    .WithMany(p => p.UsuarioWidget)
                    .HasForeignKey(d => d.IdWidget)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioWi__IdWid__5EBF139D");
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.HasKey(e => e.IdWidget)
                    .HasName("PK__Widget__F7931B71C3F9EB29");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
