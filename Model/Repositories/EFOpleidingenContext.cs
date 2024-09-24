global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Model.Repositories.Configurations;
using Model.Repositories.Seedings;
namespace Model.Repositories;
public class EFOpleidingenContext : DbContext
{
    public static IConfigurationRoot configuration = null!;
    bool testMode = false;
    string connection = "efopleiding";

    public DbSet<Docent> Docenten => Set<Docent>();
    public DbSet<Opleiding> Opleidingen => Set<Opleiding>();
    public DbSet<DocentOpleiding> DocentOpleidingen => Set<DocentOpleiding>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName).AddJsonFile("appsettings.json", false).Build();
        var connectionString = configuration.GetConnectionString(connection);
        if(connectionString != null) optionsBuilder.UseSqlServer(connectionString, options => options.MaxBatchSize(150));
    
}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocentConfiguration());
        modelBuilder.ApplyConfiguration(new OpleidingConfiguration());
        modelBuilder.ApplyConfiguration(new DocentOpleidingConfiguration());
        if (!testMode) {
            modelBuilder.ApplyConfiguration(new DocentSeeding());
            modelBuilder.ApplyConfiguration(new OpleidingSeeding());
        }
        base.OnModelCreating(modelBuilder);
    }
}
