using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Agendatelefonica.Models
{
    public partial class AgendatelefonicaContext : DbContext
    {
        public AgendatelefonicaContext()
        {
        }

        public AgendatelefonicaContext(DbContextOptions<AgendatelefonicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Electromecanica> Electromecanicas { get; set; }
        public virtual DbSet<Estacione> Estaciones { get; set; }
        public virtual DbSet<Mantenedore> Mantenedores { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server= DESKTOP-BP6RUJQ\\SQLEXPRESS; Initial catalog                 =Agendatelefonica; Trusted_Connection = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Electromecanica>(entity =>
            {
                entity.ToTable("Electromecanica");

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Extension).HasMaxLength(20);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Subsistema)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Estacione>(entity =>
            {
                entity.Property(e => e.Boleteria).HasMaxLength(10);

                entity.Property(e => e.CabinaAnden)
                    .HasMaxLength(10)
                    .HasColumnName("cabina_anden");

                entity.Property(e => e.CuartoAtbt)
                    .HasMaxLength(10)
                    .HasColumnName("Cuarto_ATBT");

                entity.Property(e => e.CuartoCom)
                    .HasMaxLength(10)
                    .HasColumnName("cuarto_com");

                entity.Property(e => e.CuartoControl)
                    .HasMaxLength(15)
                    .HasColumnName("Cuarto_Control");

                entity.Property(e => e.Enclavamiento).HasMaxLength(10);

                entity.Property(e => e.Estacion)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Interfono)
                    .HasMaxLength(10)
                    .HasColumnName("interfono");

                entity.Property(e => e.Linea)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PstnEmergencia)
                    .HasMaxLength(20)
                    .HasColumnName("PSTN_emergencia");

                entity.Property(e => e.SubestacionTraccion)
                    .HasMaxLength(10)
                    .HasColumnName("Subestacion_traccion");
            });

            modelBuilder.Entity<Mantenedore>(entity =>
            {
                entity.Property(e => e.Extension).HasMaxLength(15);

                entity.Property(e => e.Funcion)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Mantenedor)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.RadioTetra).HasMaxLength(15);

                entity.Property(e => e.Subsistema)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
