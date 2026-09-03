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
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId);
        
    }
}
 
 
