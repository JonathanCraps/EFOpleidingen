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

    public EFOpleidingenContext() { }
    public EFOpleidingenContext(DbContextOptions<EFOpleidingenContext> options) : base(options) { }

    public DbSet<Docent> Docenten => Set<Docent>();
    public DbSet<Opleiding> Opleidingen => Set<Opleiding>();
    public DbSet<DocentOpleiding> DocentOpleidingen => Set<DocentOpleiding>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory
            .GetParent(AppContext.BaseDirectory)!.FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();
        }
        
        var connectionString = configuration.GetConnectionString(connection);
        if (connectionString != null) {
            optionsBuilder.UseSqlServer(connectionString, options => options.MaxBatchSize(150));
        }
        else
        {
            testMode = true;
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocentConfiguration());
        modelBuilder.ApplyConfiguration(new OpleidingConfiguration());
        modelBuilder.ApplyConfiguration(new DocentOpleidingConfiguration());
        if (!testMode)
        {
            modelBuilder.ApplyConfiguration(new DocentSeeding());
            modelBuilder.ApplyConfiguration(new OpleidingSeeding());
            modelBuilder.ApplyConfiguration(new DocentOpleidingSeeding());
        }
        base.OnModelCreating(modelBuilder);
    }
}
