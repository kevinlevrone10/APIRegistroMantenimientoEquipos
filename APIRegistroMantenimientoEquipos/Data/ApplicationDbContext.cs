using APIRegistroMantenimientoEquipos.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace APIRegistroMantenimientoEquipos.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Administrador> Usuarios { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }

        public DbSet<EstadoEquipo> EstadosEquipo { get; set; }
        public DbSet<EstadoMantenimiento> EstadosMantenimiento { get; set; }
        public DbSet<EstadoReparacion> EstadosReparacion { get; set; }
        public DbSet<TipoMantenimiento> TiposMantenimiento { get; set; }

        public DbSet<EstadoTrabajador> EstadoTrabajador { get; set; }



     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para EstadoTrabajador
            modelBuilder.Entity<EstadoTrabajador>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(200);
            });

            // Configuración para Trabajador
            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Correo).IsUnique();
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Cargo).IsRequired().HasMaxLength(100);

                // FK con EstadoTrabajador
                entity.HasOne(e => e.Estado)
                    .WithMany()
                    .HasForeignKey(e => e.EstadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.FechaContratacion).IsRequired();
            });

            // Configuración para Administrador
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Correo).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            });


            // Configuración para Equipo
            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NumeroSerie).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.NumeroSerie).IsUnique();
                entity.Property(e => e.FechaAdquisicion).IsRequired();

                // Configuración de la relación con EstadoEquipo
                entity.HasOne(e => e.Estado)
                      .WithMany()  // La clase EstadoEquipo no tiene una propiedad de navegación hacia Equipo
                      .HasForeignKey(e => e.EstadoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Mantenimientos)
                    .WithOne(e => e.Equipo)
                    .HasForeignKey(e => e.EquipoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Reparaciones)
                    .WithOne(e => e.Equipo)
                    .HasForeignKey(e => e.EquipoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para EstadoEquipo
            modelBuilder.Entity<EstadoEquipo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(200);
            });

            // Relación Equipo -> EstadoEquipo
            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasOne(e => e.Estado)
                    .WithMany()
                    .HasForeignKey(e => e.EstadoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // Configuración de la relación para Mantenimiento
            modelBuilder.Entity<Mantenimiento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FechaInicio).IsRequired();
                entity.Property(e => e.FechaFin);
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Observaciones).HasMaxLength(500);

                // Relación con Equipo
                entity.HasOne(e => e.Equipo)
                      .WithMany(e => e.Mantenimientos)
                      .HasForeignKey(e => e.EquipoId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Trabajador
                entity.HasOne(e => e.Trabajador)
                      .WithMany(e => e.Mantenimientos)
                      .HasForeignKey(e => e.TrabajadorId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con EstadoMantenimiento
                entity.HasOne(e => e.Estado)
                      .WithMany()
                      .HasForeignKey(e => e.EstadoId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con TipoMantenimiento
                entity.HasOne(e => e.Tipo)
                      .WithMany()
                      .HasForeignKey(e => e.TipoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de EstadoMantenimiento
            modelBuilder.Entity<EstadoMantenimiento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(200);
            });


            modelBuilder.Entity<Reparacion>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Relación con EstadoReparacion (con EstadoId)
                entity.HasOne(e => e.Estado)
                      .WithMany()
                      .HasForeignKey(e => e.EstadoId)  // Relación con la clave foránea EstadoId
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Equipo)
                      .WithMany(e => e.Reparaciones)
                      .HasForeignKey(e => e.EquipoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Trabajador)
                      .WithMany(e => e.Reparaciones)
                      .HasForeignKey(e => e.TrabajadorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EstadoReparacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Descripcion).HasMaxLength(200);
            });

        }
    }
}
