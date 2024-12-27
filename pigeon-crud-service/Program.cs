using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using pigeon_crud_service.Models;
using pigeon_crud_service.Services;
using Microsoft.EntityFrameworkCore.Migrations;
using pigeon_lib.Utils;
using dotnet_third_party_integrations_core.kafka.models;
using pigeon_crud_service.Utils.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); ;

builder.Services.AddHttpClient();

builder.Services.AddScoped<FirmService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddHostedService<KafkaConsumer>();

builder.Services.AddDbContext<PigeonDBContext>(options =>
{
	_ = options.UseNpgsql(builder.Configuration.GetConnectionString("Pigeon_v1"), x
							 => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
							 builder.Configuration.GetConnectionString("Pigeon_v1_schema")));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
		{
				{
						new OpenApiSecurityScheme
						{
								Reference = new OpenApiReference
								{
										Type = ReferenceType.SecurityScheme,
										Id = "Bearer"
								},
								Scheme = "oauth2",
								Name = "Bearer",
								In = ParameterLocation.Header,

						},
						new List<string>()
				}
		});
});

var appOptionsSection = builder.Configuration.GetSection("AppOptions");
builder.Services.Configure<IAppOptions>(appOptionsSection);

var kafkaOptionsSection = builder.Configuration.GetSection("KafkaOptions");
builder.Services.Configure<KafkaOptions>(kafkaOptionsSection);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(x => x.SetIsOriginAllowed(t => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
