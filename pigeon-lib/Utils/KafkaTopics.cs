using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pigeon_lib.Utils
{
	public enum KafkaTopics
	{
		ContactIsCreated,
		LocationIsCreated,
		FirmIsCreated,
		ContactInfoCreated,
		ContactIsRemoved,
		LocationIsRemoved,
		FirmIsRemoved,
		ContactInfoRemoved,
		ContactIsUpdated,
	}
}
