using System.Text.Json;
using dotnet_third_party_integrations_core.kafka.models;
using dotnet_third_party_integrations_core.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;
using pigeon_report_service.Services;

namespace pigeon_report_service.Utils.Kafka
{

	public sealed class KafkaConsumer(IServiceScopeFactory serviceScopeFactory) : BackgroundService
	{
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					using var scope = serviceScopeFactory.CreateScope();
					var kafkaService = scope.ServiceProvider.GetRequiredService<KafkaService>();
					var kafkaOptions = scope.ServiceProvider.GetRequiredService<IOptions<KafkaOptions>>().Value;
					var contact = await Hermes.Subscribe<IContact>(kafkaOptions, KafkaTopics.ContactIsCreated.ToString());
					Console.WriteLine(JsonSerializer.Serialize(contact));
				}
			}
			catch (OperationCanceledException)
			{
				// When the stopping token is canceled, for example, a call made from services.msc,
				// we shouldn't exit with a non-zero exit code. In other words, this is expected...
			}
			catch (Exception)
			{
				Environment.Exit(1);
			}
		}
	}
}
