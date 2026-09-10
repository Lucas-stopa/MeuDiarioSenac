using Microsoft.EntityFrameworkCore;
using DiarioSenac.Model;

namespace DiarioSenac.Data;

public class DiarioSenacContext : DbContext
{
    public DbSet<Registro> Registros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    private const string StringConexao =
        "Server=127.0.0.1;Port=3306;Database=DiarioSenac;Uid=root;Pwd=admin;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(StringConexao,ServerVersion.AutoDetect(StringConexao));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar Registro
        modelBuilder.Entity<Registro>()
            .HasKey(r => r.Id);
        
        modelBuilder.Entity<Registro>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();

        // Configurar Usuario
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.Id);
        
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        // Configurar relacionamento
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
 
 
