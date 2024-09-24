global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Model.Repositories;
public class EFOpleidingenContext : DbContext
{
    public static IConfigurationRoot configuration = null!;
    bool testMode = false;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!testMode) { }
        base.OnModelCreating(modelBuilder);
    }
}
