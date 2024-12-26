using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dotnet_third_party_integrations_core.kafka.models;
using dotnet_third_party_integrations_core.Kafka;
using Microsoft.Extensions.Options;

namespace pigeon_lib.BaseModels
{
	public class ServiceKafkaBase
	{
		private readonly KafkaSettings kafkaSettings;

		public ServiceKafkaBase(IOptions<KafkaSettings>? options = null) 
		{
			kafkaSettings = options?.Value ?? new();
		}
		/// <summary>
		/// Eventhough the method is not to be used by all services, it was convenient to implement here under the circumstances.
		/// </summary>
		/// <param name="topic"></param>
		/// <param name="data"></param>
		public void SendMessage(string topic, object data)
		{
			Hermes.SendMessage(kafkaSettings, topic, data);
		}

		/// <summary>
		/// Eventhough the method is not to be used by all services, it was convenient to implement here under the circumstances.
		/// </summary>
		/// <param name="topic"></param>
		/// <param name="data"></param>
		public void Subscribe<T>(string topic, object data)
		{
			Hermes.Subscribe<T>(kafkaSettings, topic);
		}
	}
}
