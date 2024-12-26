using Microsoft.EntityFrameworkCore;

namespace pigeon_report_service.Models;

public class PigeonReportDBContext : DbContext
{
	private readonly IConfiguration configuration;

	public PigeonReportDBContext(DbContextOptions options, IConfiguration configuration) : base(options)
	{
		this.configuration = configuration;
	}

	public DbSet<Report> Reports { get; set; } = null!;

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);
		_ = configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var defaultSchema = configuration.GetConnectionString("Pigeon_v1_schema");
		modelBuilder.HasDefaultSchema(defaultSchema);
	}
}
