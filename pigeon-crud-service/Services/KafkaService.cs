using dotnet_third_party_integrations_core.kafka.models;
using Microsoft.Extensions.Options;

using pigeon_lib.BaseModels;

namespace pigeon_crud_service.Services
{
	public class KafkaService : ServiceKafkaBase
	{
		public KafkaService(IOptions<KafkaOptions>? options) : base(options) { }
	}
}
