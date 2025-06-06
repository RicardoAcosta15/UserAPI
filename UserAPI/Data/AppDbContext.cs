using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }    // Tabla de usuarios
    public DbSet<Phone> Phones { get; set; }  // Tabla de teléfonos

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}