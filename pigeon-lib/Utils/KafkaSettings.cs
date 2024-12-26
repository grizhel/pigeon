using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pigeon_lib.Utils
{
	public class ConsumerConfig
	{
		public string AutoOffsetReset { get; set; }

		public string EnableAutoCommit { get; set; }

		public string EnableAutoOffsetStore { get; set; }

		public string GroupId { get; set; }

		public string SessionTimeoutMs { get; set; }

		public string StatisticsIntervalMs { get; set; }
	}


	public class KafkaSettings
	{
		public string BootstrapServers { get; set; }

		public ConsumerConfig ConsumerConfig { get; set; }
	}
}
