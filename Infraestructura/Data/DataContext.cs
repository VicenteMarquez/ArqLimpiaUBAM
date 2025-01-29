using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }
     
        public DbSet<Contacto> Contactos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.ToTable("tbl_Contacto");
                entity.HasKey(c => c.ContactoId);
                entity.Property(c => c.Contacto_TelefonoPersonal)
                      .IsRequired()
                      .HasMaxLength(15);
                entity.Property(c => c.Contacto_Correo)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(c => c.Contacto_TelefonoCasa)
                      .IsRequired()
                      .HasMaxLength(15);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("tbl_Persona");
                entity.HasKey(p => p.Persona_Id);
                entity.Property(p => p.Persona_Nombre)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(p => p.Persona_ApellidoPaterno)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(p => p.Persona_ApellidoMaterno)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(p => p.Persona_FechaNacimiento)
                      .IsRequired();
                entity.HasOne(p => p.Contactos)
                    .WithMany(c => c.Personas)
                    .HasForeignKey(p => p.Persona_ContactoId);
                entity.Property(p => p.Persona_Activo)
                      .IsRequired();
              
            });

            modelBuilder.Entity<Usuario>(entity =>
                {
                    entity.ToTable("tbl_Usuario");
                    entity.HasKey(u => u.Usuario_Id);
                    entity.Property(u => u.Usuario_Nombre)
                          .IsRequired()
                          .HasMaxLength(50);
                    entity.Property(u => u.Usuario_ContraHash)
                          .IsRequired()
                          .HasMaxLength(100);
                    entity.Property(u => u.Usuario_Rol)
                          .IsRequired()
                          .HasMaxLength(20);
                    entity.HasOne(u => u.Persona)
              .WithMany(p => p.usuarios)
              .HasForeignKey(u => u.Usuario_PersonaId)
              .OnDelete(DeleteBehavior.SetNull);
                });
        }

    }
}
