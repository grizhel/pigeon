using dotnet_third_party_integrations_core.kafka.models;
using dotnet_third_party_integrations_core.Kafka;
using Microsoft.Extensions.Options;
using pigeon_crud_service.Services;
using pigeon_lib.Models.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;
using System.Text.Json;

namespace pigeon_crud_service.Utils.Kafka
{
	public sealed class KafkaConsumer(IServiceScopeFactory serviceScopeFactory) : BackgroundService
	{
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			using var scope = serviceScopeFactory.CreateScope();
			var kafkaOptions = scope.ServiceProvider.GetRequiredService<IOptions<KafkaOptions>>().Value;
			try
			{
			
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
