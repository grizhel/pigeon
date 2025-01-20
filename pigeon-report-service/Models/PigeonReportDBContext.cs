using System.Reflection;
using Microsoft.EntityFrameworkCore;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_report_service.Models.DBModels;

namespace pigeon_report_service.Models;

public class PigeonReportDBContext : DbContext
{
	private readonly IConfiguration configuration;

	public PigeonReportDBContext(DbContextOptions options, IConfiguration configuration) : base(options)
	{
		this.configuration = configuration;
	}

	public DbSet<Report> Reports { get; set; } = null!;

	public DbSet<Info> Info { get; set; } = null!;

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
			modelBuilder.Entity<Report>().ToTable(nameof(Report));
			modelBuilder.Entity<Info>().ToTable(nameof(Info));
		}
		else
		{
			var defaultSchema = configuration.GetConnectionString("Pigeon_v1_schema");
			modelBuilder.HasDefaultSchema(defaultSchema);
		}

#else

			var defaultSchema = configuration.GetConnectionString("Pigeon_v1_schema");

#endif
	}
}
