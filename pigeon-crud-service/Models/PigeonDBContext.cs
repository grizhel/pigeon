using Microsoft.EntityFrameworkCore;
using pigeon_crud_service.Models.DBModels;

namespace pigeon_crud_service.Models;

public class PigeonDBContext : DbContext
{
    private IConfiguration configuration;

    public PigeonDBContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public PigeonDBContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        this.configuration = configuration;
    }

    public DbSet<Contact> Contacts { get; set; } = null!;

    public DbSet<Firm> Firms { get; set; } = null!;

    public DbSet<Location> Locations { get; set; } = null!;

    public DbSet<ContactInfo> ContactInformations { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        _ = configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var defaultSchema = configuration.GetConnectionString("DefaultSchema");
        if (defaultSchema != null && defaultSchema != "public")
        {
            modelBuilder.HasDefaultSchema(configuration.GetConnectionString("DefaultSchema"));
        }
    }
}
