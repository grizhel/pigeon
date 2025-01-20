using System.Reflection;
using Microsoft.EntityFrameworkCore;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Models.Interfaces.ModelInterfaces;

namespace pigeon_crud_service.Models;

public class PigeonCrudDBContext : DbContext
{
	private readonly IConfiguration configuration;

	public PigeonCrudDBContext(DbContextOptions options, IConfiguration configuration) : base(options)
	{
		this.configuration = configuration;
	}

	public DbSet<Contact> Contacts { get; set; } = null!;

	public DbSet<Firm> Firms { get; set; } = null!;

	public DbSet<Location> Locations { get; set; } = null!;

	public DbSet<ContactInformation> ContactInformations { get; set; } = null!;

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);
		_ = configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

#if DEBUG

		bool IsRunningFromNUnit =
			 AppDomain.CurrentDomain.GetAssemblies().Any(
					 a => a.FullName.ToLowerInvariant().StartsWith("nunit"));

		if (IsRunningFromNUnit)
		{
			var defaultSchema = "test";
			modelBuilder.HasDefaultSchema(defaultSchema);
			modelBuilder.Entity<Contact>().ToTable(nameof(Contact));
			modelBuilder.Entity<Firm>().ToTable(nameof(Firm));
			modelBuilder.Entity<Location>().ToTable(nameof(Location));
			modelBuilder.Entity<ContactInformation>().ToTable(nameof(ContactInformation));
		}
		else
		{
			var defaultSchema = configuration.GetConnectionString("Pigeon_v1_schema");
			modelBuilder.HasDefaultSchema(defaultSchema);
		}

#else

		var defaultSchema = configuration.GetConnectionString("Pigeon_v1_schema");
		modelBuilder.HasDefaultSchema(defaultSchema);
			
#endif



	}
}
