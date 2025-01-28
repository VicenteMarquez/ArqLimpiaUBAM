using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Entities;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Users { get; set; }
    public DbSet<Persona> People { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UsuarioRol> UserRoles { get; set; }
    public DbSet<HistorialInicioSesion> LoginHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}