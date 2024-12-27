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
				using var scope = serviceScopeFactory.CreateScope();
				var reportService = scope.ServiceProvider.GetService<ReportService>();
				while (reportService == null)
				{
					Console.WriteLine($"Error: {nameof(KafkaConsumer)} - {nameof(ReportService)}");
					Thread.Sleep(2000);
				}
				var kafkaOptions = scope.ServiceProvider.GetRequiredService<IOptions<KafkaOptions>>().Value;
				Hermes.Subscribe(kafkaOptions, KafkaTopics.ContactIsCreated.ToString(), (obj) => reportService.ContactIsAdded(obj != default ? JsonSerializer.Deserialize<IContact>(obj) : default));
				//Hermes.Subscribe(kafkaOptions, KafkaTopics.ContactIsUpdated.ToString(), (obj) => reportService.ContactIsUpdated(obj != default ? JsonSerializer.Deserialize<IContact>(obj) : default));
				//Hermes.Subscribe(kafkaOptions, KafkaTopics.ContactIsRemoved.ToString(), (obj) => reportService.ContactIsRemoved(obj != default ? JsonSerializer.Deserialize<IContact>(obj) : default));

			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("OperationCanceledException");
			}
			catch (Exception)
			{
				Environment.Exit(1);
			}
		}
	}
}
